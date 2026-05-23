#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models.Private;
using GUIStd.Models;
using GUICore.Web.Attributes;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 【需經驗證】敦謙自助報到機 - 訂單管理控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class BookingController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
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
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="booking_type">查詢類型代碼
		///     1：網路訂單號碼 2：電話號碼 3：EMAIL 4：住宿碼 5：房號
		/// </param>
		/// <param name="start_date">入住日期（選填）</param>
		/// <param name="booking_number">查詢號碼（必填）</param>
		/// <returns>符合條件的訂單資料集合</returns>
		[HttpGet]
		public System.Collections.Generic.IEnumerable<MdKioskBooking> GetBooking(
			string domain,
			[RequiredFromQuery] string booking_type,
			string start_date,
			[RequiredFromQuery] string booking_number)
		{
			return BlKiosk.GetBooking(domain, booking_type, start_date, booking_number);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新建訂單
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="obj">旅客訂房資料物件</param>
		/// <returns>新建立的訂單資料集合</returns>
		[HttpPost]
		public System.Collections.Generic.IEnumerable<MdKioskBooking> PostBooking(
			string domain,
			[FromBody] MdKioskPostBooking obj)
		{
			// 驗證必填欄位：email 或 phone 至少需要一個
			if (obj.email == "" && obj.phone == "") return null;

			return BlKiosk.PostBooing(domain, obj);
		}

		#endregion
	}
}
