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
    /// vHTFCM09退房系統程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTFCM09Controller : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlCheckOut BlCheckOut => new BlCheckOut(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmHTFCM09 GetUIData()
        {
            string _bkDate;
            string _shift;
            IEnumerable<MdCode> _foliotypes;
            IEnumerable<MdCode> _selectConditions;            

            BlCheckOut.GetHTFCM09UIData(CurrentLang, ClientContent, out _bkDate, out _shift, out _foliotypes, out _selectConditions);
            
            return new CmHTFCM09()
            {
                BKDate = _bkDate,
                SHIFT = _shift,
                FolioTypes = _foliotypes,
                SelectConditions = _selectConditions                
            };
        }

        #endregion

    }
}
