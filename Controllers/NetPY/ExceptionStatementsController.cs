#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.PY.Private;
using GUIStd.DAL.PY.Private.ExceptionStatement;
using GUIStd.DAL.PY.Models.Private.vOLA27;

#endregion

namespace MGUIBAAPI.Controllers.NetPY
{
	/// <summary>
	/// 【需經驗證】簽核流程資料控制器
	/// </summary>
	[Route("netpy/[controller]")]
	public class ExceptionStatementsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlExceptionStatement BlExceptionStatement => new BlExceptionStatement(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查閱異常聲明項目
        /// </summary>
        /// <param name="date">查閱日期</param>
        /// <returns></returns>
        [HttpGet("{date}")]
        public MdExceptionStatement GetScheduleShiftData(string date)
        {
            return BlExceptionStatement.GetScheduleShiftData(date);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        [HttpPost("declare")]
        public MdApiMessage StatementDeclare(MdStatementDeclare obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlExceptionStatement.StatementDeclare(obj);
                var _resultObj = HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");

                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }

        }

        #endregion
    }
}
