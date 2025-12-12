#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using System; 
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Bookings;
using GUIStd.BLL.AllNewHTL.Private;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("htlpre/[controller]")]
    public class ServicesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTHC BlHTHC => new BlHTHC(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得服務資料
        /// </summary>
        /// <param name="types">類別</param> 
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet()]
		public IEnumerable<MdService> GetHelp(string types)
		{
			return BlHTHC.GetHelp(types, CurrentLang);
		}

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
