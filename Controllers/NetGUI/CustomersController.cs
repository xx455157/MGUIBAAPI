#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.Extensions;
using GUIStd.BLL.GUI;
using GUIStd.BLL.GUI.Private;
using GUIStd.BLL.PY.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.GUI.Models.Private.Customers;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class CustomersController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCustomers BlCustomers => new BlCustomers(ClientContent);
        private BlA16 BlA16 => new BlA16(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得唯一的客戶資料
        /// </summary>
        /// <param name="customerId">客戶編號</param>
        /// <returns>客戶模型物件</returns>
        [HttpGet("{customerId}")]
		public MdCustomer_2 GetRow(string customerId)
		{
			return BlCustomers.GetRow(customerId);
		}

		/// <summary>
		/// 取得客戶輔助資料
		/// </summary>
		/// <param name="fullName">是否顯示全名,否的話顯示簡稱</param>
		/// <returns>客戶資料代碼模型集合物件</returns>
		[HttpGet("help")]
		public IEnumerable<MdCode> GetHelp(bool fullName = false)
		{
			return BlA16.GetHelp(fullName, CurrentUILang);
		}

		/// <summary>
		/// 取得分頁頁次的輔助資料
		/// </summary>
		/// <param name="queryText">編號或名稱必需包含傳入的參數值</param>
		/// <param name="pageNo">查詢頁次</param>
		/// <param name="fullName">是否全名</param>
		/// <param name="sortByName">是否依名稱排序</param>
		/// <returns>分頁輔助資料模型物件</returns>
		[HttpGet("help/{queryText}/pages/{pageNo}")]
		public MdCode_p GetSHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo, 
			[FromQuery] bool fullName, [FromQuery] bool sortByName)
		{
			return BlA16.GetSHelp(queryText, ControlName, pageNo, fullName, sortByName, CurrentUILang);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新增資料
		/// </summary>
		/// <param name="obj">客戶資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost("autoid")]
		public MdApiMessage Insert([FromBody] MdCustomer_2 obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlCustomers.ProcessInsert(obj, ClientContent,out string customerId);

				// 回應前端新增成功訊息
				return HttpContext.Response.InsertSuccess(_result, responseData: customerId);
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		/// <summary>
		/// 修改資料
		/// </summary>
		/// <param name="customerId">客戶編號</param>
		/// <param name="obj">客戶資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{customerId}")]
		public MdApiMessage Update(string customerId, [FromBody] MdCustomer_2 obj)
		{
			// 檢查鍵值路徑參數與本文中的鍵值是否相同
			if (!customerId.EqualsIgnoreCase(obj.A1601))
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailedWhenKeyNotSame();
			}

			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlCustomers.ProcessUpdate(customerId, obj, ClientContent);

				// 回應前端修改成功訊息
				return HttpContext.Response.UpdateSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailed(ex);
			}
		}

		/// <summary>
		/// 刪除資料
		/// </summary>
		/// <param name="customerId">客戶編號</param>
		/// <returns>客戶資料模型物件</returns>
		[HttpDelete("{customerId}")]
		public MdApiMessage Delete(string customerId)
		{
			try
			{
				// 呼叫商業元件執行刪除作業
				int _result = BlCustomers.ProcessDelete(customerId, ClientContent);

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
