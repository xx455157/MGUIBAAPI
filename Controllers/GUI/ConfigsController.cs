#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI.Configs
{
	/// <summary>
	/// 【需經驗證】系統設定資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class ConfigsController : GUIAppAuthController
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
			return BlSINI.GetData<MdConfig>(section, topic, language);
		}

        /// <summary>
        /// 取得上傳檔案相關參數
        /// </summary>
        /// <param name="funcName">功能名稱</param>
        /// <returns>上傳檔案相關參數模型物件</returns>
        [HttpGet("UploadInfo/{funcName}")]
		public MdUploadInfo GetUploadInfo(string funcName)
		{
			return BlSINI.GetUploadInfo(funcName);
		}

        /// <summary>
        /// 取得上傳圖片功能參數
        /// </summary>
        /// <param name="funcName">功能名稱</param>
        /// <returns>上傳檔案相關參數模型物件</returns>
        [HttpGet("UploadImage/{funcName}")]
		public MdUploadImageInfo GetUploadImageInfo(string funcName)
		{
			return BlSINI.GetUploadImageInfo(funcName);
		}
		#endregion
	}
}
