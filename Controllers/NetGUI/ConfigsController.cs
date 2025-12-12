#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.GUI;
using GUIStd.DAL.GUI.Models;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 系統參數控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class ConfigsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlSINI BlSINI => new BlSINI(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得系統參數資料
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="includeLang">是否需要Language條件</param>
        /// <param name="topics">Topic 多種條件(有%的話會用模糊查詢)</param>
        /// <returns>系統別資料代碼模型集合物件</returns>
        [HttpPost("multiTopic/{section}")]
		public IEnumerable<MdCode> GetHelp(string section, [FromQuery] bool includeLang, [FromBody] string[] topics)
		{
			return BlSINI.GetRows(section, topics, (includeLang ? CurrentUILang : ""));
		}

		#endregion
	}
}
