#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.AllNewGUI;
using GUIStd.BLL.AllNewPY;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.ABM08;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.DAL.Base.Models.Reports;
using GUIStd.DAL.AllNewPY.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vABM08 出勤刷卡異常資料控制器
    /// </summary>
    [Route("py/private/[controller]")]
    public class vABM08Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 出勤刷卡異常資料商業邏輯物件屬性
        /// </summary>
        private BlABM08 BlABM08 => mBlABM08 = mBlABM08 ?? new BlABM08(this.ClientContent);
        private BlABM08 mBlABM08;

        /// <summary>
        /// 公司別商業邏輯物件屬性
        /// </summary>
        private BlA01 BlA01 => new BlA01(ClientContent);

        /// <summary>
        /// 年度考勤核准設定商業邏輯物件屬性
        /// </summary>
        private BlAnnualAttAppr BlAnnualAttAppr => mBlAnnualAttAppr = mBlAnnualAttAppr ?? new BlAnnualAttAppr(this.ClientContent);
        private BlAnnualAttAppr mBlAnnualAttAppr;

        #endregion

        #region " 共用屬性 "

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.PY;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 vABM08 頁面輔助資料（公司別、考勤等），與 vPYM03 路徑一致
        /// </summary>
        /// <returns>UI 輔助資料模型</returns>
        [HttpGet("query/uidata")]
        public MdABM08_h GetHelp()
        {
            // 1. 公司別 - Controller 呼叫 BlA01
            var _companyOptions = BlA01.GetHelp(false, false, false, "PY", true);

            // 2. 所有考勤別選項 - 不排除任何類別（用於設定對話框，並作為已勾選清單來源）
            var _allAttendanceOptions = BlAnnualAttAppr.GetAttendanceOptionsWithDeduction(null);

            // 3. 從 BLL 層取得未提供勞務原因清單
            var _reasonList = BlABM08.GetReasonList();

            // 4. 從 BLL 層取得已保存的考勤代碼設定（用於預先勾選對話框中的項目）
            var _savedAttendanceCodes = BlABM08.GetSavedAttendanceCodes();
            var _savedCodeSet = new HashSet<string>((_savedAttendanceCodes ?? new List<string>()).Select(x => x?.Trim() ?? ""));

            // 5. 判定輸入/考勤 dialog：回傳「設定已勾選」的考勤選項（保留扣抵代碼、單位等完整欄位）
            var _selectedAttendanceOptions = (_allAttendanceOptions ?? Enumerable.Empty<MdAttendCode>())
                .Where(x => x != null && _savedCodeSet.Contains((x.AB03 ?? "").Trim()))
                .ToList();

            return new MdABM08_h()
            {
                CompanyOptions = _companyOptions,
                SelectedAttendanceOptions = _selectedAttendanceOptions,
                // attendType = AB01 考勤類別（供設定對話框剔除 M 等類別）
                AllAttendanceOptions = _allAttendanceOptions?.Select(x => new { id = x.AB03, name = x.AB05, attendType = x.AB01 }).ToList(),
                ReasonList = _reasonList,
                SavedAttendanceCodes = _savedAttendanceCodes
            };
        }

        /// <summary>
        /// 取得 vABM08 查詢資料（分頁），與 vPYM03 路徑一致
        /// 自動應用已保存的考勤代碼設定
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="rowsPerPage">每頁筆數（0 表示使用 query 或 SINI 預設）</param>
        /// <returns>出勤刷卡異常資料分頁結果</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdABM08_p GetData([FromBody] MdABM08_q query, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] int rowsPerPage = 0)
        {
            if (rowsPerPage > 0)
            {
                query.RowsPerPage = rowsPerPage;
            }

            return BlABM08.GetData(query, ControlName, pageNo);
        }

        #endregion

        #region " 共用函式 - 報表查詢 "

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件（包含 basic 和 query）</param>
        /// <param name="reason">未提供勞務原因</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdABM08_q> obj, [FromQuery] string reason = "")
        {
            // 建立報表（含儲存未提供勞務原因清單，由 BLL 處理）
            var _report = await BlABM08.GetReport(obj, reason);

            // 回傳報表檔案
            if (_report.Contents != null)
                return HttpContext.Response.SendFile(_report.Contents, _report.FileName);

            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_report.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_report.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }

        #endregion
    }
}

