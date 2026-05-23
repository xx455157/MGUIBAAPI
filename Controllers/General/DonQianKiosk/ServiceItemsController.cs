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

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 【需經驗證】敦謙自助報到機 - 服務項目控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class ServiceItemsController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
		/// </summary>
		public ServiceItemsController()
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
		/// 取得服務項目清單
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="order_number">訂單號碼或住宿碼（必填）</param>
		/// <returns>服務項目資料集合</returns>
		[HttpGet]
		public System.Collections.Generic.IEnumerable<MdKioskService> GetRow(
			string domain,
			[RequiredFromQuery] string order_number)
		{
			return BlKiosk.GetService(domain, order_number);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		// 此控制器無異動功能

		#endregion
	}
}
