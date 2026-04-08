#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.Models;
using GUICore.Web.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 【需經驗證】敦謙自助報到機 - 車輛管理控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class CarController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
		/// </summary>
		public CarController()
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
		/// 登記車牌號碼
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="order_number">訂單號碼或住宿碼（必填）</param>
		/// <param name="car_number">車牌號碼（必填）</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost]
		public MdApiMessage Insert(
			string domain,
			[RequiredFromQuery] string order_number,
			[RequiredFromQuery] string car_number)
		{
			try
			{
				// 呼叫商業元件執行登記作業
				int _result = BlKiosk.InsertHPCar(domain, order_number, car_number);

				// 回應前端登記成功訊息
				return HttpContext.Response.InsertSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端登記失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		#endregion
	}
}
