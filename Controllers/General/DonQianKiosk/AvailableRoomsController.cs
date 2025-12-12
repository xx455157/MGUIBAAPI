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
using GUICore.Web.Attributes;
#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
    /// <summary>
    /// 敦謙自助報到機 - 可訂房型控制器
    /// 提供查詢可用房型及住宿價格功能
    /// </summary>
    [Route("general/DonQianKiosk/")]
    public class AvailableRoomsController : GUIAppWSController
    {
        #region " 建構子 "

        /// <summary>
        /// 建構子：初始化可訂房型控制器
        /// </summary>
        public AvailableRoomsController()
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

        /// <summary>
        /// 取得可訂房型及住宿價格
        /// 查詢指定日期區間內的可用房型及對應價格資訊
        /// </summary>
        /// <param name="domain">飯店代號（選填）</param>
        /// <param name="start_date">入住日期（必填）格式：YYYYMMDD</param>
        /// <param name="end_date">退房日期（必填）格式：YYYYMMDD</param>
        /// <param name="isDUS">查詢類型（必填）
        ///     1: 一般住宿查詢
        ///     2: DUS（Day Use Service，鐘點房）查詢
        /// </param>
        /// <returns>可訂房型及價格資料集合</returns>
        [HttpGet("available_rooms")]
        public IEnumerable<MdKioskAvailableRooms> GetRow
            (string domain, [RequiredFromQuery] string start_date, [RequiredFromQuery] string end_date, [RequiredFromQuery] string isDUS)
        {
            // 呼叫商業邏輯層查詢可訂房型及價格資訊
            return BlKiosk.GetAvailableRooms(domain, start_date, end_date, isDUS);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        // 此控制器無異動功能

        #endregion
    }
}
