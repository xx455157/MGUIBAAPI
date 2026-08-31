#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PYTBM02;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vPYTBM02 支薪代碼維護私用控制器（明細／篩選輔助資料批次載入，比照 vQPattern）
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYTBM02Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPYTBM02 BlMain => mBlMain = mBlMain ?? new BlPYTBM02(ClientContent);
        private BlPYTBM02 mBlMain;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得明細頁面首次載入所需的所有輔助資料（SINI 三組＋所得類別 PD，單次 QueryMultiple）
        /// </summary>
        /// <param name="isStateAdd">是否為新增（或複製）作業狀態，與 vQPattern 一致供後續擴充條件查詢</param>
        [HttpGet("paged")]
        public MdPYTBM02_h GetUIDataForDetail([FromQuery] bool isStateAdd) =>
            BlMain.GetUIDataForDetail(isStateAdd);

        /// <summary>
        /// 取得「計算設定」：基本／自願提撥／月結所得稅／PYP29／PayCode_PYP53（僅支薪代碼）
        /// </summary>
        [HttpGet("calcsettings")]
        public MdPYTBM02_calcSettings GetCalcPayCodeSettings() =>
            BlMain.GetCalcPayCodeSettings();

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 儲存「計算設定」至 SINI
        /// </summary>
        [HttpPost("calcsettings")]
        public MdApiMessage SaveCalcPayCodeSettings([FromBody] MdPYTBM02_calcSettings body)
        {
            try
            {
                BlMain.SaveCalcPayCodeSettings(body);
                return HttpContext.Response.UpdateSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
