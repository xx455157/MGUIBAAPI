#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private.CompanyOrgStruct;

#endregion

namespace MGUIBAAPI.Controllers.PY
{

    /// <summary>
    /// 部門層級資料控制器
    /// </summary>
    [Route("py/[controller]")]
    public class CompanyOrgStructController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCompanyOrgStruct BlCompanyOrgStruct => new BlCompanyOrgStruct(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "
        /// <summary>
		/// 取得最頂層部門
		/// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IEnumerable<MdCompanyOrgStruct> GetHelp()
        {
            return BlCompanyOrgStruct.GetCompanyOrgStruct("");
        }

        /// <summary>
		/// 取得該部門的下層部門
		/// </summary>
        /// <param name="departmentId">部門別</param>
        /// <returns></returns>
        [HttpGet("{departmentId}")]
        public IEnumerable<MdCompanyOrgStruct> GetData(string departmentId)
        {
            return BlCompanyOrgStruct.GetCompanyOrgStruct(departmentId);
        }

        /// <summary>
		/// 取得歸屬部門的員工資料
		/// </summary>
        /// <param name="departmentId">部門別</param>
        /// <returns>員工資料模型(含職位資訊)</returns>
        [HttpGet("employees")]
        public IEnumerable<MdEmployee> GetSubDeptEmployees(string departmentId)
        {
            return BlCompanyOrgStruct.GetSubDeptEmployees(departmentId);
        }
        #endregion
    }
}
