#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewHTL.Models.Private.Accounting;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vHTFCM06畫面資料模型類別
    /// </summary>
    public class CmHTFCM06
    {
        #region " 共用屬性 "

        /// <summary>
        /// 住客項目模型for帳單
        /// </summary>
        public MdFolioVisitData FolioVSData { get; set; }

        /// <summary>
        /// 帳單(帳夾)資料
        /// </summary>        
        public IEnumerable<MdHouseFolio> Folios { get; set; }

        #endregion
    }
}
