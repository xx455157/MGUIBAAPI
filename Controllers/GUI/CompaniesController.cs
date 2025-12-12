#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;


#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class CompaniesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA01 BlA01 => new BlA01(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得公司別輔助資料
        /// </summary>
        /// <param name="fullName">是否顯示公司全名,否的話顯示簡稱</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <param name="authorized">是否檢核授權</param>
        /// <param name="system">系統別 GL、IV、PY、AS</param>
        /// <returns>公司別資料代碼模型集合物件</returns>
        [HttpGet("help")]
        public IEnumerable<MdCode> GetHelp([FromQuery] bool fullName = false, [FromQuery] bool includeEmptyRow = false, [FromQuery] bool includeId = false,
             [FromQuery] bool authorized = false, [FromQuery] string system = "")
        {
            return BlA01.GetHelp(fullName, includeEmptyRow, includeId, system, authorized);
        }

        #endregion
    }
}
