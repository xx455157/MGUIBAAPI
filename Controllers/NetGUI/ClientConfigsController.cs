#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.GUI;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 客戶系統參數控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class ClientConfigsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCINI BlCINI => new BlCINI(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得系統參數資料
        /// </summary>
        /// <param name="csection">CSection</param>
        /// <param name="ctopic">CTopic</param>
        /// <param name="comp">Comp</param>
        /// <param name="clanguage">CLanguage</param>
        /// <returns>系統別資料代碼模型集合物件</returns>
        [HttpGet("{csection}")]
		public string GetData(string csection, string ctopic, string comp, string clanguage)
		{
			return BlCINI.GetData(comp, csection, ctopic, clanguage);
		}

		#endregion
	}
}
