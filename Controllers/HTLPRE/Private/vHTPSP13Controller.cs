#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using MGUIBAAPI.Models.HTLPRE;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTPSP13;
using GUICore.Web.Attributes;
using GUIStd.BLL.PY.Private;
using GUICore.Web.Extensions;
using GUIStd.DAL.PY.Models.Private.OvertimeApplication;
using GUIStd.Models;
using System;
using GUIStd.BLL.GUI;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    ///  vQRCodeLangs程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTPSP13Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPOSMenu BlPOSMenu => new BlPOSMenu(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得vHTPSP13畫面輔助資料
        /// </summary>
        [HttpGet("page")]
        public MdHTPSP13_h GetUIData() => BlPOSMenu.GetUIData<MdHTPSP13_h>();


        /// <summary>
        /// 取得vHTPSP13畫面輔助資料-指定廳別
        /// </summary>
        /// <param name="PosId">廳別代碼</param>
        [HttpGet("pagep")]
        public MdHTPSP13_h2 GetUIDataByPos([RequiredFromQuery]string PosId) => BlPOSMenu.GetUIData<MdHTPSP13_h2>(PosId);

        #endregion

        #region " 共用函式 - 異動資料 "


        #endregion

    }
}
