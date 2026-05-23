#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vHTFCM09畫面輔助資料模型類別
    /// </summary>
    public class CmHTFCM09
    {
        #region " 共用屬性 "

        /// <summary>
        /// 會計日期
        /// </summary>
        public string BKDate { get; set; }

        /// <summary>
        /// 班別
        /// </summary>
        public string SHIFT { get; set; }

        /// <summary>
        /// 帳單類別
        /// </summary>
        public IEnumerable<MdCode> FolioTypes { get; set; }

        /// <summary>
        /// 查詢條件
        /// </summary>
        public IEnumerable<MdCode> SelectConditions { get; set; }

        #endregion
    }
}
