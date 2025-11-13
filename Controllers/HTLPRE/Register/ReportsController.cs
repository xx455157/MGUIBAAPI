#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "


using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Register;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;
using MGUIBAAPI.Models.HTLPRE;
using System.Collections.Generic;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 報表資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class ReportsController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlRoomsAnalysis BlRoomsAnalysis => new BlRoomsAnalysis(ClientContent);
        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPreCheckIn BlPreCheckIn => new BlPreCheckIn(ClientContent);
        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得平面圖畫面資料
        /// </summary>
        /// <returns>平面圖資料集合</returns>
        [HttpGet("FloorPlan")]
		public IEnumerable<MdFloorPlan> GetFloorPlan()
		{
                return BlRoomsAnalysis.GetFloorPlanData();

		}

		#endregion
	}
}
