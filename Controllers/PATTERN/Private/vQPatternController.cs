#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.GUI.Private;
using GUIStd.DAL.GUI.Models.Private.vQPattern;

#endregion

namespace MGUIBAAPI.Controllers.Pattern.Private
{
    /// <summary>
    /// 【需經驗證】PTN私用客戶資料處理控制器
    /// </summary>
    [Route("pattern/private/[controller]")]
    public class vQPatternController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlvQPattern BlMain => new BlvQPattern(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        ///// <summary>
        ///// 取得主頁首次載入所需的所有輔助資料
        ///// </summary>
        ///// <returns>私有頁面輔助資料模型物件</returns>
        //[HttpGet("page")]
        //public MdQPattern_h GetUIData()
        //{
        //    return BlMain.GetUIData();
        //}

        /// <summary>
        /// 取得明細頁面首次載入所需的所有輔助資料
        /// </summary>
        /// <param name="isStateAdd">是否為新增作業狀態</param>
        /// <returns>私有頁面輔助資料模型物件</returns>
        [HttpGet("paged")]
        public MdQPatternd_h GetUIDataForDetail([FromQuery] bool isStateAdd) => 
            BlMain.GetUIDataForDetail(isStateAdd);

        #endregion
    }
}
