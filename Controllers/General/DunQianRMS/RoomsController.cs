#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL.Client.DunQian;
using GUIStd.DAL.AllNewHTL.Models.Client.DunQian;

#endregion

namespace MGUIBAAPI.Controllers.General.DunQianRMS
{
    /// <summary>
    /// 敦謙RMS房型控制器
    /// </summary>
    [Route("general/dunqianrms/[controller]")]
    public class RoomsController : GUIAppWSController
    {
        #region " 建構子：欲自行指定使用者帳號時才需加入 "
        
        // 建構子
        public RoomsController()
        {
            // 改變執行服務的使用者帳號
            this.WSUser = "DQRMS";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRoom BlRoom => new BlRoom(ClientContent);

        #endregion

        #region " 授權存取 API "

        /// <summary>
        /// 取得待上傳RMS房型資料
        /// </summary>
        /// <returns>RMS房型資料泛型集合物件</returns>
        [HttpGet()]
        public IEnumerable<MdRmsRoomType> GetData()
        {
            return BlRoom.GetData();
        }


        /// <summary>
        /// 更新RMS房型上傳訊息唯一碼
        /// </summary>
        /// <param name="obj">RMS訊息唯一碼資料</param>
		/// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage UpdateMsgUid([FromBody]MdRmsMessageUid obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlRoom.UpdateMessageUniqueId(obj);

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
