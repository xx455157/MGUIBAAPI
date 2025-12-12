#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Register;
using GUICore.Web.Extensions;
using GUIStd.Models;
using System;

#endregion

namespace MGUIBAAPI.Controllers.General.HTLPRE.Register
{
    /// <summary>
    /// 入住資料控制器
    /// </summary>
    [Route("general/htlpre/register/[controller]")]
    public class CheckInController : GUIAppAnonAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPreCheckIn BlPreCheckIn => new BlPreCheckIn(ClientContent);

        #endregion

        #region " 共用函式 - 取得住客資料 "

        /// <summary>
		/// 取得唯一的住客資料
		/// </summary>
		/// <param name="visitNo">住客號碼路徑參數</param>
		/// <returns>住客資料模型物件</returns>
		[HttpGet("precheck/{visitNo}")]
        public MdPreCheckIn GetRow(string visitNo)
        {            
            return  BlPreCheckIn.GetPreCheckInData(visitNo);
        }

        #endregion

        #region " 共用函式 - 異動住客資料 "

        /// <summary>
		/// 修改資料
		/// </summary>		        
		/// <param name="obj">住客資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("precheck")]
        public MdApiMessage Update([FromBody] MdPreCheckIn obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlPreCheckIn.ProcessUpdate(obj, ClientContent);
                //int _result = new BlPreCheckIn().ProcessUpdateTrans(visitNo, obj, ClientContent);
                
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
