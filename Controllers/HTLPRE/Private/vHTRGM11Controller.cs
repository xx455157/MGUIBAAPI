#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vHTRGM11空房查詢程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTRGM11Controller : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlRoomBlocks BlRoomBlocks => new BlRoomBlocks(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public IEnumerable<MdCode> GetUIData()
        {            
            IEnumerable<MdCode> _roomTypes;

            BlRoomBlocks.GetHTRGM11UIData(CurrentLang, out _roomTypes);
            
            return _roomTypes;
        }

        #endregion

    }
}
