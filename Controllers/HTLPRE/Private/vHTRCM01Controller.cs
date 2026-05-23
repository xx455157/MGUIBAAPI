#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTRCM01;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    ///  vHTRCM01程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTRCM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlHouseKeeping BlHouseKeeping => new BlHouseKeeping(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得vHTRCM01畫面輔助資料
        /// </summary>
        [HttpGet("page")]
        public MdHTRCM01_h GetUIData() => BlHouseKeeping.GetUIDataRCM01();



        #endregion

    }
}
