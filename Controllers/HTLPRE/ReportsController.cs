#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
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

        /// <summary>
        /// 房價稽核表商業邏輯物件屬性
        /// </summary>
        private BlPriceAuditReport BlPriceAuditReport => new BlPriceAuditReport(ClientContent);

        /// <summary>
        /// 住客應收帳款商業邏輯物件屬性
        /// </summary>
        private BlGuestReceivable BlGuestReceivable => new BlGuestReceivable(ClientContent);

        /// <summary>
        /// 訂單詳情商業邏輯物件屬性
        /// </summary>
        private BlOrderDetail BlOrderDetail => new BlOrderDetail(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得平面圖畫面資料
        /// </summary>
        /// <returns>平面圖資料集合</returns>
        [HttpGet("FloorPlan")]
        public IEnumerable<MdFloorPlan> GetFloorPlan()
        {
            Logging.Logger.LogDebug("[vHTRGM09] FloorPlan API 呼叫開始");

            try
            {
                var result = BlRoomsAnalysis.GetFloorPlanData();
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] FloorPlan API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] FloorPlan API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢帳單明細表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>帳單明細表資料集合</returns>
        [HttpPost("AccountDetail")]
        public IEnumerable<MdAccountDetailReport> AccountDetail([FromBody] MdAccountDetailReportQuery queryParams)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] AccountDetail API 呼叫開始，參數：{@queryParams}");

            try
            {
                var result = BlAccountDetailReport.Query(queryParams);
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] AccountDetail API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] AccountDetail API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢科目彙總表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>科目彙總表資料集合</returns>
        [HttpPost("AccountSummary")]
        public IEnumerable<MdAccountSummaryReport> AccountSummary([FromBody] MdAccountDetailReportQuery queryParams)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] AccountSummary API 呼叫開始，參數：{@queryParams}");

            try
            {
                var result = BlAccountSummaryReport.Query(queryParams);
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] AccountSummary API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] AccountSummary API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢發票明細表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>發票明細表資料集合</returns>
        [HttpPost("InvoiceDetail")]
        public IEnumerable<MdInvoiceDetailReport> InvoiceDetail([FromBody] MdInvoiceDetailReportQuery queryParams)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] InvoiceDetail API 呼叫開始，參數：{@queryParams}");

            try
            {
                var result = BlInvoiceDetailReport.Query(queryParams);
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] InvoiceDetail API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] InvoiceDetail API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢空房庫存預測表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>空房庫存預測表資料集合</returns>
        [HttpPost("VacantForecast")]
        public IEnumerable<MdVacantForecastReport> VacantForecast([FromBody] MdVacantForecastReportQuery queryParams)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] VacantForecast API 呼叫開始，參數：{@queryParams}");

            try
            {
                var result = BlVacantForecastReport.Query(queryParams);
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] VacantForecast API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] VacantForecast API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢住房率統計表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>住房率統計表資料集合</returns>
        [HttpPost("OccupancyStats")]
        public IEnumerable<MdOccupancyStatsReport> OccupancyStats([FromBody] MdOccupancyStatsReportQuery queryParams)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] OccupancyStats API 呼叫開始，參數：{@queryParams}");

            try
            {
                var result = BlOccupancyStatsReport.Query(queryParams);
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] OccupancyStats API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] OccupancyStats API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢房價稽核表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>房價稽核表資料集合</returns>
        [HttpPost("PriceAudit")]
        public IEnumerable<MdPriceAuditReport> PriceAudit([FromBody] MdPriceAuditReportQuery queryParams)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] PriceAudit API 呼叫開始，參數：{@queryParams}");

            try
            {
                var result = BlPriceAuditReport.Query(queryParams);
                var count = result != null ? result.Count() : 0;
                Logging.Logger.LogDebug($"[vHTRGM09] PriceAudit API 完成，共 {count} 筆資料");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] PriceAudit API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 查詢住客應收帳款資料
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <returns>住客應收帳款資料集合</returns>
        //[HttpGet("GuestReceivable/{roomNo}")]
        //public IEnumerable<MdGuestReceivable> GetGuestReceivable(string roomNo)
        //{
        //    return BlGuestReceivable.GetData(roomNo);
        //}

        /// <summary>
        /// 取得訂單詳情
        /// </summary>
        /// <param name="visitNo">住宿號碼（VS24）</param>
        /// <returns>訂單詳情資料模型</returns>
        [HttpGet("rooms/orderDetail/{visitNo}")]
        public MdOrderDetail GetOrderDetail(string visitNo)
        {
            Logging.Logger.LogDebug($"[vHTRGM09] GetOrderDetail API 呼叫開始，visitNo：{visitNo}");

            try
            {
                var result = BlOrderDetail.GetOrderDetail(visitNo);
                Logging.Logger.LogDebug($"[vHTRGM09] GetOrderDetail API 完成，visitNo：{visitNo}");
                return result;
            }
            catch (Exception ex)
            {
                Logging.Logger.LogError($"[vHTRGM09] GetOrderDetail API 發生錯誤：{ex.Message}", ex);
                throw;
            }
        }

        #endregion
    }
}
