#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUICore.Web.Extensions;
using System;
using GUIStd.DAL.AllNewHTL.Models.Private;
using GUICore.Web.Filters;
using GUICore.Web.Attributes;
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 敦謙自助報到機 - 服務項目控制器
	/// 提供飯店服務項目查詢功能
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class ServiceItemsController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子：初始化服務項目控制器
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
		/// 查詢指定訂單可使用的飯店服務項目（如早餐、接駁、洗衣等）
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="order_number">訂單號碼或住宿碼（必填）</param>
		/// <returns>服務項目資料集合</returns>
		[HttpGet()]
		public IEnumerable<MdKioskService> GetRow(string domain, [RequiredFromQuery] string order_number)
		{
			// 呼叫商業邏輯層查詢服務項目清單
			return BlKiosk.GetService(domain, order_number);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		// 此控制器無異動功能

		#endregion
	}
}
