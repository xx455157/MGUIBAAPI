#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models.Private.CompanyOrgStruct;
using GUIStd.Attributes;
using GUIStd.DAL.AllNewPY.Models.Private.PYR09;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.BasicPayment;

#endregion

namespace MGUIBAAPI.Controllers.PY.Payment
{
    /// <summary>
    /// 基本薪資資料控制器
    /// </summary>
    [Route("py/payment/[controller]")]
    public class BasicPaymentController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPA BlPA => new BlPA(ClientContent);

        private BlBasicPayment BlBasicPayment => mBlBasicPayment = mBlBasicPayment ?? new BlBasicPayment(ClientContent);
        private BlBasicPayment mBlBasicPayment;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢 組織 基本薪資分析 資料
        /// vPYR09
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <returns>組織 基本薪資分析結果</returns>
        [HttpPost("query/organization")]
        public IEnumerable<MdBasicPaymentOrganization> GetOrganizationData([FromBody] MdPYR09_q query)
        {
            return BlBasicPayment.GetOrganizationData(query);
        }

        /// <summary>
        /// 查詢 性別 基本薪資分析 資料
        /// vPYR09
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <returns>性別 基本薪資分析結果</returns>
        [HttpPost("query/gender")]
        public IEnumerable<MdBasicPaymentGender> GetGenderData([FromBody] MdPYR09_q query)
        {
            return BlBasicPayment.GetGenderData(query);
        }

        /// <summary>
        /// 查詢 職等、職務 基本薪資分析 資料
        /// vPYR09
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <returns>職等、職務 基本薪資分析結果</returns>
        [HttpPost("query/joblevel")]
        public IEnumerable<MdBasicPaymentJobLevel> GetJobLevelData([FromBody] MdPYR09_q query)
        {
            return BlBasicPayment.GetJobLevelData(query);
        }

        /// <summary>
        /// 查詢 年資分群 基本薪資分析 資料
        /// vPYR09
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <returns>年資分群 基本薪資分析結果</returns>
        [HttpPost("query/seniority")]
        public IEnumerable<MdBasicPaymentSeniority> GetSeniorityData([FromBody] MdPYR09_q query)
        {
            return BlBasicPayment.GetSeniorityData(query);
        }

        /// <summary>
        /// 查詢 年齡分群 基本薪資分析 資料
        /// vPYR09
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <returns>年資分群 基本薪資分析結果</returns>
        [HttpPost("query/age")]
        public IEnumerable<MdBasicPaymentAge> GetAgeData([FromBody] MdPYR09_q query)
        {
            return BlBasicPayment.GetAgeData(query);
        }

        /// <summary>
        /// 查詢 部門 基本薪資 資料
        /// </summary>
        /// <param name="pageNo">頁碼（最小值 1）</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("query/deptbasic/pages/{pageNo}")]
        public MdDeptBasicPaymnet_p GetDeptBasicPaymnet([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdDeptBasicPaymnet_q query, int rowsPerPage=0)
        {
            return BlBasicPayment.GetDeptBasicPaymnet(query, ControlName, pageNo, rowsPerPage);
        }

        #endregion
    }
}
