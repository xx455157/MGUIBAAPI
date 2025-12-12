#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Rooms;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Accounting
{
    /// <summary>
    /// 客房帳單資料控制器
    /// </summary>
    [Route("htlpre/accounting/[controller]")]
    public class FoliosController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTFX BlHTFX => new BlHTFX(ClientContent);
        private BlRooms BlRooms => new BlRooms(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得尚未退房的房間資料(未實作)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetDataForCheckOut()
        {
            return null;
        }

        /// <summary>
        /// 取得房間帳單資料
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("{roomNo}")]
        public MdRoomPayment GetRowForPayment(string roomNo)
        {
            return BlRooms.GetRowForPayment(roomNo);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 轉入帳務資料
        /// </summary>
        /// <param name="obj">帳務資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdFolioTransaction obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlHTFX.Insert(obj, ClientContent);

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
        /// 新增帳匣(未實作)
        /// </summary>
        /// <param name="obj">帳匣資料模型物件</param>
        /// <returns></returns>
        [HttpPost("folio")]
        public MdApiMessage InsertFolio([FromBody] object obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = -1; //new BlHTHF().Insert(obj, ClientContent);

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
