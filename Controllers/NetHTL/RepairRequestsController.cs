#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
using GUIStd.DAL.AllNewHTL.Models.Private.HouseKeeping;

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
        /// 檢查修繕區域 (HTDC) 是否存在
        /// </summary>
        [HttpGet("areas/exists/{compId}/{areaId}")]
        public bool AreaIsExists(string compId, string areaId)
        {
            return BlRepairRequests.AreaIsExists(compId, areaId);
        }

        /// <summary>
        /// 檢查地點 (HTRRH) 是否存在
        /// </summary>
        [HttpGet("locations/exists/{companyId}/{areaId}/{locationId}")]
        public bool LocationIsExists(string companyId, string areaId, string locationId)
        {
            return BlRepairRequests.LocationIsExists(companyId, areaId, locationId);
        }

        /// <summary>
        /// 檢查設備 (HTRRE) 是否存在
        /// </summary>
        [HttpGet("equipments/exists/{compId}/{equipId}")]
        public bool EquipmentIsExists(string compId, string equipId)
        {
            return BlRepairRequests.EquipmentIsExists(compId, equipId);
        }

        /// <summary>
        /// 取得單筆設備主檔 (HTRRE)
        /// </summary>
        [HttpGet("equipments/{compId}/{equipId}")]
        public MdRepairEquipment GetEquipmentRow(string compId, string equipId)
        {
            return BlRepairRequests.GetEquipmentRow(compId, equipId);
        }

        /// <summary>
        /// 檢查設備是否已有請修單紀錄
        /// </summary>
        [HttpGet("equipments/{compId}/{equipId}/repairrequests/exists")]
        public bool EquipmentHasRepairRequests(string compId, string equipId)
        {
            return BlRepairRequests.EquipmentHasRepairRequests(compId, equipId);
        }

        [HttpGet("equipments/{compId}")]
        public IEnumerable<MdEquipment> GetEquipments(string compId)
        {
            return BlRepairRequests.GetEquipments(compId);
        }

        /// <summary>
        /// 依設備類別取得公司設備主檔（category=_empty 為未分類；請改用 POST categories/query）
        /// </summary>
        [HttpGet("equipments/{compId}/categories/{category}")]
        public IEnumerable<MdEquipment> GetEquipmentsByCategory(string compId, string category)
        {
            return BlRepairRequests.GetEquipmentsByCategory(compId, category);
        }

        /// <summary>
        /// 依設備類別批次查詢公司設備主檔（body: string[]；含 _empty 未分類）
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="categories">設備類別清單</param>
        /// <returns>設備資料模型泛型集合物件</returns>
        [HttpPost("equipments/{compId}/categories/query")]
        public IEnumerable<MdEquipment> QueryEquipmentsByCategories(string compId, [FromBody] string[] categories)
        {
            return BlRepairRequests.GetEquipmentsByCategories(compId, categories);
        }

        /// <summary>
        /// 分頁查詢公司設備主檔（含故障原因）
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="obj">查詢條件</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>設備主檔分頁資料</returns>
        [HttpPost("equipments/{compId}/pages/{pageNo}")]
        public MdEquipment_p GetEquipmentsByPage(
            string compId,
            [FromBody] MdRepairEquipment_q obj,
            [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] int rowsPerPage = 0)
        {
            return BlRepairRequests.GetEquipmentsByPage(compId, obj, pageNo, rowsPerPage);
        }

        [HttpGet("equipments/{compId}/{departmentId}/{location}")]
        public IEnumerable<MdEquipment> GetEquipments(string compId, string departmentId, string location)
        {
            return BlRepairRequests.GetEquipments(compId, departmentId, location);
        }

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
        public IEnumerable<MdRepairLocationListItem> GetLocations(string companyId, string areaId)
        {
            return BlRepairRequests.GetLocations(companyId, areaId);
        }

        /// <summary>
        /// 取得指定公司的修繕區域清單
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <returns>修繕區域清單</returns>
        [HttpGet("areas/{compId}")]
        public IEnumerable<MdRepairArea> GetAreas(string compId)
        {
            return BlRepairRequests.GetAreas(compId);
        }

        /// <summary>
        /// 取得指定公司的地點類型清單
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <returns>地點類型清單</returns>
        [HttpGet("locationTypes/{compId}")]
        public IEnumerable<MdCode> GetLocationTypes(string compId)
        {
            return BlRepairRequests.GetLocationTypes(compId);
        }

        /// <summary>
        /// 取得指定公司的設備類別清單
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <returns>設備類別清單</returns>
        [HttpGet("equipmentCategories/{compId}")]
        public IEnumerable<MdCode> GetEquipmentCategories(string compId)
        {
            return BlRepairRequests.GetEquipmentCategories(compId);
        }

        /// <summary>
        /// 取得指定設備的故障原因清單
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="equipmentId">設備代碼</param>
        /// <returns>故障原因清單</returns>
        [HttpGet("reasons/{compId}/{equipmentId}")]
        public IActionResult GetReasons(string compId, string equipmentId)
        {
            var _data = BlRepairRequests.GetReasons(compId, equipmentId) ?? Enumerable.Empty<MdCode>();
            return Ok(_data);
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
        /// 新增修繕區域 (HTDC)
        /// </summary>
        [HttpPost("areas/apply")]
        public MdApiMessage ApplyArea([FromBody] MdRepairArea obj)
        {
            try
            {
                int _result = BlRepairRequests.ProcessAreaApply(obj);
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 新增地點 (HTRRH)
        /// </summary>
        [HttpPost("locations/apply")]
        public MdApiMessage ApplyLocation([FromBody] MdRepairLocation obj)
        {
            try
            {
                int _result = BlRepairRequests.ProcessLocationApply(obj);
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 批次新增設備故障原因 (HTRRF)
        /// </summary>
        [HttpPost("reasons/apply")]
        public MdApiMessage ApplyReasons([FromBody] MdRepairReasonApply_w obj)
        {
            try
            {
                int _result = BlRepairRequests.ProcessReasonApply(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 新增設備 (HTRRE)，可選建立 HTRRD 地點連結
        /// </summary>
        [HttpPost("equipments/apply")]
        public MdApiMessage ApplyEquipment([FromBody] MdRepairEquipment obj)
        {
            try
            {
                int _result = BlRepairRequests.ProcessEquipmentApply(obj);
                return HttpContext.Response.InsertSuccess(_result,
                    responseData: new { equipmentId = obj.RRE02 });
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改設備主檔 (HTRRE)
        /// </summary>
        [HttpPut("equipments/update")]
        public MdApiMessage UpdateEquipment([FromBody] MdRepairEquipment obj)
        {
            try
            {
                int _result = BlRepairRequests.ProcessEquipmentUpdate(obj);
                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除設備主檔 (HTRRE)
        /// </summary>
        [HttpDelete("equipments/{compId}/{equipId}")]
        public MdApiMessage DeleteEquipment(string compId, string equipId)
        {
            try
            {
                int _result = BlRepairRequests.ProcessEquipmentDelete(compId, equipId);
                return HttpContext.Response.DeleteSuccess(_result, "PgmMsg_DeleteSuccess");
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 套用設備清單
        /// [Body]為設備清單的JSON字串，格式為[{"location":"地點","equipmentId":["設備代碼",...]}, ...]
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="areaId">區域代碼</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("equipments/apply/{compId}/{areaId}")]
        public async Task<MdApiMessage> ApplyEquipments(string compId, string areaId)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.ApplyEquipments(compId, areaId, await Request.GetRawBodyStringAsync());

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
        /// 刪除設備
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="areaId">區域代碼</param>
        /// <param name="location">地點</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("equipments/delete/{compId}/{areaId}/{location}")]
        public async Task<MdApiMessage> DeleteEquipments(string compId, string areaId, string location)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlRepairRequests.DeleteEquipments(compId, areaId, location, await Request.GetRawBodyStringAsync());

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result, "PgmMsg_DeleteSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

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
        /// <returns>系統規範訊息物件</returns>
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
        /// <returns>系統規範訊息物件</returns>
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
        /// 維修
        /// </summary>
        /// <param name="obj">請修單驗收資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("repair")]
        public MdApiMessage repair([FromBody] MdRepair_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRepairRequests.RepairUpdate(obj);

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
        /// 請修單登錄（vRRM01 獨立入口）
        /// </summary>
        /// <param name="objs">請修單登錄資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("apply")]
        public MdApiMessage Apply([FromBody] IEnumerable<MdRepairRequestApply_w> objs)
        {
            try
            {
                int _result = BlRepairRequests.ProcessApply(objs);

                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess",
                    responseData: new
                    {
                        requestNo = objs.FirstOrDefault()?.RRA02
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 請修單修改（vRRM01 修改入口）
        /// </summary>
        /// <param name="objs">請修單登錄資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("apply")]
        public MdApiMessage UpdateApply([FromBody] IEnumerable<MdRepairRequestApply_w> objs)
        {
            try
            {
                int _result = BlRepairRequests.ProcessUpdateApply(objs);

                return HttpContext.Response.UpdateSuccess(_result, "PgmMsg_SaveSuccess",
                    responseData: new
                    {
                        requestNo = objs.FirstOrDefault()?.RRA02
                    }
                );
            }
            catch (Exception ex)
            {
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
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess", 
                    responseData: new {
                        requestNo = objs.FirstOrDefault()?.RRA02
                    }
                );
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
