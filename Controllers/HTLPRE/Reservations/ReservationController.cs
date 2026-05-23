#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Reservation;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Reservation
{
    /// <summary>
    /// 訂房資料控制器
    /// </summary>
    [Route("htlpre/reservations/[controller]")]
    public class ReservationController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlReservation BlReservation => new BlReservation(ClientContent);

        #endregion

        #region " 共用函式 - 取得訂房資料 "
       
        /// <summary>
        /// 取得分頁頁次的訂房清單
        /// </summary>
        /// <param name="checkInDate">入住日期</param>
        /// <param name="rvName">訂房名稱</param>
        /// <param name="mobileNo">手機號碼</param>
        /// <param name="eMail">電子郵件</param>
        /// <param name="rvNo">訂房號碼</param>
        /// <param name="contractCompany">合約公司</param>        
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns>分頁訂房資料模型物件</returns>
        [HttpGet("reservationdata/pages/{pageNo}")]
        public MdReservationData_p GetData([DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0,
            [FromQuery] string checkInDate = "", [FromQuery] string rvName = "", [FromQuery] string mobileNo = "",
            [FromQuery] string eMail = "", [FromQuery] string rvNo = "", [FromQuery] string contractCompany = "")
        {
            return BlReservation.GetDataByPage(RV03: checkInDate, RV11: rvName, CN09: mobileNo,
                CN10: eMail, RV01: rvNo, RV10: contractCompany, funcName: ControlName,
                pageNo: pageNo, rowsPerPage: ref rowsPerPage);
        }

        /// <summary>
        /// 取得日期範圍的房型庫存資料
        /// </summary>
        /// <param name="dates">起始日期</param>
        /// <param name="datee">結束日期</param>
        /// <param name="roomTypes">房型字串陣列</param>
        /// <returns>房型庫存資料模型物件</returns>
        [HttpPost("query/roominventory/{dates}/{datee}")]
        public IEnumerable<MdRoomInventory> GetDataForRoomInventory(string dates, string datee, [FromBody] string[] roomTypes)
        {
            return BlReservation.GetDataForRoomInventory(dates, datee, roomTypes);
        }

        /// <summary>
        /// 取得日期範圍的房型價格資料
        /// </summary>
        /// <param name="dates">起始日期</param>
        /// <param name="datee">結束日期</param>
        /// <param name="companyId">合約公司</param>
        /// <param name="roomTypes">房型字串陣列</param>
        /// <returns>房型價格資料模型物件</returns>
        [HttpPost("query/roomprice")]
        public MdRoomPriceOption GetDataForRoomPrice([FromQuery] string dates, [FromQuery] string datee, [FromQuery] string companyId, [FromBody] string[] roomTypes)
        {
            return BlReservation.GetDataForRoomPrice(dates, datee, companyId, roomTypes);
        }

        #endregion               

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增訂單作業
        /// </summary>        
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdReservation_w obj)
        {
            try
            {
                // 呼叫商業元件執行入住作業
                int _result = BlReservation.ProcessInsert(obj, out string _rvNo);

                // 回應前端新增成功訊息                
                return HttpContext.Response.SendSuccess(
                    string.Format(Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_BookingSuccessMsg"), _rvNo), responseData: _rvNo);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息                
                return HttpContext.Response.InsertFailed(ex);
            }
        }
        
        #endregion        

    }
}
