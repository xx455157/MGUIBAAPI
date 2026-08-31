#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PYS01;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// vPYS01 系統設定控制器（Private；模組清單）
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYS01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPYS01 BlMain => mBlMain = mBlMain ?? new BlPYS01(ClientContent);
        private BlPYS01 mBlMain;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 vPYS01 主頁／查詢條件輔助資料（模組下拉清單，SINI vPYS01_Modules）
        /// </summary>
        /// <returns>頁面輔助資料模型物件</returns>
        [HttpGet("page")]
        public MdPYS01_h GetUIData() => BlMain.GetUIData();

        #endregion
    }
}
