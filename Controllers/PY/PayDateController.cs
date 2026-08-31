#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Attributes;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.Extensions;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models;

#endregion

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

        /// <summary>
        /// 取得分頁查詢資料（發薪期別列表）
        /// </summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdPayDate_p GetPayPeriodQueryData([RequiredFromQuery] string companyId,
          [FromQuery] string payDateStart, [FromQuery] string payDateEnd, [RequiredFromQuery] int rowsPerPage, [DARange(1, int.MaxValue)] int pageNo)
        {
            var _rowsPerPage = rowsPerPage;
            return BlPG.GetPayPeriodQueryData(companyId, payDateStart, payDateEnd, ControlName, pageNo, ref _rowsPerPage);
        }

        /// <summary>
        /// 主鍵是否已存在
        /// </summary>
        [HttpGet("exists/{companyId}/{payYear}/{payMonth}/{payDay}")]
        public bool IsExist(string companyId, string payYear, string payMonth, string payDay)
        {
            return BlPG.IsExist(companyId, payYear, payMonth, payDay);
        }

        /// <summary>
        /// 取得單筆發薪期別明細
        /// </summary>
        [HttpGet("detail/{companyId}/{payYear}/{payMonth}/{payDay}")]
        public MdPayDate_d GetRow(string companyId, string payYear, string payMonth, string payDay)
        {
            return BlPG.GetRow(companyId, payYear, payMonth, payDay);
        }

        /// <summary>
        /// 該發薪日是否已有 PB 薪資資料
        /// </summary>
        [HttpGet("payroll-used/{companyId}/{payYear}/{payMonth}/{payDay}")]
        public bool IsPayrollUsed(string companyId, string payYear, string payMonth, string payDay)
        {
            return BlPG.IsPayrollUsed(companyId, payYear, payMonth, payDay);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增發薪期別
        /// </summary>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdPayDate_d body)
        {
            try
            {
                var _result = BlPG.ProcessInsert(body);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改發薪期別（路徑為編輯前主鍵）
        /// </summary>
        [HttpPut("{companyId}/{payYear}/{payMonth}/{payDay}")]
        public MdApiMessage Update(string companyId, string payYear, string payMonth, string payDay, [FromBody] MdPayDate_d body)
        {
            if (body == null)
            {
                return HttpContext.Response.UpdateFailed(new ArgumentNullException(nameof(body)));
            }

            if (!companyId.EqualsIgnoreCase(body.CompanyId))
            {
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            }

            try
            {
                var _result = BlPG.ProcessUpdate(companyId, payYear, payMonth, payDay, body);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除發薪期別
        /// </summary>
        [HttpDelete("{companyId}/{payYear}/{payMonth}/{payDay}")]
        public MdApiMessage Delete(string companyId, string payYear, string payMonth, string payDay)
        {
            try
            {
                var _result = BlPG.ProcessDelete(companyId, payYear, payMonth, payDay);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

    }
}
