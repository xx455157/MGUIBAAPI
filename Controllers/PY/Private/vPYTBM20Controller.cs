#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models.Private.PYTBM20;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vPYTBM20 支薪代碼會計科目私用控制器（明細／篩選輔助資料批次載入，比照 vPYTBM02）
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYTBM20Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPCA BlPCA => mBlPCA = mBlPCA ?? new BlPCA(ClientContent);
        private BlPCA mBlPCA;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得篩選／明細首次載入所需輔助資料（公司、PC、SINI／PD 下拉）
        /// </summary>
        /// <param name="isStateAdd">是否為新增（或複製）作業狀態，與 vQPattern 一致供後續擴充條件查詢</param>
        [HttpGet("paged")]
        public MdPYTBM20_h GetUIDataForDetail([FromQuery] bool isStateAdd) =>
            BlPCA.GetUIDataForDetail(isStateAdd);

        #endregion
    }
}
