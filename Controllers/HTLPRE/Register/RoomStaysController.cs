#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Register;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Register
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("htlpre/register/[controller]")]
	public class RoomStaysController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRoomStays BlRoomStays => new BlRoomStays(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得本日變化預測表
        /// </summary>

        /// <returns>住房率模型泛型集合物件</returns>
        [HttpGet("dailyguestlist")]
		public MdStatusRoomLists DailyGuestList()
		{
			return BlRoomStays.DailyGuestList();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
