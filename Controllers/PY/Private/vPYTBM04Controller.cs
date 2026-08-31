#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PYTBM04;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vPYTBM04 私用控制器（頁籤 UIData 及日後 GetHelp 下拉；路由 py/private/vpytbm04；比照 vQPattern 私用 API 分工）。
    /// 級距 CRUD、投保類別、職災列表、提撥比例列表／異動見 BracketController（py/bracket）。
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYTBM04Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPYTBM04 BlMain => mBlMain = mBlMain ?? new BlPYTBM04(ClientContent);
        private BlPYTBM04 mBlMain;

        #endregion

        #region " 共用函式 - 查詢資料（頁籤 UIData） "

        /// <summary>
        /// 取得勞保籤 UI 初始化資料（SINI 基本工資、勞保費率等）
        /// </summary>
        /// <returns>勞保頁籤 UI 資料模型</returns>
        [HttpGet("query/uidata/labor")]
        public MdPYTBM04Labor_h GetLaborUIData() => BlMain.GetLaborUIData();

        /// <summary>
        /// 取得健保籤 UI 初始化資料（SINI 補充保費費率、費基上下限）
        /// </summary>
        /// <returns>健保頁籤 UI 資料模型</returns>
        [HttpGet("query/uidata/health")]
        public MdPYTBM04Health_h GetHealthUIData() => BlMain.GetHealthUIData();

        /// <summary>
        /// 取得職災籤 UI 資料（Accident 級距上下限）
        /// </summary>
        /// <returns>職災頁籤 UI 資料模型</returns>
        [HttpGet("query/uidata/occaccident")]
        public MdPYTBM04OccAccident_h GetOccAccidentUIData() => BlMain.GetOccAccidentUIData();

        #endregion
    }
}
