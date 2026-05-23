#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.GUI;
using GUIStd.DAL.Base.Models;
using GUIStd.DAL.GUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.Pattern
{
    /// <summary>
    /// 【需經驗證】PTN群組程式授權基本資料控制器
    /// </summary>
    [Route("pattern/[controller]")]
    public class AuthProgramsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPTNA07 BlA07PTN => new BlPTNA07(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/pages/{pageNo}")]
        public MdAuthProgramCodes_p GetSHelpv2([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText, 
            [FromQuery] bool sortByName)
        {
            return BlA07PTN.GetSHelpv2(new MdHelpPaging
            {
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo
            });
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
