#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM11;


#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM11固定資產類別程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM11Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM11 BlASM11 => new BlASM11(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("page")]
        public MdASM11_h GetUIData()
        {
            return BlASM11.GetUIData();
        }




        #endregion

    }
}
