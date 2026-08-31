#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models.Private;
using GUIStd.Models;
using GUICore.Web.Attributes;
using GUIStd.DAL.AllNewHTL.Models;
using System;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 【需經驗證】敦謙自助報到機 - 付款資料控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class PaymentController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
		/// </summary>
		public PaymentController()
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

		// 此控制器無查詢功能

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 建立付款資料
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="booking_number">訂單號碼或住宿碼（必填）</param>
		/// <param name="obj">付款資料物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost]
		public MdApiMessage Insert(string domain,[RequiredFromQuery] string booking_number,[FromBody] MdKioskPayment obj)
		{
			// 驗證必填欄位：payment_id 不可為 0
			if (obj.payment_id == 0) return null;

			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlKiosk.InsertPayment(domain, booking_number, obj);

				// 回應前端新增成功訊息
				return HttpContext.Response.InsertSuccess(_result);
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
