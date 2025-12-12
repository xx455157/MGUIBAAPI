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
using GUIStd.Models;
using GUICore.Web.Attributes;
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 敦謙自助報到機 - 房間狀態控制器
	/// 提供房間狀態變更功能
	/// </summary>
	[Route("general/DonQianKiosk/")]
	public class RoomStatusController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子：初始化房間狀態控制器
		/// </summary>
		public RoomStatusController()
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
		/// 變更房間狀態
		/// 更新訂單的房間狀態（如已入住、已退房等）
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="order_number">訂單號碼或住宿碼（必填）</param>
		/// <param name="status">變更後的狀態值（必填）</param>
		/// <returns>成功時回傳系統規範訊息物件，失敗時回傳錯誤訊息物件</returns>
		[HttpPost("room_status")] 
		public MdApiMessage UpdateRoomStatus(string domain, [RequiredFromQuery] string order_number, [RequiredFromQuery] string status)
		{
			try
			{
				// 呼叫商業邏輯層更新房間狀態
				int _result = BlKiosk.UpdateRoomStatus(domain, order_number, status);

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
