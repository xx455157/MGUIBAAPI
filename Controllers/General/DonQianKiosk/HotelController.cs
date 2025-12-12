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
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 敦謙自助報到機 - 飯店資訊控制器
	/// 提供飯店基本資料查詢功能
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class HotelController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子：初始化飯店資訊控制器
		/// </summary>
		public HotelController()
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
		/// 取得飯店基本資料
		/// 提供自助報到機顯示飯店名稱、地址、聯絡方式等資訊
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <returns>飯店基本資料物件</returns>
		[HttpGet()]
		public MdKioskHotel GetRow(string domain)
		{
			// 呼叫商業邏輯層查詢飯店基本資料
			return BlKiosk.GetHotel();
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		// 此控制器無異動功能

		#endregion
	}
}
