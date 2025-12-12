#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.General.Pattern
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("general/pattern/[controller]")]
	public class CodesController : GUIAppAnonAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlCodes BlCodes => new BlCodes(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得代碼資料
		/// </summary>
		/// <param name="typeId">TB01代號</param>
		/// <param name="includeEmptyRow">是否包含空白列</param>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("{typeId}")]
		public IEnumerable<MdCode> GetHelp(string typeId,
			[FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
		{
			return BlCodes.GetHelp(typeId, CurrentLang, includeEmptyRow, includeId);
		}

		#endregion
	}
}
