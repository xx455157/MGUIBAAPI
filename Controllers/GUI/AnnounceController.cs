#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
	/// <summary>
	/// 【需經驗證】公告資料資料控制器
	/// </summary>
	[Route("gui/[controller]")]
	public class AnnounceController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA86 BlA86 => new BlA86(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "
        /// <summary>
        /// 取得公告
        /// </summary>
        /// <param name="categoryId">類別</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="departmentId">部門別</param>
        /// <param name="content">內容</param>
        /// <param name="pageNo">分頁</param>
        /// <returns></returns>
     
        [HttpGet("pages/{pageNo}")]
        public MdAnnounces_p GetData([FromQuery] string categoryId, [FromQuery] string startDate,
            [FromQuery] string endDate, [FromQuery] string departmentId, [FromQuery] string content,
            [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlA86.GetData(categoryId , startDate, endDate , departmentId , content, ControlName, pageNo );
        }
        #endregion
    }
}
