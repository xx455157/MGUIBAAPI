#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewAS.Models.Private.vASP07;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASP07財產費用分攤系統程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASP07Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlApportionment BlApportionment => new BlApportionment(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        [HttpGet("paged")]
        public MdASP07_h GetUIData()
        {
            return BlApportionment.GetASP07_UIData();
        }


        #endregion

    }
}
