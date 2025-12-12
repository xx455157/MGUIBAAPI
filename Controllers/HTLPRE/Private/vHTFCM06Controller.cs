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
    /// vHTFCM06客帳系統程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTFCM06Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlCheckOut BlCheckOut => new BlCheckOut(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得會計及會計科目代碼資料
        /// </summary>        
        [HttpGet("paged")]
        public CmHTFCM06_d GetUIData_d()
        {
            string _bkDate;
            string _shift;
            IEnumerable<MdCode> _foliotypes;
            IEnumerable<MdCode> _accounts;

            BlCheckOut.GetHTFCM06_dUIData(CurrentLang, ClientContent, "03", CurrentLang, 
                out _bkDate,out _shift, out _foliotypes, out _accounts);

            return new CmHTFCM06_d()
            {
                BKDate = _bkDate,
                SHIFT = _shift,
                FolioTypes= _foliotypes,
                Accounts = _accounts,                
            };
        }

        #endregion

    }
}
