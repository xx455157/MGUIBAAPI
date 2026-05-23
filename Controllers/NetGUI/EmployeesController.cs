#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.GUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.Base.Models;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 【需經驗證】員工基本資料控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class EmployeesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA08 BlA08 => new BlA08(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得輔助資料
        /// </summary>
        /// <param name="departmentId">部門編號路徑參數</param>
        /// <param name="includeEmptyRow">是否包含空白列查詢參數</param>
        /// <returns>員工資料模型泛型集合物件</returns>
        [HttpGet("help/{departmentId}")]
		public IEnumerable<MdCode> GetHelp(string departmentId, [FromQuery] bool includeEmptyRow)
		{
			return BlA08.GetHelp<MdCode>(departmentId, includeEmptyRow);
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
			[FromQuery]bool sortByName)
		{
			return BlA08.GetSHelp(queryText, ControlName, pageNo, sortByName, CurrentUILang);
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

        #endregion
    }
}
