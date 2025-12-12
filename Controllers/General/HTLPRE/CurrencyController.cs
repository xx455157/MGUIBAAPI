#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.BLL.AllNewHTL.Private;

#endregion

namespace MGUIBAAPI.Controllers.General.HTLPRE
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("general/htlpre/[controller]")]
	public class CurrencyController : GUIAppAnonAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCurrency BlCurrency => new BlCurrency(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "
        [HttpGet("exchangerate")]
		public IEnumerable<MdCurrency> GetData()
		{
			return BlCurrency.GetData();
		}
		#endregion
	}
}
