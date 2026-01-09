#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "


using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using System.Collections.Generic;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;
using GUIStd.DAL.AllNewHTL.Models.Private.HTL.vHTRGM09;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// 報表資料控制器
    /// </summary>
    [Route("htlpre/[controller]")]
    public class ReportsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRoomsAnalysis BlRoomsAnalysis => new BlRoomsAnalysis(ClientContent);

        /// <summary>
        /// 帳單明細表商業邏輯物件屬性
        /// </summary>
        private BlAccountDetailReport BlAccountDetailReport => new BlAccountDetailReport(ClientContent);

        /// <summary>
        /// 科目彙總表商業邏輯物件屬性
        /// </summary>
        private BlAccountSummaryReport BlAccountSummaryReport => new BlAccountSummaryReport(ClientContent);

        /// <summary>
        /// 發票明細表商業邏輯物件屬性
        /// </summary>
        private BlInvoiceDetailReport BlInvoiceDetailReport => new BlInvoiceDetailReport(ClientContent);

        /// <summary>
        /// 空房庫存預測表商業邏輯物件屬性
        /// </summary>
        private BlVacantForecastReport BlVacantForecastReport => new BlVacantForecastReport(ClientContent);

        /// <summary>
        /// 住房率統計表商業邏輯物件屬性
        /// </summary>
        private BlOccupancyStatsReport BlOccupancyStatsReport => new BlOccupancyStatsReport(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得平面圖畫面資料
        /// </summary>
        /// <returns>平面圖資料集合</returns>
        [HttpGet("FloorPlan")]
        public IEnumerable<MdFloorPlan> GetFloorPlan()
        {
            return BlRoomsAnalysis.GetFloorPlanData();

        }

        /// <summary>
        /// 查詢帳單明細表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>帳單明細表資料集合</returns>
        [HttpPost("AccountDetail")]
        public IEnumerable<MdAccountDetailReport> AccountDetail([FromBody] MdAccountDetailReportQuery queryParams)
        {
            return BlAccountDetailReport.Query(queryParams);
        }

        /// <summary>
        /// 查詢科目彙總表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>科目彙總表資料集合</returns>
        [HttpPost("AccountSummary")]
        public IEnumerable<MdAccountSummaryReport> AccountSummary([FromBody] MdAccountDetailReportQuery queryParams)
        {
            return BlAccountSummaryReport.Query(queryParams);
        }

        /// <summary>
        /// 查詢發票明細表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>發票明細表資料集合</returns>
        [HttpPost("InvoiceDetail")]
        public IEnumerable<MdInvoiceDetailReport> InvoiceDetail([FromBody] MdInvoiceDetailReportQuery queryParams)
        {
            return BlInvoiceDetailReport.Query(queryParams);
        }

        /// <summary>
        /// 查詢空房庫存預測表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>空房庫存預測表資料集合</returns>
        [HttpPost("VacantForecast")]
        public IEnumerable<MdVacantForecastReport> VacantForecast([FromBody] MdVacantForecastReportQuery queryParams)
        {
            return BlVacantForecastReport.Query(queryParams);
        }

        /// <summary>
        /// 查詢住房率統計表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>住房率統計表資料集合</returns>
        [HttpPost("OccupancyStats")]
        public IEnumerable<MdOccupancyStatsReport> OccupancyStats([FromBody] MdOccupancyStatsReportQuery queryParams)
        {
            return BlOccupancyStatsReport.Query(queryParams);
        }

        #endregion
    }
}
