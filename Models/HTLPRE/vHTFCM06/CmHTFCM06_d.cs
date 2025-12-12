#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Accounting;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vHTFCM06帳務明細畫面資料模型類別
    /// </summary>
    public class CmHTFCM06_d
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
        /// 會計科目
        /// </summary>
        public IEnumerable<MdCode> Accounts { get; set; }

        #endregion
    }
}
