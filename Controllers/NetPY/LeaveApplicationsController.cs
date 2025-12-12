#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.Attributes;

using GUIStd.BLL.GUI;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.BLL.PY.Private;
using GUIStd.DAL.PY.Models;
using GUIStd.DAL.PY.Models.Private.LeaveApplication;
using GUIStd.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
    /// <summary>
    /// 【需經驗證】假單申請資料控制器
    /// </summary>
    [Route("netpy/[controller]")]
    public class LeaveApplicationsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlAA BlAA => new BlAA(ClientContent);
        private BlAO BlAO => new BlAO(ClientContent);
        private BlAQ BlAQ => new BlAQ(ClientContent);
        private BlMail BlMail => new BlMail(ClientContent);
        private BlLeaveApplications BlLeaveApplications => new BlLeaveApplications(ClientContent);
        private BlAppAttendances BlAppAttendances => new BlAppAttendances(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 假單申請關帳日
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <returns>關帳日期YYYYMMDD</returns>
        [HttpGet("closedate/{compId}")]
        public string GetCloseDateForAB(string compId)
        {
            return BlLeaveApplications.GetCloseDateForAB(compId);
        }

        /// <summary>
        /// 員工忘打卡紀錄
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns></returns>
        [HttpGet("pages/missingpunch/{employeeId}/{pageNo}")]
        public MdMissingPunch_p GetMissingPunchData(string employeeId, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlLeaveApplications.GetMissingPunchData(employeeId, pageNo, ControlName);
        }


        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary>
        /// 產生請假單
        /// </summary>
        /// <param name="obj">請假單資料物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdApplication_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlLeaveApplications.ProcessCeateLeaveApplication(obj, out string appNo, out string managerId, out string managerName);
                // 發送Email
                _result += BlMail.SendMail(obj.DW01, obj.DW02);

                // 處理高階主管一關過情境
                if (obj.DW12 == "2")
                {
                    foreach(var _dwa in obj.Details)
                    {
                        // 準備考勤資料
                        var _attendance = new MdAttendance_w()
                        {
                            AA01 = obj.DW01,
                            AA02 = "Z",
                            AA03 = obj.DW03,
                            AA04 = _dwa.DWA08,
                            AA05 = obj.DW05,
                            AA06 = 1,
                            AA10 = _dwa.DWA04,
                            AA14 = obj.DW611,
                            AA15 = obj.DW04,
                            AA16 = "", //班別需另外判定
                            AA17 = _dwa.DWA03,
                            AA18 = _dwa.DWA03 ,
                            AA19 = _dwa.DWA11.Left(8),
                            AA20 = _dwa.DWA03,
                            AA21 = _dwa.DWA12.Left(8),
                            AA22 = $"{appNo}_{obj.DW11}",
                            AA23 = "1",
                            AA24 = obj.DW15,
                            AA25 = _dwa.DWA04,
                        };
                        // 寫入考勤紀錄
                        _result += BlAppAttendances.WriteAppAttendance(_attendance, appNo, obj.DW64,
                        out bool _updateAO);

                        // 更新班表
                        if (_updateAO)
                        {
                            var _shift = BlAO.GetEmployeeShifts(obj.DW04, obj.DW06).FirstOrDefault();
                            // 考勤數量大於應計工時，或者大於等於1日，則須回寫班表
                            if (_attendance.AA10 >= _shift.AL02 || _attendance.Unit == BlLeaveApplications.AllNewLeaveDayUnit && _attendance.AA10 > 0)
                            {
                                BlAO.MergeEmployShiftToLeave(new MdShift_w()
                                {
                                    AO01 = obj.DW04,
                                    AO02 = _dwa.DWA03,
                                    AO03 = obj.DW05,
                                    AO04 = obj.DW611,
                                    AO08 = obj.DW01,
                                    AO15 = "1",
                                });
                            }
                        }

                        // 處理銷假同意，更新原單假單
                        if (!string.IsNullOrWhiteSpace(obj.DW64))
                        {
                            // TODO

                        }
                    }
                }

                // 更新假單單號到對應刷卡與排班資料
                var _resultObj = HttpContext.Response.InsertSuccess(_result,
                responseData: new Hashtable() {
                    { "appNo", appNo },
                    { "status", "1"},
                    { "managerId",managerId},
                    { "managerName",managerName}
                });

                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 產生忘卡申請單
        /// </summary>
        /// <param name="obj">忘卡申請單物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("missedpunch")]
        public MdApiMessage InsertMissedPunchApp([FromBody] MdMissingPunch_w obj)
        {
            try
            {
                // 判斷是否為臨時班忘卡申請
                var _isExtraShift = obj.AO03.ToUpper().StartsWith("EXT");
                // 判斷是否為兼職打卡
                var _isParttimePunch = string.Compare(obj.EmployeeDept, obj.PunchDept, true) != 0; 
                // 檢查打卡時間
                var _punchTime = $"{obj.DW06}{obj.DW061.Left(4)}";
                if (BlLeaveApplications.IsPunchExists(_punchTime, obj.AO05, obj.DW04, _isParttimePunch, out string errMsg))
                    return HttpContext.Response.InsertFailed(new Exception(errMsg));

                // 呼叫商業元件執行修改作業
                int _result = BlLeaveApplications.ProcessCeateLeaveApplication(obj, out string appNo, out string managerId, out string managerName);
                // 發送Email
                _result += BlMail.SendMail(obj.DW01, obj.DW02);
                // 忘卡類型
                var _punchType = "";
                // 更新假單單號到對應刷卡與排班資料
                if (_isExtraShift)
                    _result += BlAQ.ProcessUpdateAQ32(new MdPunch_w()
                    {
                        AQ11 = obj.PunchComp,
                        AQ07 = obj.PunchDept,
                        AQ06 = obj.DW04,
                        AQ08 = obj.AO03,
                        AQ09 = obj.AO02,
                        AQ32 = appNo
                    }, out _punchType);
                else
                    _result += BlAO.ProcessUpdateAO20(new MdShift_w()
                    {
                        AO08 = obj.DW01,
                        AO01 = obj.DW04,
                        AO02 = obj.AO02,
                        AO03 = obj.AO03,
                        AO10 = obj.AO10,
                        AO20 = appNo
                    }, out _punchType);
                
                // 處理高階主管一關過情境
                if (obj.DW12== "2")
                {
                    // 準備打卡紀錄
                    var _makeupPunch = new MdPunch_w()
                    {
                        AQ01 = _punchTime,
                        AQ02 = _punchType,
                        AQ03 = _isParttimePunch ? obj.DW03 : obj.DW04,
                        AQ06 = obj.DW04,
                        AQ07 = obj.PunchDept,
                        AQ08 = obj.AO03,
                        AQ09 = obj.AO02,
                        AQ10 = obj.AO10,
                        AQ11 = obj.PunchComp,
                        AQ17 = _isParttimePunch ? "" : "1",
                        AQ32 = appNo,
                        EmployeeSocialId = obj.DW03,
                    };
                    // 回寫打卡資料
                    _result += BlAQ.Insert(_makeupPunch, out BlAQ.PunchJudge _judge, out int _timeDiv);

                    // 準備考勤資料
                    var _dwa = obj.Details.FirstOrDefault();
                    var _attendance = new MdAttendance_w()
                    {
                        AA01 = obj.DW01,
                        AA02 = "Z",
                        AA03 = obj.DW03,
                        AA04 = _dwa?.DWA08 ?? obj.DW06,
                        AA05 = obj.DW05,
                        AA06 = 1,
                        AA10 = obj.DW08,
                        AA14 = obj.PunchDept,
                        AA15 = obj.DW04,
                        AA16 = "", //班別需另外判定
                        AA17 = obj.DW06,
                        AA18 = obj.DW06,
                        AA19 = obj.DW061.Left(8),
                        AA20 = obj.DW07,
                        AA21 = obj.DW071.Left(8),
                        AA22 = $"{appNo}_{obj.DW11}",
                        AA23 = "1",
                        AA24 = obj.DW15,
                        AA25 = obj.DW17,
                    };
                    // 寫入考勤紀錄
                    _result += BlAppAttendances.WriteAppAttendance(_attendance, appNo, obj.DW64,
                    out bool _updateAO);


                    // 遲到、早退、曠職
                    if (_judge != BlAQ.PunchJudge.Normal)
                    {
                        _attendance.AA16 = obj.AO03; // 填入班別
                        _attendance.AA17 = ""; // 沒有發布日期
                        _attendance.AA22 = _punchTime; // 備註為打卡時間
                        BlAA.WritePunchAttendance(_attendance, _judge, _timeDiv);
                    }

                }

                var _resultObj = HttpContext.Response.InsertSuccess(_result,
                    responseData: new Hashtable() {
                        { "appNo", appNo },
                        { "status", obj.DW12},
                        { "managerId",managerId},
                        { "managerName",managerName}
                    });

                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }



        #endregion
    }
}
