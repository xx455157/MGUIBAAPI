#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;

#endregion

namespace MGUIBAAPI.Controllers.General.Pattern
{
	/// <summary>
	/// 訂房資料控制器
	/// </summary>
	[Route("general/pattern/reservations/[controller]")]
	public class BookingsController : GUIAppAnonAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRoomsAnalysis BlRoomsAnalysis => new BlRoomsAnalysis(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得日期範圍的房型銷售預測數量
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="buildingsId">館別</param>
        /// <returns>日期範圍房型銷售預測數量模型物件集合</returns>
        [HttpPost("query/forecast/{startDate}/{endDate}")]
        public IEnumerable<MdRoomsQty> GetDataForRoomSalesQuantity(string startDate, string endDate, [FromBody] string[] buildingsId)
        {
            return BlRoomsAnalysis.GetDataForRoomSalesQuantity(startDate, endDate, buildingsId);
        }

        #endregion
    }
}
