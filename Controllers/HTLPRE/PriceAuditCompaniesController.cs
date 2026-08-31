#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.HTL.vHTRGM09;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// 房價稽核表 - 合約公司輔助資料控制器
    /// </summary>
    [Route("htlpre/reports/priceaudit/companies")]
    public class PriceAuditCompaniesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPriceAuditReport BlPriceAuditReport => new BlPriceAuditReport(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 房價稽核表 - 依關鍵字模糊搜尋合約公司（HTGR.GR02='O' AND GR18='GIT'）
        /// </summary>
        /// <param name="keyword">搜尋關鍵字（可模糊搜 GR01/GR03/GR22）</param>
        /// <returns>合約公司清單</returns>
        [HttpGet]
        public IEnumerable<MdPriceAuditCompany> Search([FromQuery] string keyword)
        {
            return BlPriceAuditReport.SearchCompanies(keyword);
        }

        #endregion
    }
}
