#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.GUI;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
    /// <summary>
    /// 【需經驗證】授權程式資料控制器
    /// </summary>
    [Route("netgui/[controller]")]
    public class AuthProgramsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA07 BlA07 => new BlA07(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 判斷程式是否授權
        /// </summary>
        /// <param name="programId">程式代號</param>
        /// <returns></returns>
        [HttpGet("exists/{programId}")]
        public bool IsProgramAuthorized(string programId)
        {
            return BlA07.IsProgramAuthorized(programId);
        }

        #endregion
    }
}
