#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCF12;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 【需經驗證】公司產品授權(帳冊管理)控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class CompanyProductAuthController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// vMCF12 商業邏輯物件屬性
        /// </summary>
        private BlMCF12 BlMCF12 => mBlMCF12 = mBlMCF12 ?? new BlMCF12(ClientContent);
        private BlMCF12 mBlMCF12;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得公司產品授權(帳冊管理)分頁資料
        /// </summary>
        /// <param name="pageNo">查詢頁次（若 <= 0 表示取得全部資料）</param>
        /// <param name="systemId">系統別（可選，如：GL、AS、PY）</param>
        /// <param name="companyId">公司別（可選）</param>
        /// <param name="employeeId">員工編號（可選）</param>
        /// <returns>分頁公司產品授權資料模型物件</returns>
        [HttpGet("help/pages/{pageNo}")]
        public MdCompanyProductAuth_p GetHelp(
            [DARange(0, int.MaxValue)] int pageNo,
            [FromQuery] string systemId = "",
            [FromQuery] string companyId = "",
            [FromQuery] string employeeId = "")
        {
            return BlMCF12.GetCompanyProductAuthHelp(ControlName, pageNo, systemId, companyId, employeeId);
        }

        /// <summary>
        /// 依員工編號查詢授權系統
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <returns>授權系統資料陣列</returns>
        [HttpGet("help/employee/{employeeId}/systems")]
        public IEnumerable<MdCode> GetEmployeeAuthorizedSystems([FromRoute] string employeeId)
        {
            return BlMCF12.GetEmployeeAuthorizedSystems(employeeId);
        }

        #endregion

        #region " 共用函式 - 新增資料 "

        /// <summary>
        /// 新增公司產品授權資料（單筆或多筆）
        /// </summary>
        /// <param name="items">要新增的授權資料陣列，每個項目包含 section、topic 和 topicValue</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] IEnumerable<MdConfig> items)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlMCF12.InsertCompanyProductAuth(items);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 刪除資料 "

        /// <summary>
        /// 刪除公司產品授權資料（單筆或多筆）
        /// </summary>
        /// <param name="items">要刪除的授權資料陣列，每個項目包含 section 和 topic</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete]
        public MdApiMessage Delete([FromBody] IEnumerable<MdConfig> items)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlMCF12.DeleteCompanyProductAuth(items);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}

