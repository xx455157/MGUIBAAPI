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
    /// vHTRVM01明細畫面輔助資料模型類別
    /// </summary>
    public class CmHTRVM01_d
    {
        #region " 共用屬性 "

        /// <summary>
        /// 會計日期
        /// </summary>
        public string BKDate { get; set; }

        /// <summary>
        /// 合約公司集合
        /// </summary>
        public IEnumerable<MdCompany_r> Companies { get; set; }

        /// <summary>
        /// 房型
        /// </summary>
        public IEnumerable<MdCode> RoomTypes { get; set; }

        /// <summary>
        /// 服務
        /// </summary>
        public IEnumerable<MdService> Services { get; set; }

        /// <summary>
        /// 訂房類別
        /// </summary>
        public IEnumerable<MdCode> RVTypes { get; set; }

        /// <summary>
        /// 業務員
        /// </summary>
        public IEnumerable<MdCode> SalesInfo { get; set; }

        /// <summary>
		/// 國籍
		/// </summary>
		public IEnumerable<MdCode> OriginInfo { get; set; }

        /// <summary>
        /// 客戶類別(業務碼)
        /// </summary>
        public IEnumerable<MdCode> SourceInfo { get; set; }        

        #endregion
    }
}
