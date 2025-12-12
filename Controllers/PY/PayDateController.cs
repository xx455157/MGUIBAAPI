using Microsoft.AspNetCore.Mvc;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using System;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Share.PG;

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// PY系統發薪日期控制器
    /// </summary>
    [ApiController]
    [Route("py/[controller]")]
    public class PayDateController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPG BlPG => mBlPG = mBlPG ?? new BlPG(this.ClientContent);
        private BlPG mBlPG;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁發薪日期資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="payrollDate">發薪日期 (YYYYMMDD) - 可選</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns>分頁發薪日期資料模型物件</returns>
        [HttpPost("query/{companyId}/pages/{pageNo}")]
        public MdPG_p GetData(
            string companyId,
            [FromQuery] string payrollDate,
            [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlPG.GetData(companyId, payrollDate, ControlName, pageNo);
        }

        #endregion
    }
}
