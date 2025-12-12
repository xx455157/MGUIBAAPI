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
    /// 出納科目資料控制器
    /// </summary>
    [Route("htl/[controller]")]
	public class AccountsController : GUIAppAuthController
    {
        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得會計科目代碼資料
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <param name="hotelId">館別代號</param>
        /// <param name="state">科目範圍</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>代碼資料模型泛型集合物件</returns>
        [HttpGet("{posId}")]
		public IEnumerable<MdCode> GetHelp(string posId, [FromQuery] string hotelId, [FromQuery] string state,
            [FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
		{
			return new BlHTCA(ClientContent).GetHelp(posId, CurrentLang, hotelId, state, includeEmptyRow, includeId);
		}

		#endregion
	}
}
