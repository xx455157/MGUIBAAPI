#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCP10;
using GUIStd.DAL.Base.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 【需經驗證】員工基本資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class EmployeesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA08 BlA08 => new BlA08(ClientContent);
        private BlA06 BlA06 => new BlA06(ClientContent);
        private BlA62 BlA62 => new BlA62(ClientContent);
        private BlSINI BlSINI => new BlSINI(ClientContent);

        #endregion

        #region " 私用函式 "

        private string[] UniqIgnoreCase(string[] inputs)
        {
            Dictionary<string, string> _rets = new Dictionary<string, string>();
            foreach (var _str in inputs)
            {
                if (string.IsNullOrEmpty(_str))
                    continue;
                if (!_rets.Keys.Contains(_str.ToUpper()))
                    _rets.Add(_str.ToUpper(), Common.Cvr2String(_str));
            }
            return _rets.Values.ToArray();
        }

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的員工基本資料（ARTHGUI A08：員工編號 A0801、姓名 A0802、使用者帳號 A0826、到職日期 A0805、離職日期 A0806）
        /// </summary>
        /// <param name="employeeIds">員工編號(起)，選填</param>
        /// <param name="employeeIde">員工編號(迄)，選填</param>
        /// <param name="employeeName">員工姓名</param>
        /// <param name="userId">使用者代碼</param>
        /// <param name="onboardDates">到職日期(起)</param>
        /// <param name="onboardDatee">到職日期(迄)</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數，0 時由後端依 SINI 取得</param>
        /// <param name="departments">部門字串陣列</param>
        /// <returns>分頁員工資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdUser_p GetData(string employeeIds, string employeeIde, string employeeName, string userId,
            string onboardDates, string onboardDatee,
            [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] departments, int rowsPerPage = 0)
        {
            return BlA08.GetData(employeeIds ?? "", employeeIde ?? "", employeeName, userId, onboardDates, onboardDatee,
                departments ?? new string[0], ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 取得所有群組清單（A06 群組代號、群組名稱，供明細勾選用）
        /// </summary>
        /// <returns>群組代碼集合（id=群組代號, name=群組名稱）</returns>
        [HttpGet("groups")]
        public IEnumerable<MdCode> GetGroups()
        {
            return BlA06.GetHelp(false, true);
        }

        /// <summary>
        /// 取得密碼輸入政策（安控層級最小/最大長度、是否僅數字；供 vMCF08 前端檢核）
        /// </summary>
        [HttpGet("passwordpolicy")]
        public MdPasswordPolicy GetPasswordPolicy()
        {
            return BlA08.GetPasswordPolicy();
        }

        /// <summary>
        /// 判斷員工編號是否已存在（供新增時檢核）
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <returns>已存在為 true，否則為 false</returns>
        [HttpGet("exists/{employeeId}")]
        public bool IsExist(string employeeId)
        {
            return BlA08.IsExist(employeeId ?? "");
        }

        /// <summary>
        /// 取得唯一的員工資料（ARTHGUI A08：員工編號 A0801、姓名 A0802、英文姓名 A0803、使用者帳號 A0826、到職日期 A0805、離職日期 A0806、身分證字號 A0809、公司別 A0824）
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <returns>員工單筆資料模型物件</returns>
        [HttpGet("{employeeId}")]
        public MdUser GetRow(string employeeId)
        {
            return BlA08.GetRow(employeeId);
        }

        /// <summary>
        /// 取得指定員工所屬群組代號清單（A62）
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <returns>群組代號陣列</returns>
        [HttpGet("{employeeId}/groups")]
        public IEnumerable<string> GetEmployeeGroups(string employeeId)
        {
            return BlA62.GetGroupIdsByEmployee(employeeId ?? "") ?? new List<string>();
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/pages/{pageNo}")]
        public MdCode_p GetSHelpv2([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName)
        {
            return BlA08.GetSHelpv2(new MdHelpPaging
            {
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo
            });
        }

        /// <summary>
        /// 取得有效使用者帳號的員工資料
        /// </summary>
        /// <param name="pageNo">查詢頁次（若小於等於 0 表示取得全部資料）</param>
        /// <param name="queryText">編號或名稱必需包含傳入的參數值（可選，模糊比對 A0801/A0802/A0803）</param>
        /// <param name="deptId">部門代號（可選，精確比對 A0804）</param>
        /// <param name="jobTitle">職務名稱（可選，精確比對 A0823）</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpuser/pages/{pageNo}")]
        public MdUser_p GetSHelp_User([DARange(0, int.MaxValue)] int pageNo,
            [FromQuery] string queryText = "",
            [FromQuery] string deptId = "",
            [FromQuery] string jobTitle = "")
        {
            return BlA08.GetSHelp_User(queryText, ControlName, pageNo, deptId, jobTitle);
        }


        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">編號或名稱必需包含傳入的參數值</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] bool sortByName)
        {
            return BlA08.GetSHelp(queryText, ControlName, pageNo, sortByName);
        }

		/// <summary>
		/// 取得分頁頁次的輔助資料
		/// </summary>
		/// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
		/// <param name="pageNo">查詢頁次</param>
		/// <param name="query">其他條件</param>
		/// <returns>分頁輔助資料模型物件</returns>
		[HttpPost("shelp2/pages/{pageNo}")]
        public MdEmpDept_p GetSHelp2([FromQuery] string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromBody] MdEmpSHelp2_q query)
        {
            query.ExcepteEmps = UniqIgnoreCase(query.ExcepteEmps);
            return BlA08.GetSHelp2(queryText, ControlName, pageNo, query);
        }

        /// <summary>
        /// 取得授權公司別員工清單
        /// </summary>
        /// <param name="systemId">AS/GL/IV/PY/RQ</param>
        /// <param name="companyId">公司別</param>
        /// <returns>授權公司別員工清單</returns>
        [HttpGet("query/{systemId}/{companyId}")]
        public IEnumerable<MdEmpDept> GetCompanyAuthEmps(string systemId, string companyId)
        {
            return BlSINI.GetCompanyAuthEmps(systemId, companyId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 儲存員工群組：先刪除該員工所有 A62，再依傳入群組代號新增
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="groupIds">勾選的群組代號陣列</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{employeeId}/groups")]
        public MdApiMessage SaveEmployeeGroups(string employeeId, [FromBody] string[] groupIds)
        {
            if (string.IsNullOrEmpty(employeeId))
                return HttpContext.Response.UpdateFailed(new ArgumentException("employeeId required"));
            try
            {
                int _result = BlA62.SaveEmployeeGroups(employeeId, groupIds ?? new string[0]);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 新增員工資料（A08）並一併儲存群組授權（A62）
        /// </summary>
        /// <param name="obj">員工資料與群組代號（employee 必填）</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdEmployeeSaveRequest obj)
        {
            if (obj?.Employee == null || string.IsNullOrWhiteSpace(obj.Employee.A0801))
                return HttpContext.Response.InsertFailed(new ArgumentException("employeeId required"));
            try
            {
                int _a08 = BlA08.ProcessInsert(obj.Employee, this.FrontKeyFile);
                int _a62 = BlA62.SaveEmployeeGroups(obj.Employee.A0801, obj.GroupIds ?? new string[0]);
                return HttpContext.Response.InsertSuccess(_a08 + _a62);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新授權公司別員工清單
        /// </summary>
        /// <param name="systemId">AS/GL/IV/PY/RQ</param>
        /// <param name="companyId">公司別</param>
        /// <param name="emps">員工清單</param>
        /// <returns></returns>
        [HttpPut("update/compauth/{systemId}/{companyId}")]
        public MdApiMessage UpdateCompanyAuthEmps(string systemId, string companyId, [FromBody] MdAuthEmps emps)
        {
            try
            {
                emps.Emps = UniqIgnoreCase(emps.Emps);
                // 呼叫商業元件執行修改作業
                int _result = BlSINI.UpdateCompanyAuthEmps(systemId, companyId, emps);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 執行身分證字號變更（ARTHGUI、ARTHPY）
        /// </summary>
        /// <param name="obj">舊值、新值</param>
        /// <returns>系統規範訊息物件，執行結果清單於 ResponseData</returns>
        [HttpPost("update/idnumber")]
        public MdApiMessage ChangeIdNumber([FromBody] MdChangeValue obj)
        {
            try
            {
                var list = BlA08.UpdateIDNumber(obj.OldValue, obj.NewValue);
                return HttpContext.Response.UpdateSuccess(list?.Count ?? 0, responseData: list);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 執行員工編號變更（多系統）
        /// </summary>
        /// <param name="obj">舊值、新值</param>
        /// <returns>系統規範訊息物件，執行結果清單於 ResponseData</returns>
        [HttpPost("update/employeeid")]
        public MdApiMessage ChangeEmployeeId([FromBody] MdChangeValue obj)
        {
            try
            {
                var list = BlA08.UpdateEmployeeID(obj.OldValue, obj.NewValue);
                return HttpContext.Response.UpdateSuccess(list?.Count ?? 0, responseData: list);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }


        /// <summary>
        /// 修改員工資料（員工編號不可變更）並一併儲存群組授權（A62）
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <param name="obj">員工資料與群組代號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{employeeId}")]
        public MdApiMessage Update(string employeeId, [FromBody] MdEmployeeSaveRequest obj)
        {
            if (string.IsNullOrEmpty(employeeId) || obj?.Employee == null || !string.Equals(employeeId, obj.Employee.A0801, StringComparison.OrdinalIgnoreCase))
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            try
            {
                int _a08 = BlA08.ProcessUpdate(employeeId, obj.Employee, this.FrontKeyFile);
                int _a62 = BlA62.SaveEmployeeGroups(employeeId, obj.GroupIds ?? new string[0]);
                return HttpContext.Response.UpdateSuccess(_a08 + _a62);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除員工資料
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{employeeId}")]
        public MdApiMessage Delete(string employeeId)
        {
            try
            {
                int _result = BlA08.ProcessDelete(employeeId);
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
