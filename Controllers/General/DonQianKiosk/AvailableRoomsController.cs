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
	/// 【需經驗證】敦謙自助報到機 - 可訂房型控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class AvailableRoomsController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
		/// </summary>
		public AvailableRoomsController()
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
		/// 取得可訂房型及住宿價格
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="start_date">入住日期（必填），格式：YYYYMMDD</param>
		/// <param name="end_date">退房日期（必填），格式：YYYYMMDD</param>
		/// <param name="isDUS">
		///     查詢類型代碼（必填）
		///     1：一般住宿查詢
		///     2：DUS（Day Use Service，鐘點房）查詢
		/// </param>
		/// <returns>可訂房型及價格資料集合</returns>
		[HttpGet("available_rooms")]
		public System.Collections.Generic.IEnumerable<MdKioskAvailableRooms> GetRow(
			string domain,
			[RequiredFromQuery] string start_date,
			[RequiredFromQuery] string end_date,
			[RequiredFromQuery] string isDUS)
		{
			return BlKiosk.GetAvailableRooms(domain, start_date, end_date, isDUS);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		// 此控制器無異動功能

		#endregion
	}
}
