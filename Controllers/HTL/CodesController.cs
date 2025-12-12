#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.HT2020;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTL
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htl/[controller]")]
	public class CodesController : GUIAppAuthController
	{
		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得體系資料
		/// </summary>
		/// <param name="typeId">TB01代號</param>
		/// <param name="hotelId">館別代號</param>
		/// <param name="includeEmptyRow">是否包含空白列</param>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("{typeId}")]
		public IEnumerable<MdCode> GetHelp(string typeId, [FromQuery] string hotelId,
			[FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
		{
			return new BlCodes(ClientContent).GetHelp(typeId, CurrentLang, hotelId, includeEmptyRow, includeId);
		}

		#endregion
	}
}
