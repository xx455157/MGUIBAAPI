#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PYP53;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// vPYP53 所得人補充保費計算（畫面 UI 初始化）
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYP53Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPYP53 BlPYP53 => mBlPYP53 = mBlPYP53 ?? new BlPYP53(this.ClientContent);
        private BlPYP53 mBlPYP53;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得查詢頁 UI 初始化資料（公司別、計算類別等）
        /// </summary>
        /// <returns>UI 初始化資料模型物件</returns>
        [HttpGet("help")]
        public MdPYP53_h GetUIData()
        {
            return BlPYP53.GetUIData();
        }

        #endregion
    }
}
