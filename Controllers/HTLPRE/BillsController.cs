#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using System.Collections.Generic;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 帳務資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class BillsController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlCodes BlCodes => new BlCodes(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得帳夾配置資料
		/// </summary>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("Folders")]
		public IEnumerable<MdCode> GetFolders([FromQuery] bool includeId)
		{
			return BlCodes.GetHelp("FO", CurrentLang, false, includeId);
		}

		#endregion
	}
}
