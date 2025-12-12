#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Attributes;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.HT2020.Private;
using GUIStd.DAL.HT2020.Models.Private.SysMember;
using GUIStd.Models;
using MGUIBAAPI.Features;

#endregion

namespace MGUIBAAPI.Controllers.HTL.Members
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htl/[controller]")]
	public class SysMemberController : GUIAppAuthController
	{
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlSysMember BlSysMember => new BlSysMember(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得首頁活動資料
        /// </summary>
        /// <param name="deviceId">Device ID</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("events/{deviceId}")]
		public MdEvents GetDataForMenuEvents(string deviceId)
		{
			return BlSysMember.GetDataForMenuEvents(
				deviceId, ClientContent, Utils.AppSettings.TokenKeyFile, ImageReadPath);
		}

		/// <summary>
		/// 身份驗證
		/// </summary>
		/// <param name="mobileNo">手機號碼</param>
		/// <param name="password">密碼</param>
		/// <param name="deviceId">Device ID</param>
		/// <returns>程式清單匿名型別物件</returns>
		[HttpGet("login")]
		public MdApiMessage Login([RequiredFromQuery] string mobileNo,
			[RequiredFromQuery] string password, [RequiredFromQuery] string deviceId)
		{
			return BlSysMember.LoginCheck(
				mobileNo, password, deviceId, Utils.AppSettings.TokenKeyFile);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 註冊裝置
		/// </summary>
		/// <param name="deviceId">Device ID</param>
		/// <param name="obj">程式資料模型物件，可宣告為</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost("register/{deviceId}")]
		public MdApiMessage Insert(string deviceId, [FromBody] MdRegister obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlSysMember.ProcessInsert(obj, ClientContent);

				// 回應前端新增成功訊息
				return HttpContext.Response.InsertSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		#endregion
	}
}
