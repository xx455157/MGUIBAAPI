#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Invoice;
using GUIStd.Models;
using MGUIBAAPI.Models.HTLPRE;
using GUIStd.DAL.AllNewHTL.Models.Private.Restaurant;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 列印發票資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class InvoicesController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlInvoices BlInvoices => new BlInvoices(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 發票查詢
		/// </summary>
		/// <param name="folioNo">帳單號碼</param>
		/// <returns>應開發票金額與已開發票資料模型物件</returns>
		[HttpGet("query/{folioNo}")]
		public CmHTVCM12 GetData(string folioNo)
		{
			decimal _chargeAmount = 0;
			IEnumerable<MdInvoice> _invoices = null;

			BlInvoices.GetInvoiceData(folioNo: folioNo, chargeAmount: out _chargeAmount, invoices: out _invoices);

			return new CmHTVCM12()
			{
				ChargeAmount = _chargeAmount,
				Invoices = _invoices				
			};
		}

		/// <summary>
		/// 依條件查詢發票相關資料及統計
		/// </summary>
		/// <param name="obj">發票查詢條件物件</param>
		/// <returns>發票相關資料及統計模型物件</returns>
		[HttpPost("analysis")]
		public MdInvData GetDataByInvList([FromBody] MdInvData_q obj) 
		{
			return BlInvoices.GetData(obj);
		}

		/// <summary>
		/// 取得單筆發票明細資料
		/// </summary>
		/// <param name="invoiceNo">發票號碼</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpGet("detail/{invoiceNo}")]
		public MdInvoice GetRow(string invoiceNo)
		{
			return BlInvoices.GetRow(invoiceNo);
		}

		/// <summary>
		///	依發票號碼取得帳單相關明細	
		/// </summary>
		/// <param name="invoiceNo">發票號碼</param>
		/// <returns>餐廳帳單及發票資料模型物件</returns>
		[HttpGet("query/folio/{invoiceNo}")]
		public MdPosSummary GetDataByPOS(string invoiceNo)
		{
			return BlInvoices.GetDataByPOS(invoiceNo);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 新增發票資料
		/// </summary>
		/// <param name="folioNo">帳單號碼路徑參數</param>
		/// <param name="obj">發票資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost("{folioNo}")]
		public MdApiMessage Insert(string folioNo, [FromBody] IEnumerable<MdInvoice_w> obj)
		{
			try
			{				
				IEnumerable<MdInvoice> _returnObj;
				decimal _chargeAmount;				
				string[] _pqList;

				// 呼叫商業元件執行新增作業
				int _result = BlInvoices.ProcessInsert(obj, out _pqList);

				BlInvoices.GetInvoiceData(folioNo: folioNo, chargeAmount: out _chargeAmount, invoices: out _returnObj);

				var _responseData = new
				{
					returnObj = _returnObj,
					pqList = _pqList
				};
				
				// 回應前端新增成功訊息
				return HttpContext.Response.SendSuccess(
					Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_PrintInvoiceSuccess"), 
					responseData: _responseData);				
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.UpdateFailed(ex);
			}
		}

		/// <summary>
		/// 修改發票資料
		/// </summary>		        
		/// <param name="folioNo">帳單號碼路徑參數</param>
		/// <param name="invoiceNo">發票號碼路徑參數</param>
		/// <param name="status">狀態碼路徑參數</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{folioNo}/{invoiceNo}/{status}")]
		public MdApiMessage Update(string folioNo, string invoiceNo, string status)
		{
			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlInvoices.ProcessUpdate(folioNo, invoiceNo, status);
				
				// 回應前端修改成功訊息
				return HttpContext.Response.SendSuccess(
					string.Format(Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_VoidInvoiceSuccess"), invoiceNo));
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
