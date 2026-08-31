#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.ABTBM02;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vABTBM02 私用控制器（明細／篩選輔助資料，比照 vPYTBM02）
    /// </summary>
    [Route("py/private/vabtbm02")]
    public class vABTBM02Controller : GUIAppAuthController
    {
        private BlABTBM02 BlMain => mBlMain = mBlMain ?? new BlABTBM02(ClientContent);
        private BlABTBM02 mBlMain;

        /// <summary>
        /// 取得假別下拉等輔助資料（AB01=I）
        /// </summary>
        [HttpGet("paged")]
        public MdABTBM02_h GetUIDataForDetail([FromQuery] bool isStateAdd) =>
            BlMain.GetUIDataForDetail(isStateAdd);
    }
}
