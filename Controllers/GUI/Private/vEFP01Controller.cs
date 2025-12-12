#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
//using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vEFP01;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vEFP01電子表單系統程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
	public class vEFP01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlEform BlEform => new BlEform(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public MdEFP01 GetUIData()
        {
            return BlEform.GetEFormsAuthEdit();
        }

        #endregion

    }
}
