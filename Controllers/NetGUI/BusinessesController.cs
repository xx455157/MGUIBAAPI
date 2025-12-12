#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.GUI;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 【需經驗證】客戶類別資料控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class BusinessesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA23 BlA23 => new BlA23(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得客戶類別資料集合
        /// </summary>
        /// <param name="type">資料類別</param>
        /// <returns>代碼資料模型物件集合</returns>
        [HttpGet("help/{type}")]
		public IEnumerable<MdCode> GetHelp(string type)
		{
			return BlA23.GetHelp(type);
		}

		#endregion
	}
}
