#region " 匯入的名稱空間：Framework "

using System;

#endregion

namespace MGUIBAAPI.Models.HTLPRE.vHTRGM09
{
    /// <summary>
    /// 訂單詳情資料模型
    /// </summary>
    public class MdOrderDetail
    {
        #region " 客戶資訊 "

        /// <summary>
        /// 旅客姓名
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 訂房號碼
        /// </summary>
        public string ReservationNo { get; set; }

        /// <summary>
        /// 團體名稱
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 國籍
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region " 住宿資訊 "

        /// <summary>
        /// 入住日期
        /// </summary>
        public string CheckInDate { get; set; }

        /// <summary>
        /// 退房日期
        /// </summary>
        public string CheckOutDate { get; set; }

        /// <summary>
        /// 房型
        /// </summary>
        public string RoomType { get; set; }

        /// <summary>
        /// 房號
        /// </summary>
        public string RoomNo { get; set; }

        #endregion

        #region " 訂單資訊 "

        /// <summary>
        /// 訂單來源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 是否已付訂金
        /// </summary>
        public bool IsDepositPaid { get; set; }

        /// <summary>
        /// 業務
        /// </summary>
        public string Sales { get; set; }

        #endregion

        #region " 費用資訊 "

        /// <summary>
        /// 總金額
        /// </summary>
        public decimal TotalAmount { get; set; }

        #endregion
    }
}
