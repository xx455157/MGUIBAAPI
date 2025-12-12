#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 會計日期資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class BkDateController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlBkDate BlBkDate => new BlBkDate(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得會計日期資料
		/// </summary>
		/// <param name="typeId">類型代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("{typeId}")]
		public MdBKDate GetHelp(string typeId)
		{
			return BlBkDate.GetHelp(typeId);
		}

		#endregion
	}
}
