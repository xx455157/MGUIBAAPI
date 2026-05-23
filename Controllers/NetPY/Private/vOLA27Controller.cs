#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using MGUIBAAPI.Models.PY;
using GUIStd.BLL.PY;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.PY.Models;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.BLL.PY.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.PY.Models.Private.vOLA27;

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
    /// <summary>
    /// 【需經驗證】vOLA27異常聲明程式資料控制器
    /// </summary>
    [Route("netpy/private/[controller]")]
	public class vOLA27Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlExceptionStatement BlExceptionStatement => new BlExceptionStatement(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "


        [HttpGet("page")]
        public MdOLA27_h GetUIData()
        {
            return BlExceptionStatement.GetUIData();
        }
        
        #endregion

    }
}
