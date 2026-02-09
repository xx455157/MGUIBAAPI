#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Extensions;
using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd;
using GUIStd.Models;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        /// 取得有效使用者帳號的員工資料
        /// </summary>
        /// <param name="pageNo">查詢頁次（若 <= 0 表示取得全部資料）</param>
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
        /// <param name="pageNo">查詢頁次</param>
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

        #endregion
    }
}
