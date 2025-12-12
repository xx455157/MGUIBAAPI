#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "
using GUICore.Web.Attributes;
using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUICore.Web.Extensions;
using System;
using GUIStd.DAL.AllNewHTL.Models.Private;
using GUICore.Web.Filters;
using GUIStd.Models;
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
    /// <summary>
    /// 敦謙自助報到機 - 訂單管理控制器
    /// 提供訂單查詢、新建訂單等功能
    /// </summary>
    [Route("general/DonQianKiosk/[controller]")]
    public class BookingController : GUIAppWSController
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化訂單控制器
        /// </summary>
        public BookingController()
        {
            // 改變執行服務的使用者帳號為 KIOSK
            this.WSUser = "KIOSK";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlKiosk BlKiosk => new BlKiosk(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢訂單資料
        /// 根據不同的查詢類型（網路訂單號碼、電話、EMAIL、住宿碼、房號）查詢訂單
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="booking_type">查詢類型代碼
        ///     1: 網路訂單號碼
        ///     2: 電話號碼
        ///     3: EMAIL
        ///     4: 住宿碼
        ///     5: 房號
        /// </param>
        /// <param name="start_date">入住日期（選填）</param>
        /// <param name="booking_number">查詢號碼（必填）- 根據 booking_type 可能是訂單號、電話、EMAIL 等</param>
        /// <returns>符合條件的訂單資料集合</returns>
        [HttpGet()]
        public IEnumerable<MdKioskBooking> GetBooking
            (string domain, [RequiredFromQuery] string booking_type, string start_date, [RequiredFromQuery] string booking_number)
        {
            // 呼叫商業邏輯層查詢訂單資料
            return BlKiosk.GetBooking(domain, booking_type, start_date, booking_number);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新建訂單
        /// 旅客透過自助報到機建立新的訂房訂單
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="obj">旅客訂房資料物件，必須包含 email 或 phone 其中之一</param>
        /// <returns>新建立的訂單資料集合，若 email 和 phone 皆為空則回傳 null</returns>
        /// <remarks>
        /// 注意：必須提供 email 或 phone 其中至少一項，否則回傳 null
        /// </remarks>
        [HttpPost(), WSAuthActionFilter]
        public IEnumerable<MdKioskBooking> PostBooking(string domain, [FromBody] MdKioskPostBooking obj)
        {
            // 驗證必填欄位：email 或 phone 至少需要一個
            if (obj.email == "" && obj.phone == "") return null;

            // 呼叫商業邏輯層建立訂單
            return BlKiosk.PostBooing(domain, obj);
        }

        #endregion
    }
}
