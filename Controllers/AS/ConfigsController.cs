#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS;
using GUIStd.DAL.AllNewAS.Models;
using GUIStd.Models;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.AS.Configs
{
	/// <summary>
	/// 【需經驗證】系統設定資料控制器
	/// </summary>
	[Route("as/[controller]")]
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
		/// <param name="pTopic">pTopic</param>
		/// <param name="pTopicValue">pTopicValue</param>
		/// <returns>SINI模型泛型集合物件</returns>
		[HttpGet("{section}")]
		public IEnumerable<MdConfig> GetData(string section, 
			[FromQuery] string pTopic = "", [FromQuery] string pTopicValue = "")
		{
			return BlSINI.GetData<MdConfig>(section, pTopic, pTopicValue);
		}

		#endregion
	}
}
