#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;

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
		[FromQuery]bool sortByName)
	{
		return BlA08.GetSHelp(queryText, ControlName, pageNo, sortByName);
	}

	#endregion
	}
}
