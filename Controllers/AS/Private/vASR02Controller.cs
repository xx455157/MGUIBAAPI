#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASR02;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR02固定資產清單統程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASR02Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlAsset BlAsset => new BlAsset(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public MdASR02_h GetUIData()
        {
            return BlAsset.GetASR02_dUIData();
        }

        #endregion

    }
}
