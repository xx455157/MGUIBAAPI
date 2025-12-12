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
    /// 敦謙自助報到機 - 訂單取消控制器
    /// 提供訂單取消功能
    /// </summary>
    [Route("general/DonQianKiosk/[controller]")]
    public class BookingCxnlController : GUIAppWSController
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化訂單取消控制器
        /// </summary>
        public BookingCxnlController()
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
        /// 取消訂單
        /// 旅客透過自助報到機取消已預訂的訂單
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="order_number">訂單號碼或住宿碼（必填）</param>
        /// <returns>成功時回傳系統規範訊息物件，失敗時回傳 null</returns>
        /// <remarks>
        /// 注意：發生例外時回傳 null
        /// </remarks>
        [HttpPost()]
        public MdApiMessage Insert(string domain, [RequiredFromQuery] string order_number)
        {
            try
            {
                // 呼叫商業邏輯層執行訂單取消作業
                int _result = BlKiosk.BookingCxnl(domain, order_number);

                // 回應前端取消成功訊息
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
