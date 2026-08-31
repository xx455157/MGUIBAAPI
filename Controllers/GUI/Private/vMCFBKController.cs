#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCFBK;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vMCFBK 銀行基本資料維護 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vMCFBKController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>BlMCFBK 商業邏輯物件屬性</summary>
        private BlMCFBK BlvMCFBK => mvMCFBK = mvMCFBK ?? new BlMCFBK(ClientContent);
        private BlMCFBK mvMCFBK;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>取得查詢資料（分頁）</summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdMCFBK_l_p GetQueryList([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdMCFBK_q queryParams, int rowsPerPage = 0)
        {
            return BlvMCFBK.GetQueryList(queryParams, ControlName, pageNo, rowsPerPage);
        }

        /// <summary>取得單筆明細資料</summary>
        [HttpGet("query/detail/{bankCode}")]
        public MdBank_d GetDetailData(string bankCode)
        {
            return BlvMCFBK.GetDetailData(bankCode);
        }

        #endregion
    }
}
