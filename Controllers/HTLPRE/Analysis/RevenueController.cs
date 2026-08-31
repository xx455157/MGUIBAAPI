#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.RevenueAnalysis;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Analysis
{
    /// <summary>
    /// 營收分析資料控制器
    /// </summary>
    [Route("htlpre/analysis/[controller]")]
    public class RevenueController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlRevenueAnalysis BlRevenueAnalysis => new BlRevenueAnalysis(ClientContent);

        #endregion

        #region " 私用函式 "

        /// <summary>
        /// 讀取可選 Request Body 為篩選模型（空 body 視同預設篩選）
        /// </summary>
        private async Task<TFilter> ReadFilterOrDefaultAsync<TFilter>() where TFilter : class, new()
        {
            var _body = await Request.ReadMultipleRawBodyStringAsync();
            if (string.IsNullOrWhiteSpace(_body))
                return new TFilter();

            return JsonConvert.DeserializeObject<TFilter>(_body) ?? new TFilter();
        }

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得全館每日營收彙總
        /// </summary>
        [HttpPost("summary/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRevenueSummary(
            string startDate,
            string endDate)
        {
            var _filter = await ReadFilterOrDefaultAsync<MdRevenueSummaryFilter>();
            return Content(BlRevenueAnalysis.GetRevenueSummary(startDate, endDate, _filter), "application/json");
        }

        /// <summary>
        /// 取得客房營收聚合資料
        /// </summary>
        [HttpPost("roomrevenue/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRoomRevenue(
            string startDate,
            string endDate)
        {
            var _filter = await ReadFilterOrDefaultAsync<MdRoomRevenueFilter>();
            return Content(BlRevenueAnalysis.GetRoomRevenue(startDate, endDate, _filter), "application/json");
        }

        /// <summary>
        /// 取得餐飲營收資料
        /// </summary>
        [HttpPost("fnbrevenue/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFnBRevenue(
            string startDate,
            string endDate)
        {
            var _filter = await ReadFilterOrDefaultAsync<MdFnBRevenueFilter>();
            return Content(BlRevenueAnalysis.GetFnBRevenue(startDate, endDate, _filter), "application/json");
        }

        /// <summary>
        /// 取得預算與實際對比資料
        /// </summary>
        [HttpPost("budget/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetBudgetComparison(
            string startDate,
            string endDate)
        {
            var _filter = await ReadFilterOrDefaultAsync<MdBudgetComparisonFilter>();
            return Content(BlRevenueAnalysis.GetBudgetComparison(startDate, endDate, _filter), "application/json");
        }

        /// <summary>
        /// 取得營收趨勢資料
        /// </summary>
        [HttpPost("trend/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRevenueTrend(
            string startDate,
            string endDate)
        {
            var _filter = await ReadFilterOrDefaultAsync<MdRevenueTrendFilter>();
            return Content(BlRevenueAnalysis.GetRevenueTrend(startDate, endDate, _filter), "application/json");
        }

        #endregion
    }
}
