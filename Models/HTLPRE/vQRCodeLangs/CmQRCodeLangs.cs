#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
    /// <summary>
    /// vQRCodeLangs畫面輔助資料模型類別
    /// </summary>
    public class CmQRCodeLangs
    {
        #region " 共用屬性 "

        /// <summary>
        /// 廳別
        /// </summary>
        public IEnumerable<MdCode> PosIds { get; set; }

        /// <summary>
        /// 資料類別
        /// </summary>
        public IEnumerable<MdCode> DataTypes { get; set; }

        /// <summary>
        /// 支援語系
        /// </summary>
        public IEnumerable<MdCode> Langs { get; set; }

        #endregion
    }
}
