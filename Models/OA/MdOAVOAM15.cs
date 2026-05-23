#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;

#endregion

namespace MGUIBAAPI.Models.OA
{
    #region " 合約主檔 DTOs "

    /// <summary>
    /// 合約查詢條件
    /// </summary>
    public class MdContract_q
    {
        /// <summary>公司代號</summary>
        public string CompId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>查詢關鍵字</summary>
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 合約新增
    /// </summary>
    public class MdContract_i
    {
        /// <summary>公司代號</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>新/舊客戶 (N/O)</summary>
        public string NewOldCustomer { get; set; }

        /// <summary>合約終了日期</summary>
        public string ContractEndDate { get; set; }

        /// <summary>合約類型 (M=維護, S=買賣, R=租用)</summary>
        public string ContractType { get; set; }

        /// <summary>合約總價(未稅)</summary>
        public decimal ContractAmount { get; set; }

        /// <summary>合約總價(含稅)</summary>
        public decimal ContractAmountTax { get; set; }

        /// <summary>外包成本預算(含稅)</summary>
        public decimal ExternalCostBudget { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        /// <summary>展期控制日期</summary>
        public string ExtendControlDate { get; set; }

        /// <summary>目前業務</summary>
        public string CurrentSales { get; set; }

        /// <summary>合約文件URL</summary>
        public string ContractFileUrl { get; set; }
    }

    /// <summary>
    /// 合約更新
    /// </summary>
    public class MdContract_u
    {
        /// <summary>合約終了日期</summary>
        public string ContractEndDate { get; set; }

        /// <summary>合約類型</summary>
        public string ContractType { get; set; }

        /// <summary>合約總價(未稅)</summary>
        public decimal ContractAmount { get; set; }

        /// <summary>合約總價(含稅)</summary>
        public decimal ContractAmountTax { get; set; }

        /// <summary>外包成本預算(含稅)</summary>
        public decimal ExternalCostBudget { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        /// <summary>展期控制日期</summary>
        public string ExtendControlDate { get; set; }

        /// <summary>目前業務</summary>
        public string CurrentSales { get; set; }

        /// <summary>合約文件URL</summary>
        public string ContractFileUrl { get; set; }
    }

    /// <summary>
    /// 合約狀態更新
    /// </summary>
    public class MdContractStatusUpdate
    {
        /// <summary>新狀態</summary>
        public string NewStatus { get; set; }
    }

    /// <summary>
    /// 合約展期
    /// </summary>
    public class MdContractExtend
    {
        /// <summary>新終了日期</summary>
        public string NewEndDate { get; set; }

