#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.OA.Private;
using GUIStd.DAL.OA.Models.Private.OA20;
using GUIStd.Extensions;
using GUIStd.Models;
using MGUIBAAPI.Models.OA;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
    /// <summary>
    /// 【需經驗證】OA營收合約控制器
    /// </summary>
    [Route("oa/[controller]")]
    public class ContractsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlOA20 BlOA20 => new BlOA20(ClientContent);

        /// <summary>
        /// 收支款統計商業邏輯物件屬性
        /// </summary>
        private BlOA22 BlOA22 => new BlOA22(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的合約資料
        /// </summary>
        /// <remarks>修正 BUG-003 / M-001 / M-002：完整傳遞所有查詢條件到 BLL</remarks>
        [HttpPost("query/{compId}/{customerId}/pages/{pageNo}")]
        public MdOA20_p GetData(string compId, string customerId,
            [DARange(1, int.MaxValue)] int pageNo, [FromBody] MdContractQueryRequest body)
        {
            // 從 Body 中取得查詢條件
            var _contractType = body?.ContractType ?? string.Empty;
            var _contractId = body?.ContractId ?? string.Empty;
            var _contractDateStart = body?.ContractDateStart ?? string.Empty;
            var _contractDateEnd = body?.ContractDateEnd ?? string.Empty;
            var _queryText = body?.QueryText ?? string.Empty;
            var _contractStatus = body?.ContractStatus ?? string.Empty;

            return BlOA20.GetData(
                compId ?? string.Empty,
                customerId ?? string.Empty,
                _contractType,
                _contractStatus,
                _queryText,
                _contractId,
                _contractDateStart,
                _contractDateEnd,
                ControlName,
                pageNo
            );
        }

        /// <summary>
        /// 取得唯一的合約資料
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="contractId">合約編號</param>
        /// <returns>合約資料</returns>
        [HttpGet("{compId}/{contractId}")]
        public MdOA20 GetRow(string compId, string contractId)
        {
            return BlOA20.GetRow(compId, contractId);
        }

        /// <summary>
        /// 取得收支款統計資料（由 OA22 計算）
        /// </summary>
        /// <remarks>透過 BlOA22 商業邏輯層取得收支款統計</remarks>
        /// <param name="compId">公司別</param>
        /// <param name="contractId">合約編號</param>
        /// <returns>收支款統計資料</returns>
        [HttpGet("revenue/{compId}/{contractId}")]
        public MdRevenueDetailStatusSummary GetRevenueSummary(string compId, string contractId)
        {
            var _summary = BlOA22.GetSummary(compId ?? string.Empty, contractId);

            return new MdRevenueDetailStatusSummary
            {
                CompId = compId,
                //ContractId = contractId,
                //ReceivedAmount = _summary.ReceivedAmount,
                //ArAmount = _summary.ArAmount,
                //AccrualExpenseAmount = _summary.AccrualExpenseAmount,
                //PayableAmount = _summary.PayableAmount
            };
        }

        /// <summary>
        /// 批次取得收支款統計資料（由 OA22 計算）
        /// </summary>
        /// <remarks>透過 BlOA22 商業邏輯層批次取得收支款統計</remarks>
        /// <param name="compId">公司別</param>
        /// <param name="contractIds">合約編號（多個用逗號分隔）</param>
        /// <returns>收支款統計資料字典（key: 合約編號）</returns>
        [HttpGet("revenue/batch/{compId}/{contractIds}")]
        public Dictionary<string, MdRevenueDetailStatusSummary> GetRevenueSummaryBatch(
            string compId, string contractIds)
        {
            var _result = new Dictionary<string, MdRevenueDetailStatusSummary>();

            if (string.IsNullOrEmpty(contractIds))
                return _result;

            var _contractIdList = contractIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (_contractIdList.Length == 0)
                return _result;

            foreach (var _contractId in _contractIdList)
            {
                var _trimmedId = _contractId.Trim();
                var _summary = BlOA22.GetSummary(compId ?? string.Empty, _trimmedId);

                _result[_trimmedId] = new MdRevenueDetailStatusSummary
                {
                    //CompId = compId,
                    //ContractId = _trimmedId,
                    //ReceivedAmount = _summary.ReceivedAmount,
                    //ArAmount = _summary.ArAmount,
                    //AccrualExpenseAmount = _summary.AccrualExpenseAmount,
                    //PayableAmount = _summary.PayableAmount
                };
            }

            return _result;
        }

        /// <summary>
        /// 判斷合約是否已存在
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="contractId">合約編號</param>
        /// <returns></returns>
        [HttpGet("exists/{compId}/{contractId}")]
        public bool IsExist(string compId, string contractId)
        {
            return BlOA20.GetRow(compId, contractId) != null;
        }

        /// <summary>
        /// 合約輔助查詢（分頁）
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="queryText">查詢關鍵字</param>
        /// <param name="pageNo">頁次</param>
        /// <returns>合約分頁資料</returns>
        [HttpGet("help/{compId}/{queryText}/pages/{pageNo}")]
        public MdOA20_p GetSHelp(string compId, string queryText, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlOA20.GetData(
                compId ?? string.Empty,
                string.Empty,                       // customerId
                string.Empty,                       // contractType
                string.Empty,                       // contractStatus
                queryText ?? string.Empty,          // queryText
                string.Empty,                       // contractId
                string.Empty,                       // contractDateStart
                string.Empty,                       // contractDateEnd
                ControlName,                        // funcName
                pageNo
            );
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">合約資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdContract obj)
        {
            try
            {
                // 修正 BUG-011 / BUG-020：採用前端傳入的 CreateDate（若無則 fallback 為系統日）
                var _createDate = !string.IsNullOrEmpty(obj.CreateDate)
                    ? obj.CreateDate
                    : DateTime.Now.ToString("yyyyMMdd");
                // 組合新增資料模型
                var _data = new MdOA20_i
                {
                    OA2001 = obj.CompId ?? string.Empty,
                    OA2002 = obj.ContractId ?? string.Empty,
                    OA2003 = obj.CustomerId ?? string.Empty,
                    OA2004 = obj.NewOldCustomer ?? "N",
                    OA2005 = obj.ContractEndDate ?? string.Empty,
                    OA2006 = obj.ContractType ?? "M",
                    OA2007 = obj.ContractAmount,
                    OA2008 = obj.ContractAmountTax,
                    OA2009 = obj.ExternalCostBudget,
                    OA2010 = obj.ContractStatus ?? "A",
                    OA2011 = obj.Remark ?? string.Empty,
                    OA2012 = obj.ExtendControlDate ?? string.Empty,
                    OA2013 = _createDate,
                    OA2014 = obj.CurrentSales ?? ClientContent.SystemUserId,
                    OA2015 = obj.ContractFileUrl ?? string.Empty
                    // TODO: 修正 BUG-020 - 待 DB 新增 OA2016 欄位後移除註解
                    // OA2016 = obj.ExtendMode ?? "待續簽約"
                };

                // 呼叫商業元件執行新增作業
                var _result = BlOA20.Insert(_data, Array.Empty<GUIStd.DAL.OA.Models.Private.OA21.MdOA21_i>(), ControlName);

                // 檢查執行結果
                if (!_result.Success)
                {
                    return HttpContext.Response.InsertFailed(new Exception(_result.Message));
                }

                // 回應前端新增成功訊息（responseData 必須回傳 ContractId，前端 _savedContractId 才取得到值）
                return HttpContext.Response.InsertSuccess(1, responseData: _result.Result?.ContractId);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="contractId">合約編號</param>
        /// <param name="obj">合約資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{compId}/{contractId}")]
        public MdApiMessage Update(string compId, string contractId, [FromBody] MdContract obj)
        {
            // 檢查鍵值路徑參數與本文中的鍵值是否相同
            if (!compId.EqualsIgnoreCase(obj.CompId) || !contractId.EqualsIgnoreCase(obj.ContractId))
            {
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            }

            try
            {
                // 組合更新資料模型
                var _data = new MdOA20_u
                {
                    OA2005 = obj.ContractEndDate ?? string.Empty,
                    OA2006 = obj.ContractType ?? "M",
                    OA2007 = obj.ContractAmount,
                    OA2008 = obj.ContractAmountTax,
                    OA2009 = obj.ExternalCostBudget,
                    OA2010 = obj.ContractStatus ?? "A",
                    OA2011 = obj.Remark ?? string.Empty,
                    OA2012 = obj.ExtendControlDate ?? string.Empty,
                    OA2014 = obj.CurrentSales ?? ClientContent.SystemUserId,
                    OA2015 = obj.ContractFileUrl ?? string.Empty
                    // TODO: 修正 BUG-020 - 待 DB 新增 OA2016 欄位後移除註解
                    // OA2016 = obj.ExtendMode ?? "待續簽約"
                };

                // 呼叫商業元件執行修改作業
                var _result = BlOA20.Update(compId ?? string.Empty, contractId, _data, ControlName);

                // 檢查執行結果
                if (!_result.Success)
                {
                    return HttpContext.Response.UpdateFailed(new Exception(_result.Message));
                }

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(1);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="compId">公司別</param>
        /// <param name="contractId">合約編號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{compId}/{contractId}")]
        public MdApiMessage Delete(string compId, string contractId)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                var _result = BlOA20.Delete(compId ?? string.Empty, contractId, ControlName);

                // 檢查執行結果
                if (!_result.Success)
                {
                    return HttpContext.Response.DeleteFailed(new Exception(_result.Message));
                }

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(1);
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
