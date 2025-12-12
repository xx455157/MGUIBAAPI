#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using System.Collections.Generic;
using GUICore.Web.Attributes;
using GUIStd.DAL.AllNewHTL.Models.Private.Reserve;
using GUIStd.DAL.AllNewHTL.Models.Private.Restaurant;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Restaurant
{
	/// <summary>
	/// 餐廳取餐資料資料控制器
	/// </summary>
	[Route("htlpre/restaurant/[controller]")]
	public class OrdersController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlOrders BlOrders => new BlOrders(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得取餐資料
        /// </summary>
        /// <param name="posId">廳別</param>
        /// <param name="posDate">營業日期</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet]
		public IEnumerable<MdMealStatus> GetPosDateTableData([RequiredFromQuery] string posId,
            [RequiredFromQuery] string posDate)
		{
			return BlOrders.GetMealStatusData(posId, posDate);
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		#endregion
	}
}
