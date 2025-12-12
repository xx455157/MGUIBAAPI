#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.HouseKeeping;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.HouseKeeping
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("htlpre/housekeeping/[controller]")]
    public class MinibarAmenities : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlMinibarAmenities BlMinibarAmenities => new BlMinibarAmenities(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得客房冰箱飲料項目清單
        /// </summary>
        /// <returns>房冰箱飲料模型泛型集合物件</returns>
        [HttpGet("minibar")]
        public IEnumerable<MdMenuHK> GetMinibarItems()
        {
            return BlMinibarAmenities.GetMinibarItems();
        }

        /// <summary>
        /// 取得客房洗衣項目清單
        /// </summary>
        /// <returns>客房洗衣模型泛型集合物件</returns>
        [HttpGet("laundry")]
        public IEnumerable<MdMenuHK> GetLaundryItems()
        {
            return BlMinibarAmenities.GetLaundryItems();
        }

        /// <summary>
        /// 取得房間備品項目清單
        /// </summary>
        /// <returns>房冰箱飲料模型泛型集合物件</returns>
        [HttpGet("amenities")]
        public IEnumerable<MdEquipment> GetAmenities()
        {
            return BlMinibarAmenities.GetAmenitiesData();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增冰箱飲料、客房洗衣帳務資料
        /// </summary> 
		/// <returns>系統規範訊息物件</returns>
        [HttpPut("minibar")]
        public MdApiMessage InsertRoomMinibar([FromBody] IEnumerable<MdMinibar_w> obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlMinibarAmenities.ProcessInsertRoomMinibar(obj);

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
        /// 新增房間備品消耗資訊
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="transectionDate"></param>
        /// <param name="roomNo"></param>
        /// <param name="relatedNo"></param>
		/// <returns>系統規範訊息物件</returns>
        [HttpPut("amenities/{transectionDate}/{roomNo}")]
        public MdApiMessage InsertRoomAmenities([FromBody] IEnumerable<MdMenuHK_w> obj, string transectionDate, string roomNo, [FromQuery] string relatedNo = "")
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlMinibarAmenities.ProcessInsertRoomAmenities(obj, transectionDate, roomNo, relatedNo);

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
