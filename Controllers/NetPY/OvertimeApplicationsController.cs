#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.PY;
using GUIStd.DAL.PY.Models;
using GUIStd.DAL.PY.Models.Private.OvertimeApplication;
using GUIStd.BLL.PY.Private;
using GUIStd.Attributes;
using GUIStd.Extensions;
using GUIStd.DAL.PY.Models.Private;
using GUIStd.BLL.GUI;
using GUIStd.BLL.AllNewPY;
using System.Linq;
using GUIStd.DAL.AllNewPY.Models;

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
    /// <summary>
    /// 【需經驗證】加班申請資料控制器
    /// </summary>
    [Route("netpy/[controller]")]
    public class OvertimeApplicationsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlDW BlDW => new BlDW(ClientContent);
        private BlDS BlDS => new BlDS(ClientContent);
        private BlMail BlMail => new BlMail(ClientContent);
        private BlOvertimeApplications BlOvertimeApplications => new BlOvertimeApplications(ClientContent);
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
            return BlOvertimeApplications.GetCloseDateForAB(compId);
        }

        /// <summary>
        /// 取得加班單申請紀錄
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="appNo">加班單號</param>
        /// <returns>加班單資料模型泛型集合物件</returns>
        [HttpGet("{compId}/{appNo}")]
        public MdOvertimeApplication GetRow(string compId, string appNo)
        {
            return BlOvertimeApplications.GetRow(compId, appNo);
        }

        /// <summary>
        /// 取得加班單申請紀錄
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="dateS">起始日期</param>
        /// <param name="dateE">截止日期</param>
        /// <returns>加班單資料模型泛型集合物件</returns>
        [HttpGet("{employeeId}/{dates}/{datee}")]
        public IEnumerable<MdOvertimeApplication> GetData(string employeeId, string dateS, string dateE)
        {
            return BlOvertimeApplications.GetData(employeeId, dateS, dateE);
        }

        /// <summary>
        /// 取得加班單申請紀錄(分頁)
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="dates">起始日期</param>
        /// <param name="datee">截止日期</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns>加班單資料模型泛型集合物件</returns>
        [HttpGet("pages/{employeeId}/{dates}/{datee}/{pageNo}")]
        public MdOvertimeApplications_p GetDataByPage(string employeeId, string dates, string datee, 
            [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlOvertimeApplications.GetDataByPage(employeeId, dates, datee, pageNo, ControlName);
        }

        /// <summary>
        /// 檢查加班日期是否已申請加班單
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="employeeId">員工編號</param>
        /// <param name="overtimeApplicationDate">加班日期</param>
        /// <returns></returns>
        [HttpGet("exists/{companyId}/{employeeId}/{overtimeApplicationDate}")]
        public string IsExists(string companyId, string employeeId, string overtimeApplicationDate)
        {
            return BlDW.IsExistOvertimeApp(companyId, employeeId, overtimeApplicationDate);
        }

        /// <summary>
        /// 取得日別資訊並判斷能否加班
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="employeeId">員工編號</param>
        /// <param name="leaveDate">請假日</param>
        /// <param name="leaveCode">請假代碼</param>
        /// <returns>日別與允許加班資訊</returns>
        [HttpGet("allowovertime/{companyId}/{employeeId}/{leaveDate}/{leaveCode}")]
        public MdAllowOvertimeInfo IsAllowOvertime(string companyId, string employeeId, string leaveDate, string leaveCode)
        {
            return BlOvertimeApplications.IsAllowOvertime(companyId, employeeId, leaveDate, leaveCode);
        }


        /// <summary>
        /// 取得加班原因代碼輔助資料
        /// </summary>
        /// <param name="includeEmptyRow">是否包含空白行</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>線上請假原因代碼輔助模型泛型物件</returns>
        [HttpGet("help/reasons")]
        public IEnumerable<MdReasonCode> GetHelp([FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
        {
            return BlDS.GetHelp("02", includeEmptyRow, includeId);
        }

        /// <summary>
        /// 加班撤銷原因代碼輔助資料
        /// </summary>
        /// <param name="includeEmptyRow">是否包含空白行</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>線上請假原因代碼輔助模型泛型物件</returns>
        [HttpGet("help/revokereasons")]
        public IEnumerable<MdReasonCode> GetHelpRevoke([FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
        {
            return BlDS.GetHelp("03", includeEmptyRow, includeId);
        }

        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary>
        /// 新增加班單
        /// </summary>
        /// <param name="obj">加班單資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdOvertimeApplication_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlOvertimeApplications.ProcessCeateOvertimeApplication(obj, out string appNo);
                // 發送Email
                _result += BlMail.SendMail(obj.DW01, obj.DW02);
                var _resultObj = HttpContext.Response.InsertSuccess(_result,
                    responseData: new Hashtable() { { "appNo", appNo } });

                // 處理高階主管一關過情境
                if (obj.DW12 == "2")
                {
                    //依照加班明細選擇轉薪/轉休，依照不同加班倍率的級距轉換為多筆考勤紀錄
                    BlAppAttendances.WriteOvertimeAttendance(obj, BlOvertimeApplications.OvertimeCode, BlOvertimeApplications.OvertimeCodeL);

                }

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
        /// 加班單註銷申請
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="origAppNo">原加班單單號</param>
        /// <param name="reasonCode">撤銷原因代碼</param>
        /// <param name="revokeReason">撤銷原因</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("revoke/{compId}/{origAppNo}/{reasonCode}")]
        public MdApiMessage Revoke(string compId, string origAppNo,string reasonCode, [FromBody] string[] revokeReason)
        {
            try
            {
                // 以原單號抓取原加班單當作基底
                MdOvertimeApplication_w obj = BlOvertimeApplications.GetDataForRevoke(compId, origAppNo);
                var _overtimeReasonCode = obj.DW612;
                var _overtimeReason = obj.DW11;
                obj.DW612 = reasonCode;
                obj.DW11 = revokeReason[0];

                // 呼叫商業元件執行加班單撤銷申請單
                int _result = BlOvertimeApplications.ProcessCeateOvertimeApplication(obj, out string appNo);
                // 發送Email
                _result += BlMail.SendMail(compId, appNo);

                // 處理高階主管一關過情境
                if (obj.DW12 == "2")
                {
                    //依照加班明細選擇轉薪/轉休，依照不同加班倍率的級距轉換為多筆考勤紀錄
                    BlAppAttendances.WriteOvertimeAttendance(obj, BlOvertimeApplications.OvertimeCode, BlOvertimeApplications.OvertimeCodeL, _overtimeReasonCode, _overtimeReason);

                    // 處理加班撤銷同意，更新原單加班單
                    _result += BlOvertimeApplications.ProcessRevoke(compId, origAppNo, revokeReason[0]);
                }
                var _appObj = BlOvertimeApplications.GetRow(compId, appNo);
                var _resultObj = HttpContext.Response.InsertSuccess(_result,
                    responseData: _appObj);

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
        /// 加班單刪除
        /// </summary>
        /// <param name="appNo">加班單號</param>
        /// <param name="compId">公司別</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{compId}/{appNo}")]
        public MdApiMessage DeleteApp(string compId, string appNo)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlOvertimeApplications.ProcessDelete(compId, appNo);
                var _resultObj = HttpContext.Response.DeleteSuccess(_result);

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
