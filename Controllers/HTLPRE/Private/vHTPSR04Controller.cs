#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTPSR04;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vHTPSR04 發票明細表程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTPSR04Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlHTPSR04 BlHTPSR04 => new BlHTPSR04(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得會計及會計科目代碼資料
        /// </summary>        
        [HttpGet("page")]
        public MdHTPSR04 GetUIData()
        {
            return BlHTPSR04.GetUIData();
        }

        #endregion

    }
}
