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
    /// 敦謙 訂單控制器
    /// </summary>
    [Route("general/dunqianrms/[controller]")]
    public class BookingsController : GUIAppWSController
    {
        #region " 建構子：欲自行指定使用者帳號時才需加入 "
        
        // 建構子
        public BookingsController()
        {
            // 改變執行服務的使用者帳號
            this.WSUser = "DQRMS";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlBooking BlBooking => new BlBooking(ClientContent);

        #endregion

        #region " 授權存取 API "

        /// <summary>
        /// 取得RMS訂單資訊
        /// </summary>
        /// <param name="rvNo">訂單號碼</param>
        /// <returns>RMS訂單資訊模型物件</returns>
        [HttpGet("{rvNo}")]
        public MdRmsBooking GetBooking(string rvNo)
        {
            return BlBooking.GetBooking(rvNo);
        }

        /// <summary>
        /// 取得RMS訂單資訊清單
        /// </summary>
        /// <param name="rvList">訂房號碼陣列</param>
        /// <returns>RMS訂單資訊模型泛型集合物件</returns>
        [HttpPost()]
        public IEnumerable<MdRmsBooking> GetBooks([FromBody] string[] rvList)
        {
            return BlBooking.GetBookings(rvList);
        }

        /// <summary>
        /// 更新RMS訂單資訊訊息唯一碼
        /// </summary>
        /// <param name="obj">RMS訊息唯一碼資料</param>
		/// <returns>系統規範訊息物件</returns>
        [HttpPut()]
        public MdApiMessage UpdateMsgUid([FromBody] MdRmsMessageUid obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlBooking.UpdateMessageUniqueId(obj);

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
