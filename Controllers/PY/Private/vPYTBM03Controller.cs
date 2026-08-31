#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PYTBM03;
using GUICore.Web.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// vPYTBM03 發薪期別設定（畫面 UI 初始化）
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYTBM03Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPYTBM03 BlPYTBM03 => mBlPYTBM03 = mBlPYTBM03 ?? new BlPYTBM03(ClientContent);
        private BlPYTBM03 mBlPYTBM03;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得查詢頁 UI 初始化資料（公司別、開關帳）
        /// </summary>
        [HttpGet("query/uidata")]
        public MdPYTBM03_h GetUIData()
        {
            return BlPYTBM03.GetUIData();
        }

        #endregion
    }
}