        /// <summary>展期年數</summary>
        public int ExtendYears { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 合約統計
    /// </summary>
    public class MdContractStats
    {
        /// <summary>合約總數</summary>
        public int TotalContracts { get; set; }

        /// <summary>合約總金額</summary>
        public decimal TotalAmount { get; set; }
    }

    #endregion

    #region " 收支明細 DTOs（OA22 收支預算+發票+收款整併）"

    /// <summary>
    /// 收支明細新增（OA22，含收支預算、發票、收款欄位）
    /// </summary>
    public class MdRevenueDetail_i
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約代號</summary>
        public string ContractId { get; set; }

        /// <summary>序號</summary>
        public int Seq { get; set; }

        // ===== 收支預算欄位（OA2205~OA2215）=====
        /// <summary>收支別 (I=收入, E=支出)</summary>
        public string IncomeExpenseType { get; set; }

        /// <summary>收支類別</summary>
        public string BudgetCategory { get; set; }

        /// <summary>收支科目</summary>
        public string AccountSubject { get; set; }

        /// <summary>收支項目</summary>
        public string ItemName { get; set; }

        /// <summary>預算金額</summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>預算日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>實際金額</summary>
        public decimal ActualAmount { get; set; }

        /// <summary>實際日期</summary>
        public string ActualDate { get; set; }

        /// <summary>目前業務ID</summary>
        public string CurrentSalesId { get; set; }

        /// <summary>GL傳票號碼（收支）</summary>
        public string GlVoucherNo { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        // ===== 發票欄位（OA2216~OA2222）=====
        /// <summary>是否已開發票 (Y/N)</summary>
        public string IsInvoiceIssued { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票類別</summary>
        public string InvoiceType { get; set; }

        /// <summary>發票金額</summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>發票GL傳票號碼</summary>
        public string InvoiceGlVoucherNo { get; set; }

        // ===== 收款欄位（OA2224~OA2229）=====
        /// <summary>是否已收款 (Y/N)</summary>
        public string IsReceived { get; set; }

        /// <summary>匯款編號</summary>
        public string RemittanceId { get; set; }

        /// <summary>支票號碼</summary>
        public string CheckNo { get; set; }

        /// <summary>收款日期</summary>
        public string PaymentDate { get; set; }

        /// <summary>收款金額</summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>收款GL傳票號碼</summary>
        public string PaymentGlVoucherNo { get; set; }
    }

    /// <summary>
    /// 收支明細更新（OA22，含收支預算、發票、收款欄位）
    /// </summary>
    public class MdRevenueDetail_u
    {
        // ===== 收支預算欄位（OA2205~OA2215）=====
        /// <summary>收支別</summary>
        public string IncomeExpenseType { get; set; }

        /// <summary>收支類別</summary>
        public string BudgetCategory { get; set; }

        /// <summary>收支科目</summary>
        public string AccountSubject { get; set; }

        /// <summary>收支項目</summary>
        public string ItemName { get; set; }

        /// <summary>預算金額</summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>預算日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>實際金額</summary>
        public decimal ActualAmount { get; set; }

        /// <summary>實際日期</summary>
        public string ActualDate { get; set; }

        /// <summary>目前業務ID</summary>
        public string CurrentSalesId { get; set; }

        /// <summary>GL傳票號碼（收支）</summary>
        public string GlVoucherNo { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        // ===== 發票欄位（OA2216~OA2222）=====
        /// <summary>是否已開發票</summary>
        public string IsInvoiceIssued { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票類別</summary>
        public string InvoiceType { get; set; }

        /// <summary>發票金額</summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>發票GL傳票號碼</summary>
        public string InvoiceGlVoucherNo { get; set; }

        // ===== 收款欄位（OA2224~OA2229）=====
        /// <summary>是否已收款</summary>
        public string IsReceived { get; set; }

        /// <summary>匯款編號</summary>
        public string RemittanceId { get; set; }

        /// <summary>支票號碼</summary>
        public string CheckNo { get; set; }

        /// <summary>收款日期</summary>
        public string PaymentDate { get; set; }

        /// <summary>收款金額</summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>收款GL傳票號碼</summary>
        public string PaymentGlVoucherNo { get; set; }
    }

    /// <summary>
    /// 收支明細主檔（OA22，含收支預算、發票、收款欄位）
    /// </summary>
    public class MdRevenueDetail
    {
        // ===== 共同識別欄位（OA2201~OA2204）=====
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約代號</summary>
        public string ContractId { get; set; }

        /// <summary>序號</summary>
        public int Seq { get; set; }

        // ===== 收支預算欄位（OA2205~OA2215）=====
        /// <summary>收支別</summary>
        public string IncomeExpenseType { get; set; }

        /// <summary>收支類別</summary>
        public string BudgetCategory { get; set; }

        /// <summary>收支科目</summary>
        public string AccountSubject { get; set; }

        /// <summary>收支項目</summary>
        public string ItemName { get; set; }

        /// <summary>預算金額</summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>預算日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>實際金額</summary>
        public decimal ActualAmount { get; set; }

        /// <summary>實際日期</summary>
        public string ActualDate { get; set; }

        /// <summary>目前業務ID</summary>
        public string CurrentSalesId { get; set; }

        /// <summary>GL傳票號碼（收支）</summary>
        public string GlVoucherNo { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        // ===== 發票欄位（OA2216~OA2222）=====
        /// <summary>是否已開發票</summary>
        public string IsInvoiceIssued { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票類別</summary>
        public string InvoiceType { get; set; }

        /// <summary>發票金額</summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>發票GL傳票號碼</summary>
        public string InvoiceGlVoucherNo { get; set; }

        // ===== 收款欄位（OA2224~OA2229）=====
        /// <summary>是否已收款</summary>
        public string IsReceived { get; set; }

        /// <summary>匯款編號</summary>
        public string RemittanceId { get; set; }

        /// <summary>支票號碼</summary>
        public string CheckNo { get; set; }

        /// <summary>收款日期</summary>
        public string PaymentDate { get; set; }

        /// <summary>收款金額</summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>收款GL傳票號碼</summary>
        public string PaymentGlVoucherNo { get; set; }
    }

    /// <summary>
    /// 收支款現況統計（由 OA22 即時計算回傳，不再獨立存表）
    /// </summary>
    public class MdRevenueDetailStatusSummary
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約代號</summary>
        public string ContractId { get; set; }

        /// <summary>已收款金額</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>應收帳款 (AR)</summary>
        public decimal ArAmount { get; set; }

        /// <summary>銀行借款 (BL) - 需確認來源</summary>
        public string BlAmountNote { get; set; }

        /// <summary>應計支出</summary>
        public decimal AccrualExpenseAmount { get; set; }

        /// <summary>應付款合計</summary>
        public decimal PayableAmount { get; set; }
    }

    #endregion

    #region " 產品/服務 DTOs "

    /// <summary>
    /// 產品/服務新增
    /// </summary>
    public class MdContractProduct_i
    {
        /// <summary>公司代號</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>產品ID</summary>
        public string ProductId { get; set; }

        /// <summary>產品名稱</summary>
        public string ProductName { get; set; }

        /// <summary>產品類別</summary>
        public string ProductCategory { get; set; }

        /// <summary>產品系統</summary>
        public string ProductSystem { get; set; }

        /// <summary>訂製程式名稱</summary>
        public string CustomProgramName { get; set; }

        /// <summary>訂製程式說明</summary>
        public string CustomProgramDesc { get; set; }

        /// <summary>銷售金額</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>外包成本</summary>
        public decimal ExternalCostAmount { get; set; }

        /// <summary>保固開始日期</summary>
        public string WarrantyStartDate { get; set; }

        /// <summary>保固終了日期</summary>
        public string WarrantyEndDate { get; set; }

        /// <summary>維護開始日期</summary>
        public string MaintenanceStartDate { get; set; }

        /// <summary>維護終了日期</summary>
        public string MaintenanceEndDate { get; set; }

        /// <summary>租用開始日期</summary>
        public string RentalStartDate { get; set; }

        /// <summary>租用終了日期</summary>
        public string RentalEndDate { get; set; }

        /// <summary>預計維護金額</summary>
        public decimal ExpectedMaintenanceAmount { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 產品/服務更新
    /// </summary>
    public class MdContractProduct_u
    {
        /// <summary>產品ID</summary>
        public string ProductId { get; set; }

        /// <summary>產品名稱</summary>
        public string ProductName { get; set; }

        /// <summary>產品類別</summary>
        public string ProductCategory { get; set; }

        /// <summary>產品系統</summary>
        public string ProductSystem { get; set; }

        /// <summary>訂製程式名稱</summary>
        public string CustomProgramName { get; set; }

        /// <summary>訂製程式說明</summary>
        public string CustomProgramDesc { get; set; }

        /// <summary>銷售金額</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>外包成本</summary>
        public decimal ExternalCostAmount { get; set; }

        /// <summary>保固開始日期</summary>
        public string WarrantyStartDate { get; set; }

        /// <summary>保固終了日期</summary>
        public string WarrantyEndDate { get; set; }

        /// <summary>維護開始日期</summary>
        public string MaintenanceStartDate { get; set; }

        /// <summary>維護終了日期</summary>
        public string MaintenanceEndDate { get; set; }

        /// <summary>租用開始日期</summary>
        public string RentalStartDate { get; set; }

        /// <summary>租用終了日期</summary>
        public string RentalEndDate { get; set; }

        /// <summary>預計維護金額</summary>
        public decimal ExpectedMaintenanceAmount { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 產品/服務主檔
    /// </summary>
    public class MdContractProduct
    {
        /// <summary>公司代號</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>產品ID</summary>
        public string ProductId { get; set; }

        /// <summary>產品名稱</summary>
        public string ProductName { get; set; }

        /// <summary>產品類別</summary>
        public string ProductCategory { get; set; }

        /// <summary>產品系統</summary>
        public string ProductSystem { get; set; }

        /// <summary>銷售金額</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>外包成本</summary>
        public decimal ExternalCostAmount { get; set; }

        /// <summary>保固開始日期</summary>
        public string WarrantyStartDate { get; set; }

        /// <summary>保固終了日期</summary>
        public string WarrantyEndDate { get; set; }

        /// <summary>維護開始日期</summary>
        public string MaintenanceStartDate { get; set; }

        /// <summary>維護終了日期</summary>
        public string MaintenanceEndDate { get; set; }

        /// <summary>租用開始日期</summary>
        public string RentalStartDate { get; set; }

        /// <summary>租用終了日期</summary>
        public string RentalEndDate { get; set; }

        /// <summary>預計維護金額</summary>
        public decimal ExpectedMaintenanceAmount { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }
    }

    #endregion
}
