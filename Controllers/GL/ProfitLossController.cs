#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.DAL.AllNewGL.Models;
using GUIStd.DAL.AllNewGL.Models.Private;
using GUIStd.Models;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewGL.Models.Private.ProfitLoss;

#endregion

namespace MGUIBAAPI.Controllers.GL
{
    /// <summary>
    /// 排班資料控制器
    /// </summary>
    [Route("gl/[controller]")]
    public class ProfitLossController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlProfitLoss BlProfitLoss => new BlProfitLoss(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢 集團損益表
        /// </summary>
        /// <param name="obj">查詢參數</param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<MdCompaniesReport> GetData([FromBody] MdQueryCompanies obj)
        {
            return BlProfitLoss.GetDataCompanies(obj);
        }

        #endregion
    }
}
