#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.GUI;
using GUIStd.DAL.GUI.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 自訂 Menu 資料控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class CustomMenusController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlP01 BlP01 => new BlP01(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "


		#endregion

		#region " 共用函式 - 使用交易異動資料 "

		/// <summary>
		/// 處理員工自訂 Menu 存檔作業
		/// </summary>
		/// <param name="employeeId">員工編號</param>
		/// <param name="obj">員工資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost("{employeeId}")]
		public MdApiMessage ProcessSaveForPersonal(string employeeId, [FromBody] IEnumerable<MdCustomMenu> obj)
		{
			employeeId = employeeId.ToUpper();

			try
			{
				// 呼叫商業元件執行存檔作業
				int _result = BlP01.ProcessSaveTransForPerson(employeeId, obj);

				// 回應前端存檔成功訊息
				return HttpContext.Response.SendSuccess(
					Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK"));
			}
			catch (Exception ex)
			{
				// 回應前端存檔失敗訊息
				return HttpContext.Response.SendFailed(
					Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveFail"), ex);
			}
		}

		#endregion
	}
}
