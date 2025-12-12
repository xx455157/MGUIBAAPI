#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCF10;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vMCF10 輔助資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vMCF10Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// vMCF10 商業邏輯物件屬性
        /// </summary>
        private BlMCF10 BlMCF10 => mBlMCF10 = mBlMCF10 ?? new BlMCF10(ClientContent);
        private BlMCF10 mBlMCF10;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 vMCF10 頁面所需的輔助資料
        /// </summary>
        /// <returns>vMCF10 輔助資料模型物件</returns>
        [HttpGet("page")]
        public MdMCF10 GetUIData()
        {
            return BlMCF10.GetUIData();
        }

        #endregion
    }
}

