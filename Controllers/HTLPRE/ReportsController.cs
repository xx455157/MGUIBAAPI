#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;
using GUIStd.DAL.AllNewHTL.Models.Private.HTL.vHTRGM09;
using MGUIBAAPI.Models.HTLPRE.vHTRGM09;
using System;
using System.Linq;
using System.Collections.Generic;

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

        /// <summary>
        /// 查詢房價稽核表資料
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>房價稽核表資料集合</returns>
        [HttpPost("PriceAudit")]
        public IEnumerable<MdPriceAuditReport> PriceAudit([FromBody] MdPriceAuditReportQuery queryParams)
        {
            return BlPriceAuditReport.Query(queryParams);
        }

        /// <summary>
        /// 查詢住客應收帳款資料
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <returns>住客應收帳款資料集合</returns>
        [HttpGet("GuestReceivable/{roomNo}")]
        public IEnumerable<MdGuestReceivable> GetGuestReceivable(string roomNo)
        {
            return BlGuestReceivable.GetData(roomNo);
        }

        /// <summary>
        /// 取得訂單詳情
        /// </summary>
        /// <param name="rvId">旅客編號（GR01）</param>
        /// <returns>訂單詳情資料模型</returns>
        [HttpGet("rooms/{grId}/orderDetail")]
        public MdOrderDetail GetOrderDetail(string grId)
        {
            try
            {
                // 取得平面圖資料
                var floorPlanData = BlRoomsAnalysis.GetFloorPlanData();
                
                // 從平面圖資料中找到對應的旅客
                var guest = floorPlanData?.FirstOrDefault(x => x.GR01 == grId);

                if (guest == null)
                {
                    return new MdOrderDetail();
                }

                // 格式化日期
                string FormatDate(string date)
                {
                    if (string.IsNullOrEmpty(date) || date.Length < 8)
                        return date ?? "";
                    return $"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)}";
                }

                // 組裝回傳資料
                return new MdOrderDetail
                {
                    // 客戶資訊
                    GuestName = guest.GR03 ?? "",
                    Phone = guest.CN09 ?? "",
                    ReservationNo = guest.RV01 ?? grId,
                    GroupName = guest.GroupName ?? "",
                    Nationality = guest.GR08 ?? "",
                    Email = guest.CN10 ?? "",

                    // 住宿資訊
                    CheckInDate = FormatDate(guest.GR05),
                    CheckOutDate = FormatDate(guest.GR06),
                    RoomType = guest.RoomType ?? guest.GR07 ?? "",
                    RoomNo = guest.VS06 ?? "",

                    // 訂單資訊
                    Source = guest.RV14 ?? "",
                    IsDepositPaid = guest.RV17 == "Y",
                    Sales = guest.RV15 ?? "",

                    // 費用資訊
                    TotalAmount = guest.TotalAmount
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReportsController] GetOrderDetail Error: {ex.Message}");
                return new MdOrderDetail();
            }
        }

        #endregion
    }
}
