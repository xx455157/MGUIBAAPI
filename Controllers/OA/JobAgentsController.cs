#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.JobAgents;
using GUIStd.Models;
using GUICore.Web.Extensions;
using System;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("oa/[controller]")]
    public class JobAgentsController : GUIAppAuthController 
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlJobAgents BlJobAgents => new BlJobAgents(ClientContent);
        private BlA95 BlA95 => new BlA95(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 原簽核人輔助資料
        /// </summary>
        /// <param name="includeEmptyRow">是否包含空白列查詢參數</param>
        /// <returns>簽核人模型泛型集合物件</returns>
        [HttpGet("help/approver")]
        public IEnumerable<MdCode> GetHelpApprover([FromQuery] bool includeEmptyRow=false)
        {
            return BlA95.GetHelpApprover(includeEmptyRow);
        }

        /// <summary>
        /// 取得 原簽核人 分頁輔助資料
        /// </summary>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/approver/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSHelpApprover(string queryText, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlA95.GetSHelpApprover(queryText, ControlName, pageNo);
        }

        /// <summary>0  
        /// 查詢 工作代理人 資料
        /// </summary>
        /// <param name="query">查詢參數</param>
        /// <param name="pageNo">頁數</param>
        /// <returns></returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdQueryResult_p GetQueryData([FromBody] MdQueryParams query, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlJobAgents.GetQueryData(query, ControlName, pageNo);
        }

        /// <summary>
        /// 查詢 工作代理人 明細資料
        /// </summary>
        /// <param name="dataKey">查詢參數</param>
        /// <returns></returns>
        [HttpPost("query/detail")]
        public MdDetailData GetDetail([FromBody] MdQueryResult dataKey)
        {
            return BlJobAgents.GetDetail(dataKey);
        }

        /// <summary>
        /// 查詢 負責的簽核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/respflow/{empId}")]
        public IEnumerable<MdRespFlow> GetRespFlow(string empId)
        {
            //return BlJobAgents.GetRespFlow(empId);
            return null;
        }

        /// <summary>
        /// 判斷 工作代理人 資料是否已存在
        /// </summary>
        /// <param name="dataKey">查詢參數</param>
        /// <returns></returns>
        [HttpPost("exists/detail")]
        public bool IsExistDetail([FromBody] MdDetailHead dataKey)
        {
            return BlJobAgents.IsExistDetail(dataKey);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 更新 工作代理人 資料
        /// </summary>
        /// <param name="detail">客戶資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("update/detail")]
        public MdApiMessage UpdateDetail([FromBody] MdDetailUpdate detail)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlJobAgents.UpdateDetail(detail);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除 工作代理人 資料
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        [HttpDelete("delete/detail")]
        public MdApiMessage DeleteDetail([FromBody] MdDetailHead dataKey)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlJobAgents.DeleteDetail(dataKey);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }

        }
        #endregion
    }
}
