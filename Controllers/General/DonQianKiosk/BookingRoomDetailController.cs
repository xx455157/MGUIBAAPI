#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models.Private;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 【需經驗證】敦謙自助報到機 - 訂房房間明細控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class BookingRoomDetailController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
		/// </summary>
		public BookingRoomDetailController()
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
		/// 更新訂單房間入住資訊
		/// </summary>
		/// <param name="obj">訂房房間明細資料物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut]
		public MdApiMessage Update([FromBody] MdBookingRoomDetail obj)
		{
			try
			{
				// 呼叫商業元件執行更新作業
				int _result = BlKiosk.BookingRoomDetail(obj);

				// 回應前端更新成功訊息
				return HttpContext.Response.InsertSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端更新失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		#endregion
	}
}
