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
	/// 敦謙自助報到機 - 發票管理控制器
	/// 提供發票查詢及新增功能
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class InvoiceController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子：初始化發票管理控制器
		/// </summary>
		public InvoiceController()
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
		/// 查詢發票資料
		/// 根據訂單號碼或住宿碼查詢發票資訊
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="number">訂單號碼或住宿碼（必填）</param>
		/// <param name="invoice_type">發票類型代碼（必填）</param>
		/// <returns>發票資料集合</returns>
		[HttpGet("info")]
		public IEnumerable<MdKioskGetinvoice> GetRow(string domain, [RequiredFromQuery] string number, [RequiredFromQuery] int invoice_type)
		{
			// 呼叫商業邏輯層查詢發票資料
			return BlKiosk.GetInvoice(domain, number, invoice_type);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新增發票資料
		/// 旅客透過自助報到機建立發票資料（電子發票或紙本發票）
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="booking_number">訂單號碼或住宿碼（選填）</param>
		/// <param name="odj">發票資料物件，必須包含 amount 且不可為空或 0</param>
		/// <returns>成功時回傳發票資料集合，若 amount 為空或 0 則回傳 null</returns>
		/// <remarks>
		/// 注意：amount 為空字串或 "0" 時回傳 null
		/// </remarks>
		[HttpPost(), WSAuthActionFilter] 
		public IEnumerable<MdKioskGetinvoice> Insert(string domain, string booking_number, [FromBody] MdKioskHTVC_Insert odj)
		{
			// 驗證必填欄位：amount 不可為空或 "0"
			if (odj.amount == "" || odj.amount == "0") return null;

			// 呼叫商業邏輯層新增發票資料
			return BlKiosk.InsertInvoice(domain, booking_number, odj);
		}

		#endregion
	}
}
