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
	/// 程式資料控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class SystemsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA12 BlA12 => new BlA12(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得系統別輔助資料
        /// </summary>
        /// <returns>系統別資料代碼模型集合物件</returns>
        [HttpGet("help")]
		public IEnumerable<MdCode> GetHelp()
		{
			return BlA12.GetHelp();
		}

		#endregion
	}
}
