#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Extensions;
using GUICore.Web.Controllers;
using GUICore.Web.Attributes;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Share;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Restaurant
{
	/// <summary>
	/// 餐廳菜單資料資料控制器
	/// </summary>
	[Route("htlpre/restaurant/[controller]")]
	public class MenusController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPOSMenu BlMenu => new BlPOSMenu(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "


        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 使用Merge Into大批存入菜單資料
        /// </summary>
        /// <param name="objs">菜單資料泛型模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("import")]
        public MdApiMessage Upsert([FromBody] IEnumerable<MdPosMenu_w> objs)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlMenu.Upsert(objs);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                var _inner = ex.InnerException;
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(_inner ?? ex);
            }
        }

        /// <summary>
        /// 使用Merge Into大批存入菜單擴充資料
        /// </summary>
        /// <param name="objs">菜單擴充泛型模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("import/detail")]
        public MdApiMessage Upsert([FromBody] IEnumerable<MdPosMenuDetail_w> objs)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlMenu.Upsert(objs);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                var _inner = ex.InnerException;
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(_inner ?? ex);
            }
        }
        [HttpPost("import/extend")]
        public MdApiMessage Upsert([FromBody] IEnumerable<MdPosMenuExtend_w> objs)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlMenu.Upsert(objs);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                var _inner = ex.InnerException;
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(_inner ?? ex);
            }
        }

        #endregion
    }
}
