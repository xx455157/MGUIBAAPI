#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.GUI;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
	/// <summary>
	/// 【需經驗證】簽核階段資料資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class MessagesController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlD5 BlD5 => new BlD5(ClientContent);
		private BlA79 BlA79 => new BlA79(ClientContent);

		#endregion

		#region " 共用函式 -  查詢資料 "

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 複製訊息資料
		/// </summary>
		/// <param name="srcObj">來源查詢物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost("clone")]
		public MdApiMessage Clone(MdAttachment srcObj)
		{
			try
			{
				var _D5s = BlD5.GetData(0, srcObj.A7903, srcObj.A79102, Common.CvrStr2Dec(srcObj.A79101));
				if (_D5s != null && _D5s.Any() && BlA79.IsExist02(srcObj))
				{
					List<MdAttachment> _targets = new List<MdAttachment> { };
					foreach (var _D5 in _D5s)
					{
						_targets.Add(new MdAttachment
						{
							A7903 = "mBOF08",
							A79101 = Common.Cvr2String(Common.CvrStr2Int(_D5.D501)),
							A79102 = "D5"
						});
					}

					int _result = BlA79.CloneData(srcObj, _targets);
					return HttpContext.Response.InsertSuccess(_result);
				}
				else
				{
					// 回應前端新增失敗訊息
					throw new Exception(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NoQueryData"));
				}
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
