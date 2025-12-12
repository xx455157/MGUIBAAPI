#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.DAL.HT2020.Models.Private.Vouchers;
using GUIStd.BLL.HT2020.Private;
using System.Collections.Generic;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTL
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htl/[controller]")]
	public class VouchersController : GUIAppAuthController
	{
		#region "共用函式 - 查詢資料 "

		/// <summary>
		/// 取得票券基本資料
		/// </summary>
		/// <param name="voucherNo">票券號碼</param>	
		/// <returns>票券基本資料模型泛型集合物件</returns>
		[HttpGet("{voucherNo}")]
		public MdVoucher GetData(string voucherNo)
		{
			return new BlVouchers(ClientContent).GetData(voucherNo);
		}
		#endregion


		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 核銷票券資料
		/// </summary>
		/// <param name="voucherNo">票券號碼資料模型物件</param>
		/// <param name="obj">核銷票券資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{voucherNo}")]
		public MdApiMessage Update(string voucherNo, [FromBody] MdVoucher_w obj)
		{
			try
			{
				// 呼叫商業元件執行修改作業
				int _result = new BlVouchers(ClientContent).Update(voucherNo,obj);

				// 回應前端新增成功訊息
				return HttpContext.Response.UpdateSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.UpdateFailed(ex);
			}
		}

		#endregion
	}

	

	
}
