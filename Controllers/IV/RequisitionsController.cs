#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewIV.Private;
using GUIStd.DAL.AllNewIV.Models.Private.RequestEntry;
using GUIStd.Models;
using GUICore.Web.Extensions;
using System;
using GUICore.Web.Attributes;
using System.Collections.Generic;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Private.IV.Departments;

#endregion

namespace MGUIBAAPI.Controllers.IV
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("iv/[controller]")]
	public class RequisitionsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlRequisition BlRequisition => new BlRequisition(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得請購資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="departmentId">部門別</param>
        /// <param name="warehouseId">倉庫別</param>
        /// <param name="shipDate">出貨日期</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("{warehouseId}/products")]
		public MdRequisition GetRequisitionData(string companyId, string warehouseId, 
        [RequiredFromQuery] string departmentId, [RequiredFromQuery]string shipDate)
		{
            return BlRequisition.GetRequisitionData(companyId, departmentId, warehouseId, shipDate, CurrentUILang);
		}

        /// <summary>
        /// 取得請購資料-by 請購單號
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="departmentId">部門別</param>
        /// <param name="warehouseId">倉庫別</param>
        /// <param name="shipDate">出貨日期</param>
        /// <param name="rqNo">請購單號</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("products")]
        public MdRequisition GetRequisitionData2(string companyId, [RequiredFromQuery] string rqNo, [RequiredFromQuery] string warehouseId,
                                           [RequiredFromQuery] string departmentId, [RequiredFromQuery] string shipDate)
        {
            return BlRequisition.GetRequisitionData2(companyId, departmentId, warehouseId,  shipDate, rqNo, CurrentUILang);
        }

        /// <summary>
        /// 取得請購紀錄
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="departmentId">部門別</param>
        /// <param name="dataTypes">轉單方式</param>
        /// <param name="addUserId">請購人</param>
        /// <param name="rqDates">請款日期起</param>
        /// <param name="rqDatee">請款日期迄</param>
        /// <param name="rqNos">請款單號起</param>
        /// <param name="rqNoe">請款單號迄</param>
        /// <param name="rqType">請款類別</param>
        /// <param name="rqDept">部門(篩選狀態)</param>
        /// <param name="rqStatus">簽核狀態</param>
        /// <param name="rqSuppliers">供應商起值</param>
        /// <param name="rqSuppliere">供應商迄值</param>
        /// <param name="isReqPayment">是否為請款資料</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("records")]
        public IEnumerable<MdRequisition_r> GetRequisitionRecords(string companyId, string departmentId, string dataTypes, string addUserId,
            string rqDates, string rqDatee, string rqNos, string rqNoe, string rqType, string rqDept, string rqStatus, 
            string rqSuppliers, string rqSuppliere, bool isReqPayment = false)
        {

            if (isReqPayment)
            {
                return BlRequisition.GetRequisitionRecords2(companyId, departmentId, rqDates, rqDatee, rqNos, rqNoe, 
                    rqType, rqDept, rqStatus, CurrentUILang, dataTypes, addUserId, rqSuppliers, rqSuppliere);
            }
            else
            {
                return BlRequisition.GetRequisitionRecords(companyId, departmentId, rqDates, rqDatee, rqNos, rqNoe, 
                    rqType, rqDept, rqStatus, CurrentUILang, dataTypes, addUserId);
            }
        }


        /// <summary>
        /// 取得請購請款資料(已核准且未進行拋轉採購 或不進行且與未轉傳票)
        /// </summary>
        /// <param name="departmentId">部門別</param>
        /// <param name="applyUserId">申請人</param>
        /// <param name="applyDates">申請日期起</param>
        /// <param name="applyDatee">申請日期迄</param>
        /// <param name="approvalDates">核准日期起</param>
        /// <param name="approvalDatee">核准日期迄</param>
        /// <param name="rqNos">請款單號起</param>
        /// <param name="rqNoe">請款單號迄</param>
        /// <param name="rqStatus">簽核狀態</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("recordsByReject")]
        public IEnumerable<MdRequisition_reject> GetRecordsByReject(string departmentId, string applyUserId,
            string applyDates, string applyDatee, string approvalDates, string approvalDatee, string rqNos, string rqNoe, string rqStatus)
        {

            return BlRequisition.GetRequisitionRecords3(departmentId, applyUserId, applyDates, applyDatee, 
                approvalDates, approvalDatee ,rqNos, rqNoe,rqStatus,CurrentUILang);
        }

        /// <summary>
        /// 取得請購簽核流程
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="rqTypeId">部門別</param>
        /// <param name="amounts">部門別</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("{companyId}/{rqTypeId}/{amounts}")]
        public IEnumerable<MdRequisitionFlow> GetRequisitionFlows(string companyId, string rqTypeId,decimal amounts)
        {
            return BlRequisition.GetRequisitionFlows(companyId, rqTypeId, CurrentUILang, amounts);
        }

        /// <summary>
        /// 取得所屬主管之部門與原屬部門
        /// </summary>
        /// <param name="userId">員工編號</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>部門資料模型泛型集合</returns>
        [HttpGet("departments")]
        public IEnumerable<MdDepartment> GetDeptbyUserId([FromQuery] string userId,
            [FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
        {
            return BlRequisition.GetDeptbyUserId(userId, CurrentUILang, includeEmptyRow, includeId);
        }

        /// <summary>
        /// 取得請購紀錄中的部門清單
        /// </summary>
        /// <param name="userId">員工編號</param>
        /// <param name="dataTypes"></param>轉單方式
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>部門資料模型泛型集合</returns>
        [HttpGet("departmentsByRecords")]
        public IEnumerable<MdDepartment> GetDeptbyRecords([FromQuery] string userId,[FromQuery] string dataTypes,
            [FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
        {
            return BlRequisition.GetDeptbyRecords(userId, CurrentUILang, dataTypes, includeEmptyRow, includeId);
        }

        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary> 
        /// 新增請購資料
        /// </summary>
        /// <param name="obj">請購資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdRequisition_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRequisition.ProcessInsert(obj, ClientContent, out string rqNo);
                var _mdReqResult = new MdApiMessage();
                _mdReqResult.Result = true;
                _mdReqResult.Message = rqNo;
                // 回應前端修改成功訊息
                //return HttpContext.Response.InsertSuccess(_result);
                return _mdReqResult;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改請購資料
        /// </summary>		        
        /// <param name="obj">請購資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdRequisition_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlRequisition.ProcessUpdate(obj, ClientContent);

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
        /// 修改請購資料(不進行)
        /// </summary>		        
        /// <param name="obj">請購請款資料(不進行)模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("updateByReject")]
        public MdApiMessage UpdateByReject([FromBody] IEnumerable<MdRequisition_reject_u> obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                    int _result = BlRequisition.ProcessUpdatebyReject(obj, ClientContent);

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
        /// 刪除請款單/取消送簽
        /// </summary>		        
        /// <param name="companyId">公司別</param>
        /// <param name="rqNo">請購單號</param>
        /// <param name="isCancelApproval">是否為取消送簽</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{companyId}/{rqNo}")]
        public MdApiMessage Delete(string companyId, string rqNo,bool isCancelApproval=true)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlRequisition.ProcessDelete(companyId, rqNo, isCancelApproval, ClientContent);

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
