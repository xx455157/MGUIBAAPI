#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Extensions;
using GUICore.Web.Controllers;
using GUICore.Web.Attributes;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Restaurant;
using GUIStd.DAL.AllNewHTL.Models.Private.HouseKeeping;

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
        private BlHTFB BlHTFB => new BlHTFB(ClientContent);
        private BlHTFBA BlHTFBA => new BlHTFBA(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        [HttpPost("query/pages/{pageNo}")]
        public MdPosMenu_p GetDataForPaging(MdPosMenu_q obj, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] int rowsPerPage)
        {
            return BlMenu.GetDataForPaging<MdMenuBase>(obj, pageNo, rowsPerPage);
        }

        [HttpGet("isexists/{posId}/{itemCode}")]
        public bool IsExists(string posId, string itemCode)
        {
            return BlHTFB.IsExists(posId, itemCode);
        }

        #endregion

        #region " 共用函式 - 異動資料 "


        /// <summary>
        /// 新增菜單資料
        /// </summary>
        /// <param name="obj">菜單資料物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost()]
        public MdApiMessage Insert([FromBody] MdPosMenu_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTFB.Insert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 新增菜單明細資料
        /// </summary>
        /// <param name="obj">菜單明細資料物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("detail")]
        public MdApiMessage InsertDetail([FromBody] MdPosMenuDetail_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTFBA.Insert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改菜單資料
        /// </summary>
        /// <param name="obj">菜單資料物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut()]
        public MdApiMessage Update([FromBody] MdPosMenu_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTFB.Update(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 修改菜單明細資料
        /// </summary>
        /// <param name="obj">菜單明細資料物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("detail")]
        public MdApiMessage UpdateDetail([FromBody] MdPosMenuDetail_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTFBA.Update(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除菜單與關聯資料
        /// </summary>
        /// <param name="posId">餐廳代碼</param>
        /// <param name="itemCode">菜單代碼</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{posId}/{itemCode}")]
        public MdApiMessage Delete(string posId, string itemCode)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTFB.Delete(posId, itemCode);

                // 回應前端新增成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

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
        /// 使用Merge Into大批存入菜單明細資料
        /// </summary>
        /// <param name="objs">菜單明細泛型模型物件</param>
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

        /// <summary>
        /// 使用Merge Into大批存入菜單擴充資料
        /// </summary>
        /// <param name="objs">菜單擴充泛型模型物件</param>
        /// <returns>系統規範訊息物件</returns>
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
