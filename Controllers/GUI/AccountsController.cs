#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.Base.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 會計科目資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class AccountsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA15 BlA15 => new BlA15(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/pages/{pageNo}/{companyId}")]
        public MdCode_p GetSHelpv2([DARange(1, int.MaxValue)] int pageNo, string companyId, [FromQuery] string queryText,
            [FromQuery] bool sortByName)
        {
            return BlA15.GetSHelpv2(companyId, new MdHelpPaging
            {
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo
            });
        }

        #endregion

        #region " 共用屬性 - 異動資料"


        #endregion
    }
}
