#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.ABM01;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vABM01 員工考勤資料管理控制器（Private；僅頁面輔助資料）
    /// </summary>
    [Route("py/private/[controller]")]
    public class vABM01Controller : GUIAppAuthController
    {
        private BlABM01 BlMain => mBlMain = mBlMain ?? new BlABM01(ClientContent);
        private BlABM01 mBlMain;

        /// <summary>
        /// 取得 vABM01 主頁／查詢條件輔助資料（比照 vQPattern page）
        /// </summary>
        [HttpGet("page")]
        public MdABM01_h GetUIData() => BlMain.GetUIData();

        /// <summary>
        /// 取得明細頁面首次載入所需輔助資料（公司、考勤、扣抵、部門；比照 vQPattern／vPYTBM02）
        /// </summary>
        /// <param name="isStateAdd">是否為新增作業狀態</param>
        [HttpGet("paged")]
        public MdABM01_h GetUIDataForDetail([FromQuery] bool isStateAdd) =>
            BlMain.GetUIDataForDetail(isStateAdd, ControlName);
    }
}
