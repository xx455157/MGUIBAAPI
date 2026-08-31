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
using GUIStd.DAL.AllNewGUI.TSQL;

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

        #endregion
    }
}
