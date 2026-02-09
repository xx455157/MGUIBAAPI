#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;

using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCF01;
using GUIStd.BLL.AllNewGUI.Private;


#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vMCF01 公司基本資料管理 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
	public class vMCF01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// BlMCF01 商業邏輯物件屬性
        /// </summary>
        private BlMCF01 BlMCF01 => mBlMCF01 = mBlMCF01 ?? new BlMCF01(ClientContent);
        private BlMCF01 mBlMCF01;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// S140828010
        /// </summary>
        /// <returns></returns>
        [HttpGet("paged")]
        public MdMCF01d_h GetUIData()
        {
            return BlMCF01.GetMCF01_dUIData();
        }

        #endregion

    }
}
