#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Rooms;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// 合約公司程式資料控制器
    /// </summary>
    [Route("htlpre/[controller]")]
    public class CompaniesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCompany BlCompany => new BlCompany(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得合約公司基本資料
        /// </summary>
        /// <param name="companyId">合約公司代碼</param>
        /// <returns></returns>
        [HttpGet("{companyId}")]
        public object GetRow(string companyId)
        {
            return BlCompany.GetRow(companyId);
        }

        /// <summary>
        /// 依照業務碼分類取得合約公司清單
        /// </summary>
        /// <param name="source">業務員</param>
		/// <param name="pageNo">頁碼</param>
        /// <returns></returns>
        [HttpGet("{source}/{pageNo}")]
        public MdCompany_p GetDataForCompanySource(string source, int pageNo)
        {
            return BlCompany.GetDataForCompanySource(source, pageNo, "vHTATM01");
        }

        ///// <summary>
        ///// 使用多個條件模糊查詢合約公司，並取得清單(目前找不到使用地方先暫停使用，待需要時再做調整)
        ///// </summary>
        ///// <param name="company">合約公司資料模型</param>
        ///// <param name="pageNo"></param>
        ///// <returns></returns>
        //[HttpPost("query/{pageNo}")]
        //public MdCompany_p GetData([FromBody] MdCompany_r company, int pageNo)
        //{

        //    return new BlCompany().GetData(company, pageNo, "vHTATM01");
        //}

        /// <summary>
        /// 取得全部合約公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MdCompany_r> GetData2()
        {
            return BlCompany.GetData2();
        }
        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
