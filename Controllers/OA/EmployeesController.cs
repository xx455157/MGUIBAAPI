#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.DAL.Base.Models;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.BLL.OA;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
    /// <summary>
    /// 【需經驗證】OA 員工控制器（NETERP A08）
    /// </summary>
    [Route("oa/[controller]")]
    public class EmployeesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 員工商業邏輯物件屬性
        /// </summary>
        private BlA08 BlA08 => new BlA08(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得員工輔助資料
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/pages/{pageNo}")]
        public MdCode_p GetSHelp(
            [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] string queryText,
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
        /// 取得唯一的員工資料
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <returns>員工資料</returns>
        [HttpGet("{employeeId}")]
        public MdUser GetRow(string employeeId)
        {
            return BlA08.GetRow(employeeId);
        }

        /// <summary>
        /// 判斷員工編號是否已存在
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <returns>已存在為 true，否則為 false</returns>
        [HttpGet("exists/{employeeId}")]
        public bool IsExist(string employeeId)
        {
            return BlA08.IsExist(employeeId ?? "");
        }

        #endregion
    }
}
