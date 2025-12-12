#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Attributes;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.HT2020.Private;
using GUIStd.DAL.HT2020.Models;
using GUIStd.DAL.HT2020.Models.Private.Members;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTL.Members
{
	/// <summary>
	/// 【需經驗證】會員資料控制器
	/// </summary>
	[Route("htl/[controller]")]
	public class MembersController : GUIAppAuthController
	{
		#region " 共用函式 -  查詢資料 "

		/// <summary>
		/// 取得會員基本資料
		/// </summary>
		/// <param name="memberId">會員ID</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("{memberId}")]
		public MdMember GetMemberData(string memberId)
		{
			return new BlMembers(ClientContent).GetData(memberId);
		}

		/// <summary>
		/// 取得會員現況資料
		/// </summary>
		/// <param name="memberId">會員 ID</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("situation/{memberId}")]
		public object GetSitionData(string memberId)
		{
			return new BlMembers(ClientContent).GetDataForSituation(memberId, CurrentLang, ClientContent);
		}

		/// <summary>
		/// 取得會員點數紀錄
		/// </summary>
		/// <param name="memberId">會員 ID</param>
		/// <param name="pageNo">頁碼</param>
		/// <param name="startDate">起始日期</param>
		/// <param name="endDate">截止日期</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("points/histories/{memberId}/{pageNo}")]
		public MdPointHistories_p GetPointsData(string memberId, int pageNo,
			[FromQuery] string startDate, [FromQuery] string endDate)
		{
			return new BlMembers(ClientContent).GetDataForPointsByPage(
				memberId, pageNo, startDate, endDate, ClientContent);
		}

		/// <summary>
		/// 取得會員消費紀錄
		/// </summary>
		/// <param name="memberId">會員 ID</param>
		/// <param name="pageNo">頁碼</param>
		/// <param name="startDate">起始日期</param>
		/// <param name="endDate">截止日期</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("consumptions/histories/{memberId}/{pageNo}")]
		public MdConsumptionHistories_p GetData(string memberId, int pageNo,
			[FromQuery] string startDate, [FromQuery] string endDate)
		{
			return new BlMembers(ClientContent).GetDataForConsumptionHistoriesByPage(
				memberId, pageNo, startDate, endDate, CurrentLang);
		}

		/// <summary>
		/// 取得會員消費明細
		/// </summary>
		/// <param name="memberId">會員 ID</param>
		/// <param name="posId">廳別</param>
		/// <param name="hotelId">館別</param>
		/// <param name="addDate">截止日期</param>
		/// <param name="addTime">截止日期</param>
		/// <param name="addStation">截止日期</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("consumptions/details/{memberId}")]
		public MdConsumptionDetails GetConsumptionsDetails(string memberId,
			[FromQuery] string hotelId, [FromQuery] string posId, [RequiredFromQuery] string addDate,
			[RequiredFromQuery] string addTime, [RequiredFromQuery] string addStation)
		{
			return new BlMembers(ClientContent).GetDataForConsumptionsDetails(
				memberId, hotelId, posId, addDate, addTime, addStation, CurrentLang);
		}

		#endregion

		#region " 共用屬性 - 異動資料"

		/// <summary> 
		/// 修改資料
		/// </summary>
		/// <param name="memberId">會員ID</param>
		/// <param name="obj">程式資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{memberId}")]
		public MdApiMessage Update(string memberId, [FromBody] MdMember_w obj)
		{
			// 檢查鍵值路徑參數與本文中的鍵值是否相同
			if (!memberId.EqualsIgnoreCase(obj.GR06))
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailedWhenKeyNotSame();
			}

			try
			{
				// 呼叫商業元件執行修改作業
				int _result = new BlMembers(ClientContent).ProcessUpdate(obj, ClientContent);

				// 回應前端修改成功訊息
				return HttpContext.Response.UpdateSuccess(_result);
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
