#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private.CompanyOrgStruct;
using GUIStd.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 員工資料控制器
    /// </summary>
    [Route("py/[controller]")]
    public class EmployeesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPA BlPA => new BlPA(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得員工基本資料
        /// </summary> 
        /// <param name="employeeId">員工編號</param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        public MdEmployee GetRow(string employeeId)
        {
            return BlPA.GetRowById(employeeId);
        }

        /// <summary>
        /// 取得帶有職稱的員工基本資料
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <returns></returns>
        [HttpGet("{employeeId}/includePosition")]
        public MdEmployee GetRowWithPosition(string employeeId)
        {
            return BlPA.GetRowByIdWithPosition(employeeId);
        }


        /// <summary>
        /// 取得輔助資料
        /// </summary>
        /// <param name="compId">體系代碼</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>員工資料模型泛型集合物件</returns>	
        [HttpGet("help")]
		public IEnumerable<MdEmployee> GetHelp([FromQuery] string compId,
			[FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
		{
			return BlPA.GetEmployeesByCompId(compId, CurrentLang, includeEmptyRow, includeId);
        }

        /// <summary>
        /// 取得管轄的部門
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [HttpGet("incharge/{managerId}")]
        public IEnumerable<MdCompanyOrgStruct> GetInChargedDepartment(string managerId)
        {
            return null;
        }

        /// <summary>
        /// 取得分頁員工資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="payrollDate">發薪日期 (YYYYMMDD)</param>
        /// <param name="queryText">查詢文字 (員工姓名、編號、身分證字號) - 可選</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns>分頁員工資料模型物件</returns>
        [HttpPost("query/{companyId}/{payrollDate}/pages/{pageNo}")]
        public MdEmployee_p GetEmployeeData(
            string companyId,
            string payrollDate,
            [FromQuery] string queryText,
            [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlPA.GetEmployeeData(companyId, payrollDate, queryText, ControlName, pageNo);
        }

        #endregion
    }
}
