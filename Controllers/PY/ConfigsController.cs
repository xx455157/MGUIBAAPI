#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewGUI.Models;
using GUICore.Web.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
	/// <summary>
	/// 【需經驗證】PY 系統設定資料控制器
	/// </summary>
	[Route("py/[controller]")]
	public class ConfigsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlSINI BlSINI => new BlSINI(ClientContent);

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 保存設定資料（用於保存 section 對應的多個配置項到 PY SINI）
        /// 如果值為空陣列，則刪除所有符合前綴的舊資料
        /// </summary>
        /// <param name="section">Section 名稱</param>
        /// <param name="topicPrefix">Topic 前綴，例如 "ID_"</param>
        /// <param name="values">值的集合（可以為空，表示刪除所有舊資料）</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("savesectiontopiclist/{section}")]
        public MdApiMessage SaveSettings(string section, [FromQuery] string topicPrefix, [FromBody] List<string> values)
        {
            try
            {
                // 呼叫商業元件執行保存作業
                int _result = BlSINI.SaveSettings(section, topicPrefix, values);

                // 回應前端保存成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端保存失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion
    }
}
