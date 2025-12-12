#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUICore.Web.Extensions;
using System;
using GUIStd.DAL.AllNewHTL.Models.Private;
using GUICore.Web.Filters;
using GUIStd.Models;
using GUICore.Web.Attributes;
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
    /// <summary>
    /// 敦謙自助報到機 - 訂房房間明細控制器
    /// 提供更新訂單房間入住資訊功能
    /// </summary>
    [Route("general/DonQianKiosk/booking_room_detail")]
    public class BookingRoomDetailController : GUIAppWSController   
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化訂房房間明細控制器
        /// </summary>
        public BookingRoomDetailController()
        {
            // 改變執行服務的使用者帳號為 KIOSK
            this.WSUser = "KIOSK";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlKiosk BlKiosk => new BlKiosk(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        // 此控制器無查詢功能

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 更新訂單房間入住資訊
        /// 用於自助報到時更新旅客的入住詳細資訊
        /// </summary>
        /// <param name="odj">訂房房間明細資料物件，包含房間號碼、旅客資訊等</param>
        /// <returns>成功時回傳系統規範訊息物件，失敗時回傳 null</returns>
        /// <remarks>
        /// 注意：發生例外時回傳 null
        /// </remarks>
        [HttpPut()]
        public MdApiMessage Update([FromBody] MdBookingRoomDetail odj)
        {
            try
            {
                // 呼叫商業邏輯層更新訂房房間明細資料
                int _result = BlKiosk.BookingRoomDetail(odj);

                // 回應前端更新成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch
            {
                // 發生例外時回應前端 null（依對方需求）
                return null;
            }
        }

        #endregion
    }
}
