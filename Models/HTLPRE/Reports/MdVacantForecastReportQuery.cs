using System;
using System.Collections.Generic;

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// 空房庫存預測表查詢參數
    /// </summary>
    public class MdVacantForecastReportQuery
    {
        /// <summary>
        /// 日期範圍 (yyyy-MM-dd)
        /// </summary>
        public List<string> DateRange { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string Status { get; set; }
    }
}

