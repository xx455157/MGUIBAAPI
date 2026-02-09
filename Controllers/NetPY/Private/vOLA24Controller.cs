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
using GUIStd.DAL.PY.Models.Private.vOLA24;

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
    /// <summary>
    /// 【需經驗證】vOLA24加班申請程式資料控制器
    /// </summary>
    [Route("netpy/private/[controller]")]
	public class vOLA24Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlOvertimeApplications BlOvertimeApplications => new BlOvertimeApplications(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page/{employeeId}")]
        public MdOLA24_h GetUIData(string employeeId) => BlOvertimeApplications.GetOLA24UiData(employeeId);

        #endregion

    }
}
