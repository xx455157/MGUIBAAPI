#region " 匯入的名稱空間：Framework "

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Configs;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 系統參數控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class ConfigsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlSINI BlSINI => new BlSINI(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢廳別詳細組態
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <returns>MPOS廳別詳細組態設定物件</returns>
        [HttpGet("{posid}")]
        public MdPosConfig GetPosConfig(string posId)
        { 
            return BlSINI.GetPosConfigs(posId);
        }

        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary>
        /// 通用規則組態設定
        /// </summary>
        /// <param name="moduleId">模組代碼</param>
        /// <param name="values">組態設定值</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("{moduleId}")]
        public MdApiMessage WriteConfig(string moduleId, [FromBody] IEnumerable<MdValue> values)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlSINI.WritePosConfigs(moduleId, values);
                var _resultObj = HttpContext.Response.InsertSuccess(_result);
                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 廳別詳細組態設定
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <param name="moduleId">模組代碼</param>
        /// <param name="values">組態設定值</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("{posid}/{moduleId}")]
        public MdApiMessage WritePosConfig(string posId, string moduleId, [FromBody] IEnumerable<MdValue> values)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlSINI.WritePosConfigs(moduleId, values, posId);
                var _resultObj = HttpContext.Response.InsertSuccess(_result);
                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }



        #endregion
    }
}
