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

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
    /// <summary>
    /// 【需經驗證】vOLA25加班記錄與撤銷程式資料控制器
    /// </summary>
    [Route("netpy/private/[controller]")]
	public class vOLA25Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlOvertimeApplications BlOvertimeApplications => new BlOvertimeApplications(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "


        [HttpGet("page/{employeeId}")]
        public CmOLA25 GetUIData(string employeeId)
        {
            string _closeDate;
            MdAttendanceCode _overtimeCode, _overtimeCodeL;
            IEnumerable<MdReasonCode> _reasons;
            MdEmployee _employee;

            BlOvertimeApplications.GetOLA25UiData(employeeId, out _closeDate, out _employee, out _reasons, out _overtimeCode, out _overtimeCodeL);

            return new CmOLA25() { 
                closeDate = _closeDate,
                OvertimeCode = _overtimeCode,
                OvertimeCodeL = _overtimeCodeL,
                reasons = _reasons,
                employee = _employee
            };
        }

        #endregion

    }
}
