#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.HTL.Private;
using GUIStd.DAL.HTL.Models.Private.vRRM01;

#endregion

namespace MGUIBAAPI.Controllers.NetHTL
{
    /// <summary>
    /// vRRM01 請修單登錄程式資料控制器
    /// </summary>
    [Route("nethtl/private/[controller]")]
    public class vRRM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlRepairRequests BlRepairRequests => new BlRepairRequests(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得vRRM01請修單登錄畫面所需的資料
        /// </summary>
        [HttpGet("page")]
        public MdRRM01_h GetUIData()
        {
            return BlRepairRequests.GetRRM01UiData();
        }

        #endregion
    }
}
