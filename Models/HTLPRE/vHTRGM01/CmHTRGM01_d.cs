#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewHTL.Models.Private.Register;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vHTRGM01_d畫面資料模型類別
    /// </summary>
    public class CmHTRGM01_d
    {
        #region " 共用屬性 "

        /// <summary>
        /// 訂房資料
        /// </summary>
        public MdRVData RVDetail { get; set; }


        /// <summary>
        /// 住客資料
        /// </summary>
        public IEnumerable<MdVisitData> VSDetails { get; set; }

        #endregion
    }
}
