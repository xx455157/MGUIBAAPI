#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewIV;
using GUIStd.DAL.AllNewIV.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.IV.Configs
{
	/// <summary>
	/// 【需經驗證】系統設定資料控制器
	/// </summary>
	[Route("iv/[controller]")]
	public class ConfigsController : GUIAppController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlSINI BlSINI => new BlSINI(ClientContent);

		#endregion

		#region " 共用函式 -  查詢資料 "

		/// <summary>
		/// 取得系統設定資料
		/// </summary>
		/// <param name="section">Section</param>
		/// <param name="topic">Topic</param>
		/// <param name="language">沒有輸入就視為沒有條件</param>
		/// <returns>SINI模型泛型集合物件</returns>
		[HttpGet("{section}")]
		public IEnumerable<MdConfig> GetData(string section, 
			[FromQuery] string topic = "", [FromQuery] string language = "")
		{
			if (language == "") language = CurrentUILang;
			if (language == null) language = "";
			return BlSINI.GetData<MdConfig>(section, topic, language);
		}

		/// <summary>
		/// 新增資料
		/// </summary>
		/// <param name="obj">系統設定資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost]
		public MdApiMessage Insert([FromBody] IEnumerable<MdConfig> obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlSINI.ProcessInsert(obj);

				// 回應前端新增成功訊息
				return HttpContext.Response.InsertSuccess(_result);
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
		/// <param name="obj">系統設定資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{section}/{topic}")]
		public MdApiMessage Update([FromBody] IEnumerable<MdConfig> obj)
		{
			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlSINI.ProcessUpdate(obj);

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
		/// 修改資料
		/// </summary>
		/// <param name="obj">系統設定資料模型物件</param>
		/// <param name="sectionLike">相似的Section</param>
		/// <param name="section">Section</param>
		/// <param name="topicLike">相似的Topic</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut]
		public MdApiMessage Update([FromBody] IEnumerable<MdConfig> obj,
			[FromQuery] string sectionLike, [FromQuery] string section,
			[FromQuery] string topicLike)
		{
			try
			{
				// 呼叫商業元件執行修改作業
				int _result = BlSINI.ProcessUpdate2(obj, sectionLike, section, topicLike);

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
		/// <param name="obj">系統設定資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpDelete]
		public MdApiMessage Delete([FromBody] IEnumerable<MdConfig> obj)
		{
			try
			{
				// 呼叫商業元件執行刪除作業
				int _result = BlSINI.ProcessDelete(obj);

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
