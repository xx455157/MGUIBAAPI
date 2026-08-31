#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.DAL.Base.Models;
using GUIStd.BLL.AllNewIV;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.IV
{
	/// <summary>
	/// 產品資料控制器
	/// </summary>
	[Route("iv/[controller]")]
	public class ProductsController : GUIAppAuthController
	{
		private BlB10 BlB10 => new BlB10(this.ClientContent);

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 取得分頁頁次的輔助資料(搜尋字眼非必要)
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="companyId">公司別</param>
        /// <param name="queryText">編號或名稱的參數值</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/pages/{companyId}/{pageNo}")]
        public MdCode_p GetSHelp2([DARange(1, int.MaxValue)] int pageNo, string companyId, [FromQuery] string queryText,
            [FromQuery] bool sortByName)
        {
            return BlB10.GetSHelpv2(new MdHelpPaging
            {
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo
            }, companyId);
        }

        #endregion

    }
}
