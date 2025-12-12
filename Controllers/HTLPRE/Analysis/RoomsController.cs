#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.RoomsAnalysis;

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

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
