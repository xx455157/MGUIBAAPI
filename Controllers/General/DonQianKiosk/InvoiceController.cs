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
	/// 【需經驗證】敦謙自助報到機 - 發票管理控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class InvoiceController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
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
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="number">訂單號碼或住宿碼（必填）</param>
		/// <param name="invoice_type">發票類型代碼（必填）</param>
		/// <returns>發票資料集合</returns>
		[HttpGet("info")]
		public System.Collections.Generic.IEnumerable<MdKioskGetinvoice> GetRow(
			string domain,
			[RequiredFromQuery] string number,
			[RequiredFromQuery] int invoice_type)
		{
			return BlKiosk.GetInvoice(domain, number, invoice_type);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新增發票資料
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="booking_number">訂單號碼或住宿碼（選填）</param>
		/// <param name="obj">發票資料物件</param>
		/// <returns>發票資料集合</returns>
		[HttpPost]
		public System.Collections.Generic.IEnumerable<MdKioskGetinvoice> Insert(
			string domain,
			string booking_number,
			[FromBody] MdKioskHTVC_Insert obj)
		{
			// 驗證必填欄位：amount 不可為空或 "0"
			if (obj.amount == "" || obj.amount == "0") return null;

			return BlKiosk.InsertInvoice(domain, booking_number, obj);
		}

		#endregion
	}
}
