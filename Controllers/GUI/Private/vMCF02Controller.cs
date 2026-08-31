#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCF02;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vMCF02 部門基本資料維護 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vMCF02Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>BlMCF02 商業邏輯物件屬性</summary>
        private BlMCF02 BlvMCF02 => mvMCF02 = mvMCF02 ?? new BlMCF02(ClientContent);
        private BlMCF02 mvMCF02;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>取得查詢頁 UI 選項資料</summary>
        [HttpGet("query/uidata/qv")]
        public MdMCF02_h_qv GetUIDataQV()
        {
            return BlvMCF02.GetUIDataQV();
        }

        /// <summary>取得明細頁 UI 選項資料</summary>
        [HttpGet("query/uidata/d")]
        public MdMCF02_h_d GetUIDataD(string companyId = "")
        {
            return BlvMCF02.GetUIDataD(companyId);
        }

        /// <summary>取得查詢資料（分頁）</summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdMCF02_l_p GetQueryList([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdMCF02_q queryParams, int rowsPerPage = 0)
        {
            return BlvMCF02.GetQueryList(queryParams, ControlName, pageNo, rowsPerPage);
        }

        /// <summary>取得單筆明細資料</summary>
        [HttpGet("query/detail/{deptCode}")]
        public MdDepartment_d GetDetailData(string deptCode)
        {
            return BlvMCF02.GetDetailData(deptCode);
        }

        #endregion
    }
}
