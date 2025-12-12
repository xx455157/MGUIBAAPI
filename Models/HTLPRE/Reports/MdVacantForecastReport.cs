using System;

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// 空房庫存預測表資料模型
    /// </summary>
    public class MdVacantForecastReport
    {
        /// <summary>
        /// 日期 (MM/dd)
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string Weekday { get; set; }

        /// <summary>
        /// 空房總計
        /// </summary>
        public int TotalVacant { get; set; }

        /// <summary>
        /// 空房百分比
        /// </summary>
        public int VacantPercentage { get; set; }

        /// <summary>
        /// SWK 庫存
        /// </summary>
        public int SwkStock { get; set; }

        /// <summary>
        /// SWK 百分比
        /// </summary>
        public int SwkPercentage { get; set; }

        /// <summary>
        /// SWT 庫存
        /// </summary>
        public int SwtStock { get; set; }

        /// <summary>
        /// SWT 百分比
        /// </summary>
        public int SwtPercentage { get; set; }

        /// <summary>
        /// DWT 庫存
        /// </summary>
        public int DwtStock { get; set; }

        /// <summary>
        /// DWT 百分比
        /// </summary>
        public int DwtPercentage { get; set; }

        /// <summary>
        /// DWK 庫存
        /// </summary>
        public int DwkStock { get; set; }

        /// <summary>
        /// DWK 百分比
        /// </summary>
        public int DwkPercentage { get; set; }

        /// <summary>
        /// 加床庫存
        /// </summary>
        public int ExtraBedStock { get; set; }

        /// <summary>
        /// 加床百分比
        /// </summary>
        public int ExtraBedPercentage { get; set; }

        /// <summary>
        /// 散客
        /// </summary>
        public int Fit { get; set; }

        /// <summary>
        /// 團客
        /// </summary>
        public int Git { get; set; }

        /// <summary>
        /// OOO
        /// </summary>
        public int Ooo { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 有效
        /// </summary>
        public int Valid { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        public int Cancelled { get; set; }

        /// <summary>
        /// 確認
        /// </summary>
        public int Confirmed { get; set; }

        /// <summary>
        /// 已付訂
        /// </summary>
        public int Paid { get; set; }

        /// <summary>
        /// OOS
        /// </summary>
        public int Oos { get; set; }

        /// <summary>
        /// 可售房
        /// </summary>
        public int AvailableRooms { get; set; }

        /// <summary>
        /// 總房數
        /// </summary>
        public int TotalRooms { get; set; }

        /// <summary>
        /// 空房率(可售房)
        /// </summary>
        public int SalesRateAvailable { get; set; }

        /// <summary>
        /// 空房率(總房數)
        /// </summary>
        public int SalesRateTotal { get; set; }
    }
}

