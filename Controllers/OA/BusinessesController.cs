#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.OA;
using GUIStd.DAL.OA.Models;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.BLL.GUI.Private;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
	/// <summary>
	/// 業務機會資料控制器
	/// </summary>
	[Route("oa/[controller]")]
	public class BusinessesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlOA12 BlOA12 => new BlOA12(ClientContent); 

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得唯一的業務機會資料
        /// </summary>
        /// <param name="seqNo">Key</param>
        /// <returns>業務機會模型物件</returns>
        [HttpGet("{seqNo}")]
		public MdBusinessOppty GetRow(decimal seqNo)
		{
			return BlOA12.GetRow(seqNo, CurrentUILang);
		}

		/// <summary>
		/// 取得業務機會輔助資料
		/// </summary>
		/// <param name="customerId">客戶代碼</param>
		/// <returns>業務機會資料代碼模型集合物件</returns>
		[HttpGet("help/{customerId}")]
		public IEnumerable<MdCode> GetHelp(string customerId)
		{
			return BlOA12.GetHelp(customerId);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新增資料
		/// </summary>
		/// <param name="obj">業務機會資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost]
		public MdApiMessage Insert([FromBody] MdBusinessOppty obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlOA12.ProcessInsert(obj, ClientContent, out decimal seqNo);

				// 回應前端新增成功訊息
				return HttpContext.Response.InsertSuccess(_result, responseData: seqNo);
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
		/// <param name="seqNo">Key</param>
		/// <param name="obj">業務機會資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{seqNo}")]
		public MdApiMessage Update(decimal seqNo, [FromBody] MdBusinessOppty obj)
		{
			// 檢查鍵值路徑參數與本文中的鍵值是否相同
			if (!seqNo.Equals(obj.OA1201))
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailedWhenKeyNotSame();
			}

			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlOA12.ProcessUpdate(seqNo, obj, ClientContent);

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
		/// <param name="seqNo">Key</param>
		/// <returns>客戶資料模型物件</returns>
		[HttpDelete("{seqNo}")]
		public MdApiMessage Delete(decimal seqNo)
		{
			try
			{
				// 呼叫商業元件執行刪除作業
				int _result = BlOA12.ProcessDelete(seqNo);

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
