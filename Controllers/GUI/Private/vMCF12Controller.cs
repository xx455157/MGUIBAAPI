#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCF12;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vMCF12 輔助資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vMCF12Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// vMCF12 商業邏輯物件屬性
        /// </summary>
        private BlMCF12 BlMCF12 => mBlMCF12 = mBlMCF12 ?? new BlMCF12(ClientContent);
        private BlMCF12 mBlMCF12;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 vMCF12 頁面所需的輔助資料
        /// 包含：公司別、系統別、員工(有效)、職稱(有效)
        /// </summary>
        /// <returns>vMCF12 輔助資料模型物件</returns>
        [HttpGet("page")]
        public MdMCF12 GetUIData()
        {
            return BlMCF12.GetUIData();
        }

        #endregion
    }
}

