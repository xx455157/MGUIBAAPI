#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;
using GUIStd.BLL.AllNewHTL.Private;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Analysis
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htlpre/analysis/[controller]")]
	public class RoomsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRoomsAnalysis BlRoomsAnalysis => new BlRoomsAnalysis(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得日期範圍的住房率資料
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="buildingsId">館別</param> 
        /// <returns>住房率模型泛型集合物件</returns>
        [HttpPost("getocprate/{startDate}/{endDate}")]
		public IEnumerable<MdRoomsOcpRate> GetDataForRoomOcp(string startDate,string endDate,  [FromBody] string[]  buildingsId)
		{
			return BlRoomsAnalysis.GetDataForRoomOcp(startDate, endDate, buildingsId);
		}

        /// <summary>
        /// 取得平面圖畫面資料
        /// </summary>
        /// <returns>平面圖資料模型集合</returns>
        /// <remarks>
        /// 此 API 用於提供房間平面圖所需的房間狀態、入住資訊等資料。
        /// 包含樓層、房號、房型、入住/預訂客人、入離店日期等資訊。
        /// </remarks>
        [HttpGet("floorplan")]
        public IEnumerable<MdFloorPlan> GetFloorPlan()
        {
            return BlRoomsAnalysis.GetFloorPlanData();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
