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
using GUIStd.Extensions;
using GUICore.Web.Attributes;
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
    /// <summary>
    /// 敦謙自助報到機 - 付款資料控制器
    /// 提供建立付款資料功能
    /// </summary>
    [Route("general/DonQianKiosk/[controller]")]
    public class PaymentController : GUIAppWSController
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化付款資料控制器
        /// </summary>
        public PaymentController()
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
        /// 建立付款資料
        /// 旅客透過自助報到機建立付款紀錄（如信用卡付款、現金付款等）
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="booking_number">訂單號碼或住宿碼（必填）</param>
        /// <param name="odj">付款資料物件，必須包含 payment_id</param>
        /// <returns>成功時回傳系統規範訊息物件，若 payment_id 為 0 或發生例外則回傳 null</returns>
        /// <remarks>
        /// 注意：
        /// 1. payment_id 為 0 時回傳 null
        /// 2. 發生例外時回傳 null
        /// </remarks>
        [HttpPost()]
        public MdApiMessage Insert(string domain, [RequiredFromQuery] string booking_number, [FromBody] MdKioskPayment odj)
        {
            // 驗證必填欄位：payment_id 不可為 0
            if (odj.payment_id == 0) return null;

            try
            {
                // 呼叫商業邏輯層建立付款資料
                int _result = BlKiosk.InsertPayment(domain, booking_number, odj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 發生例外時回應前端 null（依對方需求）
                return null;
            }
        }

        #endregion
    }
}
