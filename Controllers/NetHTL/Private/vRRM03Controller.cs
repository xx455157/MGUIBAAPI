#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.HTL.Private;
using GUIStd.DAL.HTL.Models.Private.vRRM02;

#endregion

namespace MGUIBAAPI.Controllers.NetHTL
{
    /// <summary>
    /// vRRM03程式資料控制器
    /// </summary>
    [Route("nethtl/private/[controller]")]
	public class vRRM03Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRepairRequests BlRepairRequests => new BlRepairRequests(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得vRRM03畫面所需的資料
        /// </summary>
        [HttpGet("page")]
        public MdRRM02_h GetUIData()
        {
            return BlRepairRequests.GetRRM02UiData();
        }


        #endregion

        #region " 共用函式 - 報表查詢 "



        #endregion

    }
}
