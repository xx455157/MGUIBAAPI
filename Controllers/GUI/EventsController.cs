#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models.Private.HTL.Events;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class EventsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA90 BlA90 => new BlA90(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得各活動所有資料
        /// </summary>
        /// <param name="eventType">活動代碼(banners,news,hots)</param>
        /// <param name="pageNo">頁碼</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("categories/{eventType}/{pageNo}")]
		public MdEvents_p GetDataByPage(string eventType, int pageNo)
		{
			return BlA90.GetDataByPage(
				ClientContent, eventType, pageNo, "mHTSHM11", ImageReadPath);
		}

		#endregion
	}
}
