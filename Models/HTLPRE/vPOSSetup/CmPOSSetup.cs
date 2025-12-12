#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Configs;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vCmPOSSetup畫面輔助資料模型類別
    /// </summary>
    public class CmPOSSetup
    {
        #region " 共用屬性 "

        /// <summary>
        /// 會計日期
        /// </summary>
        public string BkDate { get; set; }

        /// <summary>
        /// 餐廳別
        /// </summary>
        public IEnumerable<MdCode> PosIds { get; set; }

        /// <summary>
        /// 客房部出納科目代碼
        /// </summary>
        public IEnumerable<MdCode> FrontDeskAccts { get; set; }

        /// <summary>
        /// 付款類別
        /// </summary>
        public IEnumerable<MdCode> PayTypes { get; set; }

        /// <summary>
        /// KOT04標籤紙列印欄位選項
        /// </summary>
        public IEnumerable<MdCode> Kot04Fields { get; set; }

        /// <summary>
        /// KOT10標籤紙列印欄位選項
        /// </summary>
        public IEnumerable<MdCode> Kot10Fields { get; set; }

        /// <summary>
        /// 通用規則組態設定
        /// </summary>
        public MdDefaultConfig DefaultSettings { get; set; }

        #endregion
    }
}
