#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Attributes;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.HT2020;
using GUIStd.DAL.HT2020.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTL
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htl/[controller]")]
	public class ConfigsController : GUIAppAuthController
    {
        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得SINI資料
        /// </summary>
        /// <param name="hotelId">館別代碼</param>
        /// <param name="section">section</param>
        /// <param name="topic">topic</param>
        /// <param name="fldName">topic</param>
        /// <returns>字串</returns>
        [HttpGet]
		public string GetData([FromQuery] string hotelId, [RequiredFromQuery] string section,
			[RequiredFromQuery] string topic, [FromQuery] string fldName)
		{
			return new BlSINI(ClientContent).GetData(hotelId, section, topic, fldName);
		}

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 異動SINI資料
        /// </summary>
        /// <param name="obj">SINI物件</param>
        /// 
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdConfig_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = new BlSINI(ClientContent).Insert(obj);

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
