#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.OA.Private;
using GUIStd.DAL.OA.Models.Private.CallReport;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.OA;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
	/// <summary>
	/// Call Report資料控制器
	/// </summary>
	[Route("oa/[controller]")]
	public class CallReportsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCallReport BlCallReport => new BlCallReport(ClientContent);
        private BlOA03 BlOA03 => new BlOA03(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得符合條件的所有Call Report的分頁頁次資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="customerId">拜訪客戶</param>
        /// <param name="visitDates">拜訪日期(起)</param>
        /// <param name="visitDatee">拜訪日期(迄)</param>
        /// <param name="nextVisitDates">下次拜訪日期(起)</param>
        /// <param name="nextVisitDatee">下次拜訪日期(迄)</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="funcName">功能名稱</param>
        /// <returns>Call Report泛型集合物件</returns>
        [HttpGet("query/pages/{pageNo}")]
		public MdCallReport_p GetData(string companyId, string customerId,
			string visitDates, string visitDatee, string nextVisitDates, string nextVisitDatee,
			[DARange(1, int.MaxValue)] int pageNo, string funcName)
		{ 
			return BlCallReport.GetData(companyId, customerId, visitDates, visitDatee,
				nextVisitDates, nextVisitDatee, funcName, pageNo, CurrentUILang);
		}

		/// <summary>
		/// 取得唯一的Call Report資料
		/// </summary>
		/// <param name="companyId">公司別</param>
		/// <param name="callNo">Call No.</param>
		/// <returns>Call Report模型物件</returns>
		[HttpGet("{companyId}/{callNo}")]
		public MdCallReport GetRow(string companyId, string callNo)
		{
			return BlCallReport.GetRow(companyId, callNo, CurrentUILang);
		}

		#endregion

		#region " 共用函式 - 使用交易異動資料 "

		/// <summary>
		/// 交易式新增資料
		/// </summary>
		/// <param name="obj">Call Report資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost]
		public MdApiMessage InsertTrans([FromBody] MdCallReport obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlOA03.ProcessInsertTrans(obj, ClientContent, out string _callNo);

				// 回應前端新增成功訊息
				return HttpContext.Response.InsertSuccess(_result, responseData: _callNo);
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		/// <summary>
		/// 交易式修改資料
		/// </summary>
		/// <param name="obj">Call Report資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut]
		public MdApiMessage UpdateTrans([FromBody] MdCallReport obj)
		{
			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlOA03.ProcessUpdateTrans(obj, ClientContent);

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
		/// 交易式刪除資料
		/// </summary>
		/// <param name="companyId">公司別</param>
		/// <param name="callNo">Call No.</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpDelete("{companyId}/{callNo}")]
		public MdApiMessage DeleteTrans(string companyId, string callNo)
		{
			try
			{
				// 呼叫商業元件執行刪除作業
				int _result = BlOA03.ProcessDeleteTrans(companyId, callNo);

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
