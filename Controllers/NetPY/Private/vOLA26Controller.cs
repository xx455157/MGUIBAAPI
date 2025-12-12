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

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
    /// <summary>
    /// 【需經驗證】vOLA26忘打卡補登程式資料控制器
    /// </summary>
    [Route("netpy/private/[controller]")]
	public class vOLA26Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlLeaveApplications BlLeaveApplications => new BlLeaveApplications(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "


        [HttpGet("page/{employeeId}")]
        public CmOLA26 GetUIData(string employeeId)
        {

            BlLeaveApplications.GetOLA26UiData(employeeId, 
                out string _closeDate, 
                out bool _clockOutNotOverLimit, 
                out MdEmployee _employee, 
                out IEnumerable<MdReasonCode> _reasons, 
                out IEnumerable<MdAttendanceCode> _attendCodes);

            return new CmOLA26() { 
                closeDate = _closeDate,
                clockOutNotOverLimit = _clockOutNotOverLimit,
                attendCodes = _attendCodes,
                reasons = _reasons,
                employee = _employee
            };
        }
        
        #endregion

    }
}
