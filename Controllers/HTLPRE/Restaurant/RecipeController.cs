#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using System.Collections.Generic;
using GUIStd.DAL.AllNewHTL.Models.Private.Restaurant;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Restaurant
{
    /// <summary>
    /// 餐廳食譜資料控制器
    /// </summary>
    [Route("htlpre/restaurant/[controller]")]
    public class RecipeController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRecipe BlRecipe => new BlRecipe(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得食譜
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="posId">廳別</param>
        /// <returns></returns>
        [HttpGet("{startDate}/{endDate}/{posId}")]
        public IEnumerable<MdRecipe> GetData(string startDate, string endDate, string posId)
        {
            return BlRecipe.GetData(startDate, endDate, posId);
        }
        /// <summary>
        /// 取得餐飲分析資料
        /// </summary>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns></returns>
        [HttpGet("{startDate}/{endDate}")]
        public IEnumerable<MdBills> GetData(string startDate, string endDate)
        {
            return BlRecipe.GetData(startDate, endDate);
        }
        #endregion
    }
}
