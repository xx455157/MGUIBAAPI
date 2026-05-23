#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.HTL.Private;
using GUIStd.DAL.HTL.Models;
using GUIStd.DAL.HTL.Models.Private.RepairRequests;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.NetHTL
{
	/// <summary>
	/// 【需經驗證】工務修繕資料控制器
	/// </summary>
	[Route("nethtl/[controller]")]
	public class RepairRequestsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRepairRequests BlRepairRequests => new BlRepairRequests(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢房間設備修繕中紀錄
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="roomNo">房號</param>
        /// <returns>請修單資料模型泛型集合物件</returns>
        [HttpGet("{compId}/{roomNo}")]
        public IEnumerable<MdRepairRequestHK> GetRepairRequests(string compId, string roomNo, [FromQuery] bool inProcess)
        {
            return BlRepairRequests.GetRepairRequests(compId, roomNo, inProcess);
        }

        /// <summary>
        /// 查詢房間設備修繕歷史紀錄（分頁）
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="roomNo">房號</param>
        /// <param name="facilityId">設備代碼</param>
        /// <param name="pageNo">頁次</param>
        /// <returns>請修單資料模型泛型集合物件</returns>
        [HttpGet("history/{compId}/{roomNo}/{facilityId}/pages/{pageNo}")]
        public MdRepairRequestHK_p<MdRepairRequestHK> GetRepairHistory(string compId, string roomNo, string facilityId, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlRepairRequests.GetRepairHistoryPaging(compId, roomNo, facilityId, pageNo);
        }

        /// <summary>
        /// 取得修繕區域的地點清單
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="areaId">區域代碼</param>
        /// <returns>地點清單資料模型泛型集合物件</returns>
        [HttpGet("locations/{companyId}/{areaId}")]
        public IEnumerable<MdCode> GetLocations(string companyId, string areaId)
        {
            return BlRepairRequests.GetLocations(companyId, areaId);
        }


        /// <summary>
        /// 查詢指定條件的請修單紀錄（分頁）
        /// </summary>
        /// <param name="obj">請修單查詢條件物件</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <param name="functionName">查詢函式名稱</param>
        /// <returns>請修單分頁資料模型物件</returns>
        [HttpPost("pages/{pageNo}")]
        public MdRepairRequestHK_p<MdRepairRequest> GetDataByPage(MdRepairRequest_q obj, [DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0, string functionName = "vRRM02")
        {
            return BlRepairRequests.GetDataByPage(obj, functionName, pageNo, rowsPerPage);
        }

        /// <summary>
        /// 查詢指定請修單的紀錄
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="requestNo">請修單號</param>
        /// <param name="requestSerialNo">請修單序號</param>
        /// <returns>請修單資料模型物件</returns>
        [HttpGet("{compId}/{requestNo}/{requestSerialNo}")]
        public MdRepairRequest GetRow(string compId, string requestNo, [DARange(0, int.MaxValue)] decimal requestSerialNo)
        {
            return BlRepairRequests.GetRow(compId, requestNo, requestSerialNo);
        }


        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 修繕派工
        /// </summary>
        /// <param name="obj">請修單資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("assign")]
        public MdApiMessage Assign([FromBody] MdRepairAssign_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.AssignJob(obj);

                //var _test = new Token(Utils.AppSettings.TokenKeyFile).GenerateAPIAccessToken("0034", "gui");

                // 回應前端修改成功訊息 
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 驗收
        /// </summary>
        /// <param name="obj">請修單驗收資料模型物件</param>
        /// <returns>受影響筆數</returns>
        [HttpPut("inspect")]
        public MdApiMessage Inspect([FromBody] MdRepairInspect_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.InspectJob(obj);

                // 回應前端修改成功訊息 
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 結案
        /// </summary>
        /// <param name="obj">請修單驗收資料模型物件</param>
        /// <returns>受影響筆數</returns>
        [HttpPut("close")]
        public MdApiMessage Close([FromBody] MdRepairInspect_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.CloseJob(obj);

                // 回應前端修改成功訊息 
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 修繕申請
        /// </summary>
        /// <param name="objs">請修單資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("damage")]
        public MdApiMessage ReportDamage([FromBody]IEnumerable<MdRepairRequestHK_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.ProcessReportDamage(objs);

                // 回應前端修改成功訊息 
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 請修內容更新
        /// </summary>
        /// <param name="obj">請修單內容資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("damage/content")]
        public MdApiMessage UpdateReportDetail([FromBody]MdRepairRequestContentHK_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.ProcessUpdateReportDetail(obj);

                // 回應前端修改成功訊息 
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }


        #endregion
    }
}
