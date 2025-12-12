#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models.Private.Bookings;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;
using GUIStd.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Reservations
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htlpre/reservations/[controller]")]
	public class BookingsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTRV BlHTRV => new BlHTRV(ClientContent);
        private BlBookings BlBookings => new BlBookings(ClientContent);
        private BlRoomsAnalysis BlRoomsAnalysis => new BlRoomsAnalysis(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得飯店可用房型
        /// </summary>
        /// <returns></returns>
        [HttpGet("roomTypes")]
        public object GetRoomTypes()
        {
            return null;
        }

        /// <summary>
        /// 使用訂單號碼查詢訂單
        /// </summary>
        /// <param name="rvno">訂單號碼</param>
        /// <returns></returns>
        [HttpGet("{rvno}")]
        public MdReservation GetReservation(string rvno)
        {
            return BlBookings.GetReservation(rvno);
        }

        /// <summary>
        /// 取得該日期的訂單資料
        /// </summary>   
        /// <param name="checkInDate">到達日期</param>
        /// <returns></returns>
        [HttpGet("query/{checkInDate}")]
        public IEnumerable<MdReservations> GetReservations(string checkInDate)
        {
            return BlBookings.GetReservations(checkInDate);
        }

        /// <summary>
        /// 查詢房價選項
        /// </summary>
        /// <param name="startDate">入住日期</param>
        /// <param name="endDate">退房日期</param>
        /// <param name="rvType">散客或團客</param>
        /// <param name="roomTypes">房型</param>
        /// <returns></returns>
        [HttpPost("query/rates/{startDate}/{endDate}/{rvType}")]
        public object GetRoomRates(string startDate, string endDate, string rvType, [FromBody] string[] roomTypes = null)
        {
            return null;
        }

        /// <summary>
        /// 取得日期範圍的房型銷售預測數量
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="buildingsId">館別</param>
        /// <returns>日期範圍房型銷售預測數量模型物件集合</returns>
        [HttpPost("query/forecast/{startDate}/{endDate}")]
        public IEnumerable<MdRoomsQty> GetDataForRoomSalesQuantity(string startDate, string endDate, [FromBody] string[] buildingsId)
        {
            return BlRoomsAnalysis.GetDataForRoomSalesQuantity(startDate, endDate, buildingsId);
        }

        /// <summary>
        /// 取得日期範圍的房型庫存預測數量
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="buildingsId">館別</param>
        /// <returns>日期範圍房型庫存預測數量模型物件集合</returns>
        [HttpPost("query/salesInventory/{startDate}/{endDate}")]
        public IEnumerable<MdRoomsQty> GetDataForRoomSalesInventory(string startDate, string endDate, [FromBody] string[] buildingsId)
        {
            return BlRoomsAnalysis.GetDataForRoomSalesInventory(startDate, endDate, buildingsId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 訂房資料寫入
        /// </summary>
        /// <param name="obj">訂房資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
		public MdApiMessage Insert([FromBody] MdBookings obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlHTRV.ProcessInsert(obj, ClientContent, out string _rvNo);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result, responseData: _rvNo);
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
        }

        /// <summary>
        /// 訂房資料更新
        /// </summary>
        /// <param name="rvno">訂房資料模型物件</param>
        /// <param name="obj">訂房資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{rvno}")]
        public MdApiMessage Update(string rvno, [FromBody] MdBookings obj)
        {
            // 檢查鍵值路徑參數與本文中的鍵值是否相同
            if (!rvno.EqualsIgnoreCase(obj.RV01))
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            }
            try
            {
                // 呼叫商業元件執行修改作業
                var _result = BlHTRV.ProcessUpdate(rvno, obj, ClientContent);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
