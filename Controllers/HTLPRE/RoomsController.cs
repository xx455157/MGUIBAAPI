#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.Models;
using GUICore.Web.Extensions;
using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using System.Collections.Generic;
using GUIStd.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// 房間資料控制器
    /// </summary>
    [Route("htlpre/[controller]")]
    public class RoomsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTHK BlHTHK => new BlHTHK(ClientContent);
        private BlHTRT BlHTRT => new BlHTRT(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("exist/{roomNo}")]
        public bool IsExists(string roomNo)
        {
            return BlHTHK.IsExists(roomNo);
        }

        [HttpPost("exist")]
        public bool IsExists([FromBody]MdRange<string> roomNoRange)
        {
            return BlHTHK.IsExists(roomNoRange);
        }

        [HttpGet("inuse/{roomNo}")]
        public bool IsInUse(string roomNo)
        {
            return BlHTHK.IsInUse(roomNo);
        }

        /// <summary>
        /// 檢查房型是否存在房間
        /// </summary>
        /// <param name="roomTypeCode">房型代碼</param>
        /// <returns>是否存在房間</returns>
        [HttpGet("exist/roomtype/{roomTypeCode}")]
        public bool IsRoomTypeExistsAnyRoom(string roomTypeCode)
        {
            return BlHTRT.IsExists(roomTypeCode);
        }

        /// <summary>
        /// 取得分頁頁次的房間資料
        /// </summary>
        /// <param name="obj">查詢條件物件</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">每頁資料筆數</param>
        /// <returns>分頁房間資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdRoom_p GetDataForPaging(MdRoom_q obj, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] int rowsPerPage)
        {
            return BlHTHK.GetDataForPaging(obj, pageNo, rowsPerPage);
        }

        /// <summary>
        /// 查詢房型資料
        /// </summary>
        /// <returns>房型資料泛型集合物件</returns>
        [HttpGet("roomtypes")]
        public IEnumerable<MdRoomType> GetRoomTypes()
        {
            return BlHTRT.GetData();
        }


        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增房間資料
        /// </summary>
        /// <param name="obj">房間資料模型物件</param>
        /// <returns>API 回應訊息物件</returns>
        [HttpPost()]
        public MdApiMessage Insert(MdRoomBasic_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTHK.Insert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }
        [HttpPost("range")]
        public MdApiMessage Insert(MdRoomRange_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTHK.Insert(obj);

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
        /// 修改房間資料
        /// </summary>
        /// <param name="obj">房間資料模型物件</param>
        /// <returns>API 回應訊息物件</returns>
        [HttpPut()]
        public MdApiMessage Update(MdRoomBasic_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTHK.Update(obj);

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
        /// 刪除房間資料
        /// </summary>
        /// <param name="roomNo">房間編號</param>
        /// <returns>API 回應訊息物件</returns>
        [HttpDelete("{roomNo}")]
        public MdApiMessage Delete(string roomNo)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                var _result = BlHTHK.Delete(roomNo);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增房型資料
        /// </summary>
        /// <param name="obj">房型資料模型物件</param>
        /// <returns>API 回應訊息物件</returns>
        [HttpPost("roomtype")]
        public MdApiMessage Insert(MdRoomType_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTRT.Insert(obj);

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
        /// 更新房型資料
        /// </summary>
        /// <param name="obj">房型資料模型物件</param>
        /// <returns>API 回應訊息物件</returns>
        [HttpPut("roomtype")]
        public MdApiMessage Update(MdRoomType_w obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHTRT.Update(obj);

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
        /// 刪除房間資料
        /// </summary>
        /// <param name="roomNo">房間編號</param>
        /// <returns>API 回應訊息物件</returns>
        [HttpDelete("roomtype/{roomNo}")]
        public MdApiMessage DeleteRoomType(string roomNo)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                var _result = BlHTRT.Delete(roomNo);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }


        #endregion
    }
}
