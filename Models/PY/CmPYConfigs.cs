#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewPY.Models.Private.PersonalAttend;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewPY.Models;

#endregion

namespace MGUIBAAPI.Models.PY
{
    /// <summary>
    /// PY設定頁面統一資料模型 (類似 CmPOSSetup)
    /// </summary>
    public class CmPYConfigs
    {
        #region " 共用屬性 "

        /// <summary>
        /// 考勤資料下拉選項 (AB表，<see cref="MdAttendCode"/> 序列化含 attendCode／attendName／attendType／unitName 等)
        /// </summary>
        public IEnumerable<MdAttendCode> AttendanceOptions { get; set; }

        /// <summary>
        /// 公司別下拉選項 (ARTHGUI A01表)
        /// </summary>
        public IEnumerable<MdCode> CompanyOptions { get; set; }

        /// <summary>
        /// 兼職特休假：核給不足1日處理方式選項 (SINI PYP60_LessThanOneDay2)
        /// </summary>
        public IEnumerable<MdCode> LessThanOneDay2Options { get; set; }


        #endregion
    }
} 