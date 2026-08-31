#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using MGUIBAAPI.Models.OA;
using GUIStd.BLL.OA.Private;
using GUIStd.Extensions;
using MdOA22 = GUIStd.DAL.OA.Models.Private.OA22;
using BlOA22 = GUIStd.BLL.OA.Private.BlOA22;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.OA
{
    /// <summary>
    /// 收支明細控制器（OA22）
    /// 含收支預算、發票、收款三大區塊
    /// </summary>
    [Route("oa/[controller]")]
    public class RevenueDetailsController : GUIAppAuthController
    {
        #region " 商業邏輯層屬性 "

        private BlOA22 BlOA22 => new BlOA22(ClientContent);

        #endregion

        #region " 收支明細查詢 "

        /// <summary>
        /// 依合約取得收支明細清單
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="incomeExpenseType">收支別 (I=收入, E=支出)</param>
        /// <returns>收支明細清單</returns>
        [HttpGet("by-contract/{compId}/{contractId}")]
        public IEnumerable<MdOA22.MdOA22> GetRevenueDetailsByContract(string compId, string contractId, string incomeExpenseType = null)
        {
            if (string.IsNullOrWhiteSpace(incomeExpenseType))
                return BlOA22.GetRevenueDetailsByContract(compId ?? string.Empty, contractId);

            return BlOA22.GetByIncomeExpenseType(compId ?? string.Empty, contractId, incomeExpenseType);
        }

        /// <summary>
        /// 取得收支明細單筆資料
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="seq">序號</param>
        /// <returns>收支明細資料</returns>
        [HttpGet("{compId}/{contractId}/{seq}")]
        public MdOA22.MdOA22 GetRevenueDetail(string compId, string contractId, int seq)
        {
            return BlOA22.GetRow(compId ?? string.Empty, contractId, seq);
        }

        /// <summary>
        /// 檢查收支明細是否存在
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="seq">序號</param>
        /// <returns>是否存在</returns>
        [HttpGet("exists/{compId}/{contractId}/{seq}")]
        public bool ExistsRevenueDetail(string compId, string contractId, int seq)
        {
            return BlOA22.Exists(compId ?? string.Empty, contractId, seq);
        }

        #endregion

        #region " 收支款統計 "

        /// <summary>
        /// 取得合約收支款統計
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <returns>收支款統計資料</returns>
        [HttpGet("summary/{compId}/{contractId}")]
        public GUIStd.BLL.OA.Private.MdRevenueSummary GetRevenueSummary(string compId, string contractId)
        {
            return BlOA22.GetSummary(compId ?? string.Empty, contractId);
        }

        /// <summary>
        /// 依收支別分類統計
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <returns>分類統計</returns>
        [HttpGet("category-summary/{compId}/{contractId}")]
        public List<GUIStd.BLL.OA.Private.MdRevenueCategorySummary> GetCategorySummary(string compId, string contractId)
        {
            return BlOA22.GetCategorySummary(compId ?? string.Empty, contractId).ToList();
        }

        #endregion

        #region " 收支明細維護 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">收支明細資料</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdRevenueDetail_i obj)
        {
            if (obj == null)
                return HttpContext.Response.InsertFailed(new Exception("請提供收支明細資料"));

            var _data = new MdOA22.MdOA22_i
            {
                OA2201 = obj.CompId ?? string.Empty,
                OA2202 = obj.ContractId ?? string.Empty,
                OA2203 = obj.Seq,
                OA2204 = obj.IncomeExpenseType ?? "I",
                OA2205 = obj.BudgetCategory ?? string.Empty,
                OA2206 = obj.AccountSubject ?? string.Empty,
                OA2207 = obj.ItemName ?? string.Empty,
                OA2208 = obj.BudgetAmount,
                OA2209 = obj.BudgetDate ?? string.Empty,
                OA2210 = obj.ActualAmount,
                OA2211 = obj.ActualDate ?? string.Empty,
                OA2212 = obj.CurrentSalesId ?? string.Empty,
                // 註：後端 OA22 表無 CurrentSalesName 欄位，暫存於 Filler 欄位 OA2222（如需正式欄位請加 DDL）
                OA2213 = obj.GlVoucherNo ?? string.Empty,
                OA2214 = obj.Remark ?? string.Empty,
                OA2215 = obj.IsInvoiceIssued ?? "N",
                OA2216 = obj.InvoiceNo ?? string.Empty,
                OA2217 = obj.InvoiceDate ?? string.Empty,
                OA2218 = obj.InvoiceType ?? string.Empty,
                OA2219 = obj.InvoiceAmount,
                OA2220 = obj.InvoiceDescription ?? string.Empty,
                OA2221 = obj.InvoiceGlVoucherNo ?? string.Empty,
                OA2222 = obj.CurrentSalesName ?? string.Empty,  // Filler 用於儲存 SalesName
                OA2223 = obj.IsReceived ?? "N",
                OA2224 = obj.RemittanceId ?? string.Empty,
                OA2225 = obj.CheckNo ?? string.Empty,
                OA2226 = obj.PaymentDate ?? string.Empty,
                OA2227 = obj.PaymentAmount,
                OA2228 = obj.PaymentGlVoucherNo ?? string.Empty
            };

            try
            {
                var _result = BlOA22.Insert(_data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.InsertFailed(ex: new Exception(_result.Message));

                return HttpContext.Response.InsertSuccess(1, responseData: _result.Result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="seq">序號</param>
        /// <param name="obj">收支明細資料</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{compId}/{contractId}/{seq}")]
        public MdApiMessage Update(string compId, string contractId, int seq, [FromBody] MdRevenueDetail_u obj)
        {
            // 註：MdRevenueDetail_u 不含鍵值欄位（CompId/ContractId/Seq 僅在 URL 路徑），故不適用 UpdateFailedWhenKeyNotSame。
            if (obj == null)
                return HttpContext.Response.UpdateFailed(new Exception("請提供收支明細資料"));

            var _data = new MdOA22.MdOA22_u
            {
                OA2204 = obj.IncomeExpenseType ?? "I",
                OA2205 = obj.BudgetCategory ?? string.Empty,
                OA2206 = obj.AccountSubject ?? string.Empty,
                OA2207 = obj.ItemName ?? string.Empty,
                OA2208 = obj.BudgetAmount,
                OA2209 = obj.BudgetDate ?? string.Empty,
                OA2210 = obj.ActualAmount,
                OA2211 = obj.ActualDate ?? string.Empty,
                OA2212 = obj.CurrentSalesId ?? string.Empty,
                OA2213 = obj.GlVoucherNo ?? string.Empty,
                OA2214 = obj.Remark ?? string.Empty,
                OA2215 = obj.IsInvoiceIssued ?? "N",
                OA2216 = obj.InvoiceNo ?? string.Empty,
                OA2217 = obj.InvoiceDate ?? string.Empty,
                OA2218 = obj.InvoiceType ?? string.Empty,
                OA2219 = obj.InvoiceAmount,
                OA2220 = obj.InvoiceDescription ?? string.Empty,
                OA2221 = obj.InvoiceGlVoucherNo ?? string.Empty,
                OA2223 = obj.IsReceived ?? "N",
                OA2224 = obj.RemittanceId ?? string.Empty,
                OA2225 = obj.CheckNo ?? string.Empty,
                OA2226 = obj.PaymentDate ?? string.Empty,
                OA2227 = obj.PaymentAmount,
                OA2228 = obj.PaymentGlVoucherNo ?? string.Empty
            };

            try
            {
                var _result = BlOA22.Update(compId ?? string.Empty, contractId, seq, _data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.UpdateFailed(ex: new Exception(_result.Message));

                return HttpContext.Response.UpdateSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="seq">序號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{compId}/{contractId}/{seq}")]
        public MdApiMessage Delete(string compId, string contractId, int seq)
        {
            try
            {
                var _result = BlOA22.Delete(compId ?? string.Empty, contractId, seq, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.DeleteFailed(ex: new Exception(_result.Message));

                return HttpContext.Response.DeleteSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 發票作業 "

        /// <summary>
        /// 更新發票資訊
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="seq">序號</param>
        /// <param name="invoiceData">發票資料</param>
        /// <returns>更新結果</returns>
        [HttpPatch("{compId}/{contractId}/{seq}/invoice")]
        public MdApiMessage UpdateInvoice(string compId, string contractId, int seq, [FromBody] MGUIBAAPI.Models.OA.MdInvoiceUpdate invoiceData)
        {
            // 註：MdInvoiceUpdate 不含鍵值欄位，鍵值僅在 URL 路徑，故不適用 UpdateFailedWhenKeyNotSame。
            if (invoiceData == null)
                return HttpContext.Response.UpdateFailed(new Exception("請提供發票資料"));

            var _data = new GUIStd.BLL.OA.Private.MdInvoiceUpdate
            {
                IsInvoiceIssued = invoiceData.IsInvoiceIssued ?? "N",
                InvoiceNo = invoiceData.InvoiceNo,
                InvoiceDate = invoiceData.InvoiceDate,
                InvoiceType = invoiceData.InvoiceType,
                InvoiceAmount = invoiceData.InvoiceAmount,
                InvoiceDescription = invoiceData.InvoiceDescription,
                InvoiceGlVoucherNo = invoiceData.InvoiceGlVoucherNo
            };

            try
            {
                var _result = BlOA22.UpdateInvoice(compId ?? string.Empty, contractId, seq, _data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.UpdateFailed(ex: new Exception(_result.Message));

                return HttpContext.Response.UpdateSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 批次更新發票狀態
        /// </summary>
        /// <param name="batchInvoiceData">批次發票資料</param>
        /// <returns>批次更新結果</returns>
        [HttpPost("batch-invoice")]
        public MdApiMessage BatchUpdateInvoice([FromBody] List<MdBatchInvoiceItem> batchInvoiceData)
        {
            if (batchInvoiceData == null || batchInvoiceData.Count == 0)
                return HttpContext.Response.InsertFailed(new Exception("請提供發票資料"));

            var _results = new List<object>();
            var _hasError = false;

            try
            {
                foreach (var item in batchInvoiceData)
                {
                    var _data = new GUIStd.BLL.OA.Private.MdInvoiceUpdate
                    {
                        IsInvoiceIssued = item.IsInvoiceIssued ?? "N",
                        InvoiceNo = item.InvoiceNo,
                        InvoiceDate = item.InvoiceDate,
                        InvoiceType = item.InvoiceType,
                        InvoiceAmount = item.InvoiceAmount,
                        InvoiceDescription = item.InvoiceDescription,
                        InvoiceGlVoucherNo = item.InvoiceGlVoucherNo
                    };

                    var _result = BlOA22.UpdateInvoice(item.CompId ?? string.Empty, item.ContractId, item.Seq, _data, ControlName);
                    _results.Add(new
                    {
                        compId = item.CompId ?? string.Empty,
                        contractId = item.ContractId,
                        seq = item.Seq,
                        success = _result.Success,
                        message = _result.Message
                    });

                    if (!_result.Success)
                        _hasError = true;
                }

                if (_hasError)
                    return HttpContext.Response.UpdateFailed(new Exception("批次發票更新存在失敗項目"));

                return HttpContext.Response.UpdateSuccess(1, responseData: _results);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

        #region " 收款作業 "

        /// <summary>
        /// 更新收款資訊
        /// </summary>
        /// <param name="compId">公司代號</param>
        /// <param name="contractId">合約代號</param>
        /// <param name="seq">序號</param>
        /// <param name="paymentData">收款資料</param>
        /// <returns>更新結果</returns>
        [HttpPatch("{compId}/{contractId}/{seq}/payment")]
        public MdApiMessage UpdatePayment(string compId, string contractId, int seq, [FromBody] MGUIBAAPI.Models.OA.MdPaymentUpdate paymentData)
        {
            // 註：MdPaymentUpdate 不含鍵值欄位，鍵值僅在 URL 路徑，故不適用 UpdateFailedWhenKeyNotSame。
            if (paymentData == null)
                return HttpContext.Response.UpdateFailed(new Exception("請提供收款資料"));

            var _data = new GUIStd.BLL.OA.Private.MdPaymentUpdate
            {
                IsReceived = paymentData.IsReceived ?? "N",
                RemittanceId = paymentData.RemittanceId,
                CheckNo = paymentData.CheckNo,
                PaymentDate = paymentData.PaymentDate,
                PaymentAmount = paymentData.PaymentAmount,
                PaymentGlVoucherNo = paymentData.PaymentGlVoucherNo
            };

            try
            {
                var _result = BlOA22.UpdatePayment(compId ?? string.Empty, contractId, seq, _data, ControlName);

                if (!_result.Success)
                    return HttpContext.Response.UpdateFailed(ex: new Exception(_result.Message));

                return HttpContext.Response.UpdateSuccess(1);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 批次更新收款狀態
        /// </summary>
        /// <param name="batchPaymentData">批次收款資料</param>
        /// <returns>批次更新結果</returns>
        [HttpPost("batch-payment")]
        public MdApiMessage BatchUpdatePayment([FromBody] List<MdBatchPaymentItem> batchPaymentData)
        {
            if (batchPaymentData == null || batchPaymentData.Count == 0)
                return HttpContext.Response.InsertFailed(new Exception("請提供收款資料"));

            var _results = new List<object>();
            var _hasError = false;

            try
            {
                foreach (var item in batchPaymentData)
                {
                    var _data = new GUIStd.BLL.OA.Private.MdPaymentUpdate
                    {
                        IsReceived = item.IsReceived ?? "N",
                        RemittanceId = item.RemittanceId,
                        CheckNo = item.CheckNo,
                        PaymentDate = item.PaymentDate,
                        PaymentAmount = item.PaymentAmount,
                        PaymentGlVoucherNo = item.PaymentGlVoucherNo
                    };

                    var _result = BlOA22.UpdatePayment(item.CompId ?? string.Empty, item.ContractId, item.Seq, _data, ControlName);
                    _results.Add(new
                    {
                        compId = item.CompId ?? string.Empty,
                        contractId = item.ContractId,
                        seq = item.Seq,
                        success = _result.Success,
                        message = _result.Message
                    });

                    if (!_result.Success)
                        _hasError = true;
                }

                if (_hasError)
                    return HttpContext.Response.UpdateFailed(new Exception("批次收款更新存在失敗項目"));

                return HttpContext.Response.UpdateSuccess(1, responseData: _results);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }

    #region " 輔助類別 "

    /// <summary>
    /// 收支分類統計
    /// </summary>
    public class MdRevenueCategorySummary
    {
        /// <summary>收支別</summary>
        public string IncomeExpenseType { get; set; }

        /// <summary>收支類別</summary>
        public string BudgetCategory { get; set; }

        /// <summary>筆數</summary>
        public int Count { get; set; }

        /// <summary>預算金額合計</summary>
        public decimal TotalBudgetAmount { get; set; }

        /// <summary>實際金額合計</summary>
        public decimal TotalActualAmount { get; set; }

        /// <summary>發票金額合計</summary>
        public decimal TotalInvoiceAmount { get; set; }

        /// <summary>收款金額合計</summary>
        public decimal TotalPaymentAmount { get; set; }
    }

    #endregion
}
