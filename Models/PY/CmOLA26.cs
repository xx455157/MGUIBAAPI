#region " 匯入的名稱空間：Framework "

using System.Collections;
using System.Collections.Generic;


#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.PY.Models;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Models.PY
{
    /// <summary>
    /// 
    /// </summary>
    public class CmOLA26
    {
        #region " 共用屬性 "

        /// <summary>
        /// 假單申請關帳日
        /// </summary>
        public string closeDate { get; set; }

        /// <summary>
        /// 下班超過容許時間是否可以打卡
        /// </summary>
        public bool clockOutNotOverLimit { get; set; }

        /// <summary>
        /// 忘卡考勤代碼
        /// </summary>
        public IEnumerable<MdAttendanceCode> attendCodes { get; set; }

        /// <summary>
        /// 忘卡原因
        /// </summary>
        public IEnumerable<MdReasonCode> reasons { get; set; }

        /// <summary>
        /// 員工資訊
        /// </summary>
        public MdEmployee employee { get; set; }

        #endregion
    }
}
