#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Reserve;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Register
{
    /// <summary>
    /// 房間資料控制器
    /// </summary>
    [Route("htlpre/Reservations/[controller]")]
    public class RoomBlocksController : GUIAppAuthController
    {
        #region " 私用屬性 "
                        
        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRoomBlocks BlRoomBlocks => new BlRoomBlocks(ClientContent);

        #endregion

        #region " 共用函式 - 取得房間資料 "        

        /// <summary>
		/// 取得符合條件的房間資料
		/// </summary>		
		/// <param name="dates">日期(起)</param>
		/// <param name="datee">日期(迄)</param>		
		/// <param name="roomTypes">房型字串陣列</param>
		/// <returns>房間資料模型物件</returns>
		[HttpPost("query/{dates}/{datee}")]
        public IEnumerable<MdRoomStatus> GetData(string dates, string datee, [FromBody] string[] roomTypes)
        {
            return BlRoomBlocks.GetData(dates, datee, roomTypes);
        }

        /// <summary>
        /// 取得符合條件的房價資料
        /// </summary>
        /// <param name="rvNo">訂房號碼</param>
        /// <param name="oldRoomType">舊房型</param>
		/// <param name="newRoomType">新房型</param>				
        /// <returns>房價資料模型物件</returns>
        [HttpGet("query/roomprice")]
        public MdRoomPrice GetRoomPriceData([FromQuery] string rvNo = "", [FromQuery] string oldRoomType = "", 
            [FromQuery] string newRoomType = "")
        {
            return BlRoomBlocks.GetRoomPriceData(rvNo, oldRoomType, newRoomType);
        }

        #endregion

        #region " 共用函式 - 異動資料 "		

        /// <summary>
		/// 修改房價
		/// </summary>		        
		/// <param name="obj">房價資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("roomprice")]
        public MdApiMessage Update([FromBody] MdRoomPrice obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRoomBlocks.ProcessUpdate(obj);                

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

    }
}
