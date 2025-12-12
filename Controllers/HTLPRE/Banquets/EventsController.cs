#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Banquet;
using GUIStd.BLL.AllNewHTL;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Banquets
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("htlpre/banquets/[controller]")]
    public class EventsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlBanquet BlBanquet => new BlBanquet(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 以日期取得當日有效宴會檢曆
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>宴會檢曆模型泛型集合物件</returns>
        [HttpGet("brief/{date}")]
        public IEnumerable<MdBanquetBrief> GetDataByDate(string date)
        {
            return BlBanquet.GetDataByDate(date);
        }

        /// <summary>
        /// 以日期+狀態取得宴會檢曆
        /// </summary>
        /// <param name="date"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("brief/{date}/{status}")]
        public IEnumerable<MdBanquetBrief> GetDataByDate(string date, string status)
        {
            return BlBanquet.GetDataByDate(date, status);
        }

        /// <summary>
        /// 取得訂宴活動類別
        /// </summary>
        /// <returns></returns>
        [HttpGet("eventType")]
        public IEnumerable<MdBanquetEventType> GetDataEventType()
        {
            return BlBanquet.GetDataEventType();
        }

        /// <summary>
        /// 取得廳別底下的宴會場地
        /// </summary>
        /// <param name="posid">餐廳別</param>
        /// <returns></returns>
        [HttpGet("rooms/")]
        public IEnumerable<MdBanquetRoom> GetDataRooms([FromQuery] string posid = "")
        {
            return BlBanquet.GetDataRooms(posid);
        }

        /// <summary>
        /// 取得訂宴業務員
        /// </summary>
        /// <returns></returns>
        [HttpGet("sales/")]
        public IEnumerable<MdBanquetUser> GetDataSales()
        {
            return BlBanquet.GetDataSales();
        }

        /// <summary>
        /// 取得宴會詳細資料
        /// </summary>
        /// <param name="rvno">訂宴RV號碼</param>
        /// <param name="serialNo">訂宴流水號</param>
        /// <returns></returns>
        [HttpGet("{rvno}")]
        public MdBanquet GetRow(string rvno, [FromQuery] string serialNo = "A")
        {
            return BlBanquet.GetRow(rvno, serialNo);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
