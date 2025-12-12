#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vHTVCM12畫面資料模型類別
    /// </summary>
    public class CmHTVCM12
    {
        #region " 共用屬性 "

        /// <summary>
        /// 應開發票金額
        /// </summary>
        public decimal ChargeAmount { get; set; }

        /// <summary>
        /// 已開發票資料
        /// </summary>        
        public IEnumerable<MdInvoice> Invoices { get; set; }

        #endregion
    }
}
