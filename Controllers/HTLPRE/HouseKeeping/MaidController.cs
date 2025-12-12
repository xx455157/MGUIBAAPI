#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.HouseKeeping;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.HouseKeeping
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("htlpre/housekeeping/[controller]")]
    public class MaidController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHouseKeeping BlHouseKeeping => new BlHouseKeeping(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
        /// 取得房間內入住的旅客資訊與房務服務
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <returns>入住旅客模型泛型集合物件</returns>
        [HttpGet("guest/{roomNo}")]
        public MdRoomHK GetResGuests(string roomNo)
        {
            return BlHouseKeeping.GetResGuests(roomNo);
        }

        /// <summary>
        /// 取得飯店某樓層房間房控狀態
        /// </summary>
        /// <param name="floor">樓層代碼</param>
        /// <param name="maidId">房務員ID</param>
        /// <returns>房間房況模型泛型集合物件</returns>
        [HttpGet("rooms")]
        public IEnumerable<MdRoomHK> GetRooms([FromQuery] string floor = null, [FromQuery] string maidId = null)
        {
            return BlHouseKeeping.GetRooms(floor, maidId);
        }

        /// <summary>
        /// 取得飯店所有樓層
        /// </summary>
        /// <returns>樓層字串陣列</returns>
        [HttpGet("floors")]
        public string[] GetFloors()
        {
            return BlHouseKeeping.GetFloors(CurrentUILang);
        }

        /// <summary>
        /// 抓取飯店樓層資訊，並依照房務人員ID顯示負責樓層
        /// </summary>
        /// <param name="maidId">房務員ID</param>
        /// <returns></returns>
        [HttpGet("floors/{maidId}")]
        public IEnumerable<MdFloor> GetFloors(string maidId)
        {
            return BlHouseKeeping.GetFloors(CurrentUILang, maidId);
        }

        /// <summary>
        /// 取得房況平面圖顏色配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("colors")]
        public IEnumerable<MdRoomColor> GetColors()
        {
            return BlHouseKeeping.GetColors(CurrentUILang);
        }

        /// <summary>
        /// 取得房況指令
        /// </summary>
        /// <param name="type">指令類別，Ex:R、A、S、0等HTRC定義類別</param>
        /// <param name="prefix">指令碼前綴</param>
        /// <returns></returns>
        [HttpGet("instructions/{type}")]
        public IEnumerable<MdRoomInstruction> GetInstructions(string type,[FromQuery] string prefix = "")
        {
            return BlHouseKeeping.GetInstructions(type, prefix);
        }

        /// <summary>
        /// 取得 MobileHTL 飯店所有樓層
        /// </summary>
        /// <returns>樓層字串陣列</returns>
        [HttpGet("mobilehtl/floors")]
        public string[] GetMobileHTLFloors()
        {
            return BlHouseKeeping.GetFloorsForMobileHTL(CurrentUILang);
        }

        /// <summary>
        /// 抓取 MobileHTL 飯店樓層資訊，並依照房務人員ID顯示負責樓層
        /// </summary>
        /// <param name="maidId">房務員ID</param>
        /// <returns></returns>
        [HttpGet("mobilehtl/floors/{maidId}")]
        public IEnumerable<MdFloor> GetMobileHTLFloors(string maidId)
        {
            return BlHouseKeeping.GetFloorsForMobileHTL(CurrentUILang, maidId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 更新房間房況指令
        /// </summary>
        /// <param name="obj">房況指令的泛型集合物件'</param>
        /// <returns></returns>
        [HttpPut("room/instruction")]
        public MdApiMessage UpdateRoomInstruction([FromBody] IEnumerable<MdRoomHK_w> obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHouseKeeping.ProcessUpdateRoomInstruction(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 房間清潔
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("room/clean")]
        public MdApiMessage UpdateRoomClean([FromBody] IEnumerable<MdRoom_w> obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHouseKeeping.ProcessUpdateRoomClean(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
