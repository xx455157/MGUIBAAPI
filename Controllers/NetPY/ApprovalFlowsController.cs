#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.PY.Private;
using GUIStd.DAL.PY.Models.Private.OvertimeApplication;
using GUIStd.DAL.PY.Models.Private.ApprovalFlow;
using GUIStd.BLL.GUI;

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
	/// <summary>
	/// 【需經驗證】簽核流程資料控制器
	/// </summary>
	[Route("netpy/[controller]")]
	public class ApprovalFlowsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlOvertimeApplications BlOvertimeApplications => new BlOvertimeApplications(ClientContent);
        private BlApprovalFlows BlApprovalFlows => new BlApprovalFlows(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得加班簽核流程
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="departmentId">申請人部門別</param>
        /// <param name="userId">申請人員編</param>
        /// <param name="applyQty">加班時數</param>
        /// <param name="overtimeToLeave">加班轉補休</param>
        /// <returns></returns>
        [HttpGet("overtimeflow/{companyId}/{departmentId}/{userId}/{applyQty}")]
        public MdOvertimeFlow GetOvertimeFlow(string companyId,string departmentId, string userId, decimal applyQty, [FromQuery] bool overtimeToLeave)
        {
            return BlOvertimeApplications.GetOvertimeFlow(companyId,userId, departmentId, applyQty, overtimeToLeave);
        }

        /// <summary>
        /// 取得請假簽核流程
        /// </summary>
        /// <param name="leaveCode">線上請假代碼</param>
        /// <param name="companyId">公司別</param>
        /// <param name="departmentId">申請人部門別</param>
        /// <param name="userId">申請人員編</param>
        /// <param name="applyQty">加班時數</param>
        /// <param name="agentId">代理人員編</param>
        /// <param name="agentComp">代理人公司別</param>
        /// <returns></returns>
        [HttpGet("leaveflow/{leaveCode}/{companyId}/{departmentId}/{userId}/{applyQty}")]
        public MdApprovalFlow GetLeaveFlow(string leaveCode, string companyId, string departmentId, string userId,
            decimal applyQty, [FromQuery] string agentId, [FromQuery] string agentComp)
        {
            return BlApprovalFlows.GetApprovalFlow(leaveCode, companyId, userId, departmentId, applyQty, agentId, agentComp);
        }



        #endregion
    }
}
