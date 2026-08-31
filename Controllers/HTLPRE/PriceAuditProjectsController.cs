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
    /// 房價稽核表 - 專案輔助資料控制器
    /// </summary>
    [Route("htlpre/reports/priceaudit/projects")]
    public class PriceAuditProjectsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPriceAuditReport BlPriceAuditReport => new BlPriceAuditReport(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 房價稽核表 - 依關鍵字模糊搜尋啟用中的專案（HTPG.PG14='Y'）
        /// </summary>
        /// <param name="keyword">搜尋關鍵字（可模糊搜 PG01/PG02）</param>
        /// <returns>專案清單</returns>
        [HttpGet]
        public IEnumerable<MdPriceAuditProject> Search([FromQuery] string keyword)
        {
            return BlPriceAuditReport.SearchProjects(keyword);
        }

        #endregion
    }
}
