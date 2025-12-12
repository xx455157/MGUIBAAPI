#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewHTL.Models.Private.Register;

#endregion

namespace MGUIBAAPI.Controllers.General.HTLPRE.Register
{
    /// <summary>
    /// 訂房須知資料控制器
    /// </summary>
    [Route("general/htlpre/register/[controller]")]
    public class ReservationInfoController : GUIAppAnonAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRVInfo BlRVInfo => new BlRVInfo(ClientContent);

        #endregion

        #region " 共用函式 - 取得訂房須知 "

        /// <summary>
        /// 取得訂房須知
        /// </summary>		
        /// <returns>訂房須知模型物件</returns>
        [HttpGet("")]
        public MdRVInfo GetCol()
        {            
            return BlRVInfo.GetCol();
        }

        #endregion        

    }
}
