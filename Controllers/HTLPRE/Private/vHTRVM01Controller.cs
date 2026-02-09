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
    /// vHTRVM01訂房系統程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTRVM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlReservation BlReservation => new BlReservation(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmHTRVM01 GetUIData()
        {
            string _bkDate;
            IEnumerable<MdCode> _selectConditions;            

            BlReservation.GetHTRVM01UIData(out _bkDate, out _selectConditions);
            
            return new CmHTRVM01()
            {
                BKDate = _bkDate,
                SelectConditions = _selectConditions
            };
        }

        #endregion

    }
}
