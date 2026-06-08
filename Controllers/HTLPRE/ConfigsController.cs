#region " 匯入的名稱空間：Framework "

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Configs;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTSetup;

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
        private BlConfigs BlConfigs => new BlConfigs(ClientContent);

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

        /// <summary>
        /// 取得模組(Section)組態設定
        /// </summary>
        /// <param name="section">SINI Section</param>
        /// <returns>SINI 組態列表</returns>
        [HttpGet("module/{section}")]
        public IEnumerable<MdCode> GetModuleConfigs(string section)
        {
            // 回傳格式：[{ id: Topic, name: TopicValue }]
            return BlSINI.GetRows(section, new string[0], string.Empty);
        }

        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary>
        /// 使用Merge Into指令處理SINI新增/修改
        /// </summary>
        /// <param name="obj">Server組態設定資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost()]
        public MdApiMessage Upsert([FromBody] IEnumerable<MdConfig> obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlSINI.Upsert(obj);

                // 回應前端修改成功訊息 
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 寫入旅館營業資訊組態
        /// </summary>
        /// <param name="obj">旅館營業資訊組態設定資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("hotelInfo")]
        public MdApiMessage WriteConfig([FromBody] MdHTSetupHotelInfo_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlConfigs.WriteHotelInfo(obj);
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
                var _resultObj = HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
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

