#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.Models;
using GUIStd.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】考勤（AA）異動 API：新增考勤、修改考勤、刪除考勤；呼叫 BlAA → DaAA
    /// </summary>
    [Route("py/[controller]")]
    public class AttendanceController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlAA BlAA => mBlAA = mBlAA ?? new BlAA(this.ClientContent);
        private BlAA mBlAA;

        #endregion

        /// <summary>
        /// 取得員工當日考勤資料
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="employeeId">員工編號</param>
        /// <param name="date">日期</param>
        /// <param name="attCodes">考勤代碼陣列</param>
        /// <returns>考勤紀錄資料模型泛型集合物件</returns>
        [HttpPost("query/{compId}/{employeeId}/{date}")]
        public IEnumerable<MdAttendance_r> GetAttendance(string compId, string employeeId, string date, [FromBody] string[] attCodes)
        {
            return BlAA.GetAttendance(compId, employeeId, date, attCodes);
        }

        /// <summary>
        /// vABM01：分頁查詢員工考勤清單（vQPattern getData）
        /// </summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdAttendance_p GetData(
            [DARange(1, int.MaxValue)] int pageNo,
            [FromBody] MdAttendance_q query)
        {
            var _rowsPerPage = query?.RowsPerPage ?? 0;
            return BlAA.GetData(ControlName, pageNo, ref _rowsPerPage, query);
        }

        /// <summary>
        /// vABM01：考勤複合主鍵是否已存在（vQPattern IsExist）；請求體 MdAttendance 鍵欄位
        /// </summary>
        [HttpPost("exists")]
        public bool IsExist([FromBody] MdAttendance request)
        {
            return BlAA.IsExist(request);
        }

        /// <summary>
        /// vABM01：依主鍵取得單筆考勤資料（vQPattern getRow）；請求體 MdAttendance 鍵欄位
        /// </summary>
        [HttpPost("row")]
        public MdAttendance_row GetRow([FromBody] MdAttendance request)
        {
            return BlAA.GetRow(request);
        }

        #region " 共用函式 - 異動考勤 "

        /// <summary>
        /// 刪除一筆考勤（AA）；請求體使用 Share/AA 的 MdAttendance（AA01, AA03, AA04, AA05，AA16 對應 JSON shiftId）
        /// </summary>
        /// <param name="request">考勤資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("delete")]
        public MdApiMessage Delete([FromBody] MdAttendance request)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlAA.Delete(request);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增一筆考勤（AA）；vABM01 vQPattern insert
        /// </summary>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdAttendance request)
        {
            if (request == null)
                return HttpContext.Response.InsertFailed(new ArgumentNullException(nameof(request)));
            try
            {
                int _result = BlAA.ProcessInsert(request);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改一筆考勤（AA）；vABM01 vQPattern update；請求體 original 為編輯前主鍵、data 為新資料
        /// </summary>
        [HttpPost("update")]
        public MdApiMessage Update([FromBody] MdAttendance_update request)
        {
            if (request == null)
                return HttpContext.Response.UpdateFailed(new ArgumentNullException(nameof(request)));
            try
            {
                int _result = BlAA.ProcessUpdate(request);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 新增或修改一筆考勤（AA）；請求體使用 Share/AA 的 MdAttendance（vABM08 等 upsert）
        /// </summary>
        /// <param name="request">考勤資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("save")]
        public MdApiMessage SaveAttendance([FromBody] MdAttendance request)
        {
            if (request == null)
                return HttpContext.Response.UpdateFailed(new ArgumentNullException(nameof(request)));
            try
            {
                // 呼叫商業元件執行新增或修改作業（新增時回傳指派的 AA16，JSON 鍵與 MdAttendance.AA16 一致：shiftId）
                var (_result, _assignedAa16) = BlAA.SaveAttendance(request);
                object _responseData = null;
                if (_result > 0 && !string.IsNullOrEmpty(_assignedAa16))
                    _responseData = new { shiftId = _assignedAa16 };

                // 回應前端存檔成功訊息
                return HttpContext.Response.UpdateSuccess(_result, responseData: _responseData);
            }
            catch (Exception ex)
            {
                // 回應前端存檔失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
