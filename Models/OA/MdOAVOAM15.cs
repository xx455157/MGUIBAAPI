#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;

#endregion

namespace MGUIBAAPI.Models.OA
{
    #region " 合約主檔 DTOs "

    /// <summary>
    /// 合約主檔（用於查詢結果與維護）
    /// </summary>
    public class MdContract
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

        /// <summary>建檔日期</summary>
        public string CreateDate { get; set; }

        /// <summary>目前業務</summary>
        public string CurrentSales { get; set; }

        /// <summary>合約文件URL</summary>
        public string ContractFileUrl { get; set; }

        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>業務名稱</summary>
        public string CurrentSalesName { get; set; }

        /// <summary>續約模式（待續簽約/自動續約/合約完成）</summary>
        public string ExtendMode { get; set; }

        // ===== 收支款統計（由 OA22 JOIN 計算）=====
        /// <summary>已收款金額</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>應收帳款 (AR)</summary>
        public decimal ArAmount { get; set; }

        /// <summary>應計支出</summary>
        public decimal AccrualExpenseAmount { get; set; }

        /// <summary>應付款合計</summary>
        public decimal PayableAmount { get; set; }
    }

    /// <summary>
    /// 合約分頁結果
    /// </summary>
    public class MdContract_p
    {
        /// <summary>合約資料清單</summary>
        public List<MdContract> Codes { get; set; }

        /// <summary>分頁資訊</summary>
        public PagingInfo Paging { get; set; }
    }

    /// <summary>
    /// 分頁資訊
    /// </summary>
    public class PagingInfo
    {
        /// <summary>目前頁次</summary>
        public int CurrentPage { get; set; }

        /// <summary>每頁筆數</summary>
        public int RowsPerPage { get; set; }

        /// <summary>總筆數</summary>
        public int TotalRows { get; set; }
    }

    /// <summary>
    /// 合約查詢條件
    /// </summary>
    public class MdContract_q
    {
        /// <summary>公司代號（可空白，由後端自動取得）</summary>
        public string CompId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>查詢關鍵字</summary>
        public string QueryText { get; set; }

        /// <summary>業務項目</summary>
        public string BusinessItem { get; set; }

        /// <summary>合約起始日(起)</summary>
        public string ContractDateStart { get; set; }

        /// <summary>合約起始日(訖)</summary>
        public string ContractDateEnd { get; set; }

        /// <summary>合約編號</summary>
        public string ContractId { get; set; }
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

        /// <summary>續約模式（待續簽約/自動續約/合約完成）</summary>
        public string ExtendMode { get; set; }
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

        /// <summary>續約模式（待續簽約/自動續約/合約完成）</summary>
        public string ExtendMode { get; set; }
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

    /// <summary>
    /// 合約查詢請求（用於 POST /oa/contracts/query 端點的 request body）
    /// </summary>
    public class MdContractQueryRequest
    {
        /// <summary>合約編號</summary>
        public string ContractId { get; set; }

        /// <summary>合約起始日(起)</summary>
        public string ContractDateStart { get; set; }

        /// <summary>合約起始日(訖)</summary>
        public string ContractDateEnd { get; set; }

        /// <summary>業務項目</summary>
        public string ContractType { get; set; }

        /// <summary>查詢關鍵字（客戶名稱）</summary>
        public string QueryText { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }
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

        /// <summary>目前業務姓名</summary>
        public string CurrentSalesName { get; set; }

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

        /// <summary>目前業務姓名</summary>
        public string CurrentSalesName { get; set; }

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

        /// <summary>目前業務姓名</summary>
        public string CurrentSalesName { get; set; }

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

        /// <summary>已收款（有發票+已收款）</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>AR現況（有發票+未收款）</summary>
        public decimal ArAmount { get; set; }

        /// <summary>BL現況（無發票+無收款）</summary>
        public decimal BlAmount { get; set; }

        /// <summary>累計支出款項</summary>
        public decimal AccrualExpenseAmount { get; set; }
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

    #region " RevenueDetails 輔助 DTO（用於 RevenueDetailsController）"

    /// <summary>
    /// 發票更新資料
    /// </summary>
    public class MdInvoiceUpdate
    {
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
    }

    /// <summary>
    /// 收款更新資料
    /// </summary>
    public class MdPaymentUpdate
    {
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
    /// 批次發票項目
    /// </summary>
    public class MdBatchInvoiceItem
    {
        /// <summary>公司代號</summary>
        public string CompId { get; set; }

        /// <summary>合約代號</summary>
        public string ContractId { get; set; }

        /// <summary>序號</summary>
        public int Seq { get; set; }

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
    }

    /// <summary>
    /// 批次收款項目
    /// </summary>
    public class MdBatchPaymentItem
    {
        /// <summary>公司代號</summary>
        public string CompId { get; set; }

        /// <summary>合約代號</summary>
        public string ContractId { get; set; }

        /// <summary>序號</summary>
        public int Seq { get; set; }

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

    #endregion
}
