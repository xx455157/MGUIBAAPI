#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 配置資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class ConfigController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlCodes BlCodes => new BlCodes(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得房型配置資料
		/// </summary>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("RoomTypes")]
		public IEnumerable<MdCode> GetRoomTypes([FromQuery] bool includeId)
		{
			return BlCodes.GetHelp("RT", CurrentLang, false, includeId);
		}

		/// <summary>
		/// 取得樓層配置資料
		/// </summary>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("Floors")]
		public IEnumerable<MdCode> GetFloors([FromQuery] bool includeId)
		{
			return BlCodes.GetHelp("FL", CurrentLang, false, includeId);
		}

		/// <summary>
		/// 取得班別配置資料
		/// </summary>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("Shifts")]
		public IEnumerable<MdCode> GetShifts([FromQuery] bool includeId)
		{
			return BlCodes.GetHelp("SH", CurrentLang, false, includeId);
		}

		#endregion
	}
}
