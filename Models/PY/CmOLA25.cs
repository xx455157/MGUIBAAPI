#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.PY.Models;

#endregion

namespace MGUIBAAPI.Models.PY
{
    /// <summary>
    /// 
    /// </summary>
    public class CmOLA25
    {
        #region " 共用屬性 "

        /// <summary>
        /// 假單申請關帳日
        /// </summary>
        public string closeDate { get; set; }

        /// <summary>
        /// 加班轉休請假代號
        /// </summary>
        public MdAttendanceCode OvertimeCode { get; set; }

        /// <summary>
        /// 加班轉薪請假代號
        /// </summary>
        public MdAttendanceCode OvertimeCodeL { get; set; }

        /// <summary>
        /// 撤銷原因
        /// </summary>
        public IEnumerable<MdReasonCode> reasons { get; set; }

        /// <summary>
        /// 員工資訊
        /// </summary>
        public MdEmployee employee { get; set; }

        #endregion
    }
}
