#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private.CompanyOrgStruct;
using GUIStd.DAL.Base.Models;

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
        /// 檢核員工是否已存在於薪資系統 PA 表（PA06=員工編號）；供 vMCF08 刪除前判斷（有 PA 時僅刪授權群組不刪 A08）。
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <returns>true 表示 PA 表已有此員工</returns>
        [HttpGet("exists/employeeid/{employeeId}")]
        public bool IsExistByPA06(string employeeId)
        {
            return BlPA.IsExistByPA06(employeeId ?? "");
        }

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

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">編號或名稱必需包含傳入的參數值（可選，模糊比對 PA06/PA03/PA04）</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/pages/{pageNo}")]
        public MdEmployee_p GetHelpPaging(
            [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] string queryText = null,
            [FromQuery] bool sortByName = false)
        {
            return BlPA.GetHelpPaging(queryText ?? string.Empty, ControlName, pageNo, sortByName);
        }

        /// <summary>
        /// 判斷身分證字號是否已存在（供 vMCP10 身分證字號變更檢核，比照 GUI EmployeesController IsExist）
        /// </summary>
        /// <param name="socialId">身分證字號路徑參數</param>
        /// <returns>已存在為 true，否則為 false</returns>
        [HttpGet("exists/{socialId}")]
        public bool IsExist(string socialId)
        {
            return BlPA.IsExist(socialId ?? "");
        }

        /// <summary>
        /// 取得身分證字號分頁輔助資料（vMCP10 身分證字號變更用，比照 GUI EmployeesController GetSHelpv2）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件（Codes: Id=身分證字號, Name=姓名）</returns>
        [HttpGet("helpv2/pages/{pageNo}")]
        public MdCode_p GetSHelpv2(
            [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] string queryText,
            [FromQuery] bool sortByName)
        {
            return BlPA.GetSHelpv2(new MdHelpPaging
            {
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo
            });
        }

        #endregion
    }
}
