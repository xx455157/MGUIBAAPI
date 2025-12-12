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

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 代碼資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class CodesController : GUIAppAuthController
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

		[HttpGet("")]
		public IEnumerable<MdCodeHT> GetData([FromQuery]string typeIds, [FromQuery] bool includeId)
		{
			return BlCodes.GetData(typeIds.Split(','), CurrentLang, includeId);
		}

		/// <summary>
		/// 取得國家代碼資料
		/// </summary>		
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("country")]
		public IEnumerable<MdCode> GetDataForCountry([FromQuery] bool includeId)
		{
			return BlCodes.GetDataForCountry(CurrentLang, includeId);
		}

		/// <summary>
		/// 取得城市代碼資料
		/// </summary>		
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("city")]
		public IEnumerable<MdCode> GetDataForCity([FromQuery] bool includeId)
		{
			return BlCodes.GetDataForCity(CurrentLang, includeId);
		}

		#endregion
	}
}
