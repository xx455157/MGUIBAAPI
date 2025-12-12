#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using MGUIBAAPI.Models.HTLPRE;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    ///  vQRCodeLangs程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vQRCodeLangsController : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlQRCodeLangs BlQRCodeLangs => new BlQRCodeLangs(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmQRCodeLangs GetUIData()
        {
            IEnumerable<MdCode> _posIds;
            IEnumerable<MdCode> _dataTypes;
            IEnumerable<MdCode> _langs;

            BlQRCodeLangs.GetQRCodeLangsUIData(out _posIds, out _dataTypes, out _langs);

            return new CmQRCodeLangs()
            {
                PosIds = _posIds,
                DataTypes = _dataTypes,
                Langs = _langs,
            };
        }        

        #endregion

    }
}
