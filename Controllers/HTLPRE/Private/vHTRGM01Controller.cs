#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    /// vHTRGM01登記系統程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTRGM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlCheckIn BlCheckIn=> new BlCheckIn(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmHTRGM01 GetUIData()
        {
            string _bkDate;
            IEnumerable<MdCode> _selectConditions;
            IEnumerable<MdCode> _ciStatus;

            BlCheckIn.GetHTRGM01UIData(CurrentLang, out _bkDate, out _selectConditions, out _ciStatus);
            
            return new CmHTRGM01()
            {
                BKDate = _bkDate,
                SelectConditions = _selectConditions,
                CIStatus = _ciStatus
            };
        }

        #endregion

    }
}
