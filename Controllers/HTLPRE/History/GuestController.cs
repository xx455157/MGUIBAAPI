#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.History;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Register
{
    /// <summary>
    /// 旅客資料控制器
    /// </summary>
    [Route("htlpre/history/[controller]")]
    public class GuestController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlGuest BlGuest => new BlGuest(ClientContent);

        #endregion

        #region " 共用函式 - 取得旅客資料 "

        /// <summary>
        /// 取得分頁頁次的旅客清單
        /// </summary>        
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="obj">查詢條件物件</param>
        /// <returns>分頁旅客資料模型物件</returns>
        [HttpPost("guestdata/pages/{pageNo}")]
        public MdGuestData_p GetData([DARange(1, int.MaxValue)] int pageNo,[FromBody] MdGuestData_q obj)
        {
            return BlGuest.GetDataByPage(obj: obj, currentLang: CurrentLang, funcName: ControlName, pageNo: pageNo);            
		}

        /// <summary>
        /// 取得唯一的旅客資料
        /// </summary>
        /// <param name="guestNo">旅客號碼路徑參數</param>
        /// <returns>旅客資料模型物件</returns>
        [HttpGet("{guestNo}")]
        public MdGuestData_d GetRow(string guestNo)
        {
            return BlGuest.GetGuestData(guestNo);
        }
       
		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 修改資料
		/// </summary>		        
		/// <param name="obj">旅客資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("guestdata")]
        public MdApiMessage Update([FromBody] MdGuestData_d obj)
        {
            try
            {                
                // 呼叫商業元件執行修改作業
                int _result = BlGuest.ProcessUpdate(obj, ClientContent);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result, responseData: obj.GR01);
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
