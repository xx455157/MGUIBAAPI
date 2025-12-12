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
    /// 敦謙自助報到機 - 房卡管理控制器
    /// 提供房卡號碼寫入功能
    /// </summary>
    [Route("general/DonQianKiosk/[controller]")]
    public class CardController : GUIAppWSController
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化房卡管理控制器
        /// </summary>
        public CardController()
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
        /// 寫入房卡號碼
        /// 將房卡號碼綁定至訂單，用於自助報到時發放房卡
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="order_number">訂單號碼或住宿碼（必填）</param>
        /// <param name="card_number">房卡號碼（必填）</param>
        /// <returns>成功時回傳系統規範訊息物件，失敗時回傳 null</returns>
        /// <remarks>
        /// 注意：發生例外時回傳 null
        /// </remarks>
        [HttpPost()]
        public MdApiMessage Insert(string domain, [RequiredFromQuery] string order_number, [RequiredFromQuery] string card_number)
        {
            try
            {
                // 呼叫商業邏輯層寫入房卡號碼
                int _result = BlKiosk.InsertHPCard(domain, order_number, card_number);

                // 回應前端寫入成功訊息
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
