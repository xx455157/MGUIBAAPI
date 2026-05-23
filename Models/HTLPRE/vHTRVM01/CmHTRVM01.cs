#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vHTRVM01畫面輔助資料模型類別
    /// </summary>
    public class CmHTRVM01 
    {
        #region " 共用屬性 "

        /// <summary>
        /// 會計日期
        /// </summary>
        public string BKDate { get; set; }

        /// <summary>
        /// 查詢條件
        /// </summary>
        public IEnumerable<MdCode> SelectConditions { get; set; }

        /// <summary>
        /// 合約公司集合
        /// </summary>
        public IEnumerable<MdCompany_r> Companies { get; set; }

        #endregion
    }
}
