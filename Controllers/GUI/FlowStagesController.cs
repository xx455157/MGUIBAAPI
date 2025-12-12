#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using System;
using System.Collections.Generic;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.GUI;
using GUIStd.BLL.GUI.Private;
using GUIStd.BLL.AllNewGUI;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.EForm;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using System.Threading.Tasks;
using GUIStd.Attributes;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
	/// <summary>
	/// 【需經驗證】簽核階段資料資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class FlowStagesController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlA87 BlA87 => new BlA87(ClientContent);

		#endregion

		#region " 共用函式 -  查詢資料 "

		/// <summary>
		/// 取得簽核流程資料
		/// </summary>
		/// <param name="dataType">資料別</param>
		/// <param name="petPKey">簽呈PKey</param>
		/// <returns>簽核流程模型集合物件</returns>
		[HttpGet("{dataType}/{petPKey}")]
		public IEnumerable<MdFlowStep> GetFlowStages(string dataType, decimal petPKey)
		{
			return BlA87.GetData(petPKey, dataType);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		#endregion
	}
}
