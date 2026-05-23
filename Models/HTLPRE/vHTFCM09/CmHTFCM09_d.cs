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
    /// vHTFCM09轉帳明細畫面資料模型類別    
    /// </summary>
    public class CmHTFCM09_d
    {
        #region " 共用屬性 "

        /// <summary>
        /// 已入住房號集合
        /// </summary>
        public IEnumerable<MdCode> CheckInRoomNos { get; set; }

        /// <summary>
        /// 帳單(帳夾)資料
        /// </summary>        
        public IEnumerable<MdHouseFolio> Folios { get; set; }

        #endregion
    }
}
