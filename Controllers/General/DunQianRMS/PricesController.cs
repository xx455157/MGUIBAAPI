#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL.Client.DunQian;
using GUIStd.DAL.AllNewHTL.Models.Client.DunQian;

#endregion

namespace MGUIBAAPI.Controllers.General.DunQianRMS
{
    /// <summary>
    /// 敦謙RMS房價控制器
    /// </summary>
    [Route("general/dunqianrms/[controller]")]
    public class PricesController : GUIAppWSController
    {
        #region " 建構子：欲自行指定使用者帳號時才需加入 "
        
        // 建構子
        public PricesController()
        {
            // 改變執行服務的使用者帳號
            this.WSUser = "DQRMS";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPrice BlPrice => new BlPrice(ClientContent);

        #endregion

        #region " 授權存取 API "

        /// <summary>
        /// 取得RMS待更新房價資訊
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="roomType">房型</param>
        /// <returns>RMS房價模型物件</returns>
        [HttpGet("{date}/{roomType}")]
        public MdRmsRoomTypePrice GetDataPrice(string date, string roomType)
        {
            return BlPrice.GetData(date, roomType);
        }

        /// <summary>
        /// 取得RMS待更新房價資訊
        /// </summary>
        /// <param name="dates">日期起</param>
        /// <param name="datee">日期迄</param>
        /// <param name="roomType">房型</param>
        /// <returns>RMS房價模型泛型集合物件</returns>
        [HttpGet("{dates}/{datee}/{roomType}")]
        public IEnumerable<MdRmsRoomTypePrice> GetDataPrice(string dates, string datee, string roomType)
        {
            return BlPrice.GetData(dates,datee, roomType);
        }

        /// <summary>
        /// 取得RMS待更新房價資訊
        /// </summary>
        /// <param name="obj">房型日期陣列</param>
        /// <returns>RMS房價模型泛型集合物件</returns>
        [HttpPost()]
        public IEnumerable<MdRmsRoomTypePrice> GetDataPrice([FromBody] MdRmsRoomTypeDates obj)
        {
            return BlPrice.GetData(obj.dates, obj.roomType);
        }


        /// <summary>
        /// 更新RMS房價上傳訊息唯一碼
        /// </summary>
        /// <param name="obj">RMS訊息唯一碼資料</param>
		/// <returns>系統規範訊息物件</returns>
        [HttpPut()]
        public MdApiMessage UpdateMsgUid([FromBody] MdRmsMessageUid obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlPrice.UpdateMessageUniqueId(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        [HttpPut("{roomtype}")]
        public MdApiMessage UpdateMsgUid(string roomType, [FromBody] MdRmsMessageUid obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlPrice.UpdateMessageUniqueId(roomType, obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        [HttpPut("{dates}/{datee}/{roomtype}")]
        public MdApiMessage UpdateMsgUid(string dates, string datee, string roomType, [FromBody] MdRmsMessageUid obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlPrice.UpdateMessageUniqueId(dates, datee, roomType, obj.messageUid);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

    }
}
