#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.HT2020;
using GUIStd.DAL.AllNewGUI.Models;
using GUICore.Web.Attributes;
using System;
using GUIStd.Models;
using GUIStd.DAL.HT2020.Models;
using GUICore.Web.Extensions;
using GUIStd.BLL.HT2020.Private;
using GUIStd.DAL.HT2020.Models.Private.Performances;

#endregion

namespace MGUIBAAPI.Controllers.HTL
{
	/// <summary>
	/// 業績資料控制器
	/// </summary>
	[Route("htl/[controller]")]
	public class PerformancesController : GUIAppAuthController
	{
        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得業績資料
        /// </summary>
        /// <param name="state">查詢狀態</param>
        /// <param name="hotelId">館別代碼</param>
        /// <returns>取得欄位資料字串</returns>
        [HttpGet]
		public IEnumerable<MdPerformance> GetPerformanceData([RequiredFromQuery] string state, [FromQuery] string hotelId)
		{
			return new BlPerformance(ClientContent).GetDataForPerformance(state, hotelId, CurrentLang);
		}

        /// <summary>
        /// 取得業績三年比較資料
        /// </summary>
        /// <param name="state">查詢狀態</param>
        /// <param name="hotelId">館別代碼</param>
        /// <returns>取得欄位資料字串</returns>
        [HttpGet("compare")]
        public IEnumerable<MdPerformanceCompare> GetPerformanceCompareData([RequiredFromQuery] string state, [FromQuery] string hotelId)
        {
            return new BlPerformance(ClientContent).GetDataForPerformanceCompare(state, hotelId, CurrentLang);
        }

        #endregion

        #region " 共用函式 - 異動資料 "


        #endregion
    }
}
