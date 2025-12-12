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
	/// 【需經驗證】基本代碼資料控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class BasicCodesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA41 BlA41 => new BlA41(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得類別下的所有基本代碼資料集合
        /// </summary>
        /// <param name="category">類別路徑參數</param>
        /// <returns>代碼資料模型物件集合</returns>
        [HttpGet("{category}")]
		public IEnumerable<MdCode> GetHelp(string category)
		{
			return BlA41.GetHelp(category);
		}

		#endregion
	}
}
