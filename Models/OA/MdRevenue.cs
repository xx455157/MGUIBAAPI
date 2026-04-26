#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;

#endregion

namespace MGUIBAAPI.Models.OA
{
    #region " 合約主檔模型 "

    /// <summary>
    /// 合約主檔 - 查詢參數
    /// </summary>
    public class MdContract_q
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>新/舊客戶 (N=新, O=舊)</summary>
        public string NewOldCustomer { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>業務人員ID</summary>
        public string SalesId { get; set; }

        /// <summary>起始日期</summary>
        public string StartDate { get; set; }

        /// <summary>截止日期</summary>
        public string EndDate { get; set; }

        /// <summary>查詢關鍵字</summary>
        public string QueryText { get; set; }
    }

    /// <summary>
    /// 合約主檔 - 新增
    /// </summary>
    public class MdContract_i
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>新/舊客戶 (N=新, O=舊)</summary>
        public string NewOldCustomer { get; set; }

        /// <summary>合約類別 (M=維護, S=買賣, R=租用)</summary>
        public string ContractType { get; set; }

        /// <summary>合約總價 (未稅)</summary>
        public decimal ContractAmount { get; set; }

        /// <summary>合約總價 (含稅)</summary>
        public decimal ContractAmountTax { get; set; }

        /// <summary>外部成本總預算 (含稅)</summary>
        public decimal ExternalCostBudget { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>合約到期日</summary>
        public string ContractEndDate { get; set; }

        /// <summary>合約延展控制日期</summary>
        public string ExtendControlDate { get; set; }

        /// <summary>合約掃描檔連結</summary>
        public string ContractFileUrl { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        /// <summary>目前Sales</summary>
        public string CurrentSales { get; set; }
    }

    /// <summary>
    /// 合約主檔 - 更新
    /// </summary>
    public class MdContract_u
    {
        /// <summary>合約類別 (M=維護, S=買賣, R=租用)</summary>
        public string ContractType { get; set; }

        /// <summary>合約總價 (未稅)</summary>
        public decimal ContractAmount { get; set; }

        /// <summary>合約總價 (含稅)</summary>
        public decimal ContractAmountTax { get; set; }

        /// <summary>外部成本總預算 (含稅)</summary>
        public decimal ExternalCostBudget { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>合約到期日</summary>
        public string ContractEndDate { get; set; }

        /// <summary>合約延展控制日期</summary>
        public string ExtendControlDate { get; set; }

        /// <summary>合約掃描檔連結</summary>
        public string ContractFileUrl { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }

        /// <summary>目前Sales</summary>
        public string CurrentSales { get; set; }
    }

    /// <summary>
    /// 合約主檔 - 顯示
    /// </summary>
    public class MdContract
    {
        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>新/舊客戶 (N=新, O=舊)</summary>
        public string NewOldCustomer { get; set; }

        /// <summary>合約類別 (M=維護, S=買賣, R=租用)</summary>
        public string ContractType { get; set; }

        /// <summary>合約總價 (未稅)</summary>
        public decimal ContractAmount { get; set; }

        /// <summary>合約總價 (含稅)</summary>
        public decimal ContractAmountTax { get; set; }

        /// <summary>外部成本總預算 (含稅)</summary>
        public decimal ExternalCostBudget { get; set; }

        /// <summary>合約狀態</summary>
        public string ContractStatus { get; set; }

        /// <summary>合約到期日</summary>
        public string ContractEndDate { get; set; }

        /// <summary>合約延展控制日期</summary>
        public string ExtendControlDate { get; set; }

        /// <summary>合約成立日期</summary>
        public string CreateDate { get; set; }

        /// <summary>目前Sales</summary>
        public string CurrentSales { get; set; }

        /// <summary>目前Sales名稱</summary>
        public string CurrentSalesName { get; set; }

        /// <summary>已收款金額</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>AR金額</summary>
        public decimal ARAmount { get; set; }

        /// <summary>Back Log金額</summary>
        public decimal BLAmount { get; set; }

        /// <summary>已計提支出金額</summary>
        public decimal PaidExpenseAmount { get; set; }

        /// <summary>合約掃描檔連結</summary>
        public string ContractFileUrl { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
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
        /// <summary>展期年數</summary>
        public int ExtendYears { get; set; }

        /// <summary>新的到期日</summary>
        public string NewEndDate { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 合約統計
    /// </summary>
    public class MdContractStats
    {
        /// <summary>總合約數</summary>
        public int TotalContracts { get; set; }

        /// <summary>合約總金額</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>已收款金額</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>AR金額</summary>
        public decimal ARAmount { get; set; }

        /// <summary>Back Log金額</summary>
        public decimal BLAmount { get; set; }
    }

    #endregion

    #region " 產品/服務模型 "

    /// <summary>
    /// 產品/服務 - 新增
    /// </summary>
    public class MdContractProduct_i
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>產品/服務ID</summary>
        public string ProductId { get; set; }

        /// <summary>分配之銷售金額 (含稅)</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>外部成本預估分配金額</summary>
        public decimal ExternalCostAmount { get; set; }

        /// <summary>保固起始日</summary>
        public string WarrantyStartDate { get; set; }

        /// <summary>保固到期日</summary>
        public string WarrantyEndDate { get; set; }

        /// <summary>維護合約起始日</summary>
        public string MaintenanceStartDate { get; set; }

        /// <summary>維護合約到期日</summary>
        public string MaintenanceEndDate { get; set; }

        /// <summary>租用生效日期</summary>
        public string RentalStartDate { get; set; }

        /// <summary>租用到期日</summary>
        public string RentalEndDate { get; set; }

        /// <summary>預計維護金額/%</summary>
        public decimal ExpectedMaintenanceAmount { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }

        /// <summary>產品類別</summary>
        public string ProductCategory { get; set; }
    }

    /// <summary>
    /// 產品/服務 - 更新
    /// </summary>
    public class MdContractProduct_u
    {
        /// <summary>分配之銷售金額 (含稅)</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>外部成本預估分配金額</summary>
        public decimal ExternalCostAmount { get; set; }

        /// <summary>保固起始日</summary>
        public string WarrantyStartDate { get; set; }

        /// <summary>保固到期日</summary>
        public string WarrantyEndDate { get; set; }

        /// <summary>維護合約起始日</summary>
        public string MaintenanceStartDate { get; set; }

        /// <summary>維護合約到期日</summary>
        public string MaintenanceEndDate { get; set; }

        /// <summary>租用生效日期</summary>
        public string RentalStartDate { get; set; }

        /// <summary>租用到期日</summary>
        public string RentalEndDate { get; set; }

        /// <summary>預計維護金額/%</summary>
        public decimal ExpectedMaintenanceAmount { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }
    }

    /// <summary>
    /// 產品/服務 - 顯示
    /// </summary>
    public class MdContractProduct
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>產品/服務ID</summary>
        public string ProductId { get; set; }

        /// <summary>產品/服務名稱</summary>
        public string ProductName { get; set; }

        /// <summary>產品類別</summary>
        public string ProductCategory { get; set; }

        /// <summary>分配之銷售金額 (含稅)</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>外部成本預估分配金額</summary>
        public decimal ExternalCostAmount { get; set; }

        /// <summary>保固起始日</summary>
        public string WarrantyStartDate { get; set; }

        /// <summary>保固到期日</summary>
        public string WarrantyEndDate { get; set; }

        /// <summary>維護合約起始日</summary>
        public string MaintenanceStartDate { get; set; }

        /// <summary>維護合約到期日</summary>
        public string MaintenanceEndDate { get; set; }

        /// <summary>租用生效日期</summary>
        public string RentalStartDate { get; set; }

        /// <summary>租用到期日</summary>
        public string RentalEndDate { get; set; }

        /// <summary>預計維護金額/%</summary>
        public decimal ExpectedMaintenanceAmount { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }
    }

    #endregion

    #region " 收支預算模型 "

    /// <summary>
    /// 收支預算 - 查詢參數
    /// </summary>
    public class MdRevenueBudget_q
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>收支類別 (income/expense)</summary>
        public string BudgetType { get; set; }

        /// <summary>業務人員ID</summary>
        public string SalesId { get; set; }

        /// <summary>起始日期</summary>
        public string StartDate { get; set; }

        /// <summary>截止日期</summary>
        public string EndDate { get; set; }

        /// <summary>狀態 (received/ar/bl)</summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 收支預算 - 新增
    /// </summary>
    public class MdRevenueBudget_i
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>預算期別名稱</summary>
        public string PeriodName { get; set; }

        /// <summary>收支別 (I=收入, E=支出)</summary>
        public string IncomeExpense { get; set; }

        /// <summary>營收類別 (會計科目)</summary>
        public string RevenueCategory { get; set; }

        /// <summary>預計收/付款金額</summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>預計收/付款日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>目前Sales</summary>
        public string CurrentSales { get; set; }

        /// <summary>目前PM</summary>
        public string CurrentPM { get; set; }

        /// <summary>收/付款內容及變化說明</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 收支預算 - 更新
    /// </summary>
    public class MdRevenueBudget_u
    {
        /// <summary>預算期別名稱</summary>
        public string PeriodName { get; set; }

        /// <summary>收支別 (I=收入, E=支出)</summary>
        public string IncomeExpense { get; set; }

        /// <summary>營收類別 (會計科目)</summary>
        public string RevenueCategory { get; set; }

        /// <summary>預計收/付款金額</summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>預計收/付款日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>收/付款內容及變化說明</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 收支預算 - 顯示
    /// </summary>
    public class MdRevenueBudget
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>預算期別名稱</summary>
        public string PeriodName { get; set; }

        /// <summary>收支別 (I=收入, E=支出)</summary>
        public string IncomeExpense { get; set; }

        /// <summary>收支別名稱</summary>
        public string IncomeExpenseName { get; set; }

        /// <summary>營收類別 (會計科目)</summary>
        public string RevenueCategory { get; set; }

        /// <summary>營收類別名稱</summary>
        public string RevenueCategoryName { get; set; }

        /// <summary>收款金額 (預計/實際)</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>付款金額 (預計/實際)</summary>
        public decimal PaidAmount { get; set; }

        /// <summary>預計收/付款日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>目前Sales</summary>
        public string CurrentSales { get; set; }

        /// <summary>目前Sales名稱</summary>
        public string CurrentSalesName { get; set; }

        /// <summary>變化過程紀錄</summary>
        public string Remark { get; set; }

        /// <summary>已收款</summary>
        public decimal AlreadyReceived { get; set; }

        /// <summary>AR現況</summary>
        public decimal ARStatus { get; set; }

        /// <summary>BL現況</summary>
        public decimal BLStatus { get; set; }

        /// <summary>應計支出現況</summary>
        public decimal AccruedExpense { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>GL傳票號碼</summary>
        public string GLVoucherNo { get; set; }

        /// <summary>匯款ID</summary>
        public string RemittanceId { get; set; }

        /// <summary>支票號碼</summary>
        public string CheckNo { get; set; }

        /// <summary>收支付日期</summary>
        public string PaymentDate { get; set; }

        /// <summary>收款GL傳票號碼</summary>
        public string PaymentGLVoucherNo { get; set; }
    }

    /// <summary>
    /// 預算複製請求
    /// </summary>
    public class MdBudgetCopyRequest
    {
        /// <summary>來源合約ID</summary>
        public string SourceContractId { get; set; }

        /// <summary>目標合約ID</summary>
        public string TargetContractId { get; set; }

        /// <summary>日期偏移天數</summary>
        public int DateOffsetDays { get; set; }
    }

    /// <summary>
    /// 收支調整 (折讓/退費)
    /// </summary>
    public class MdBudgetAdjustment
    {
        /// <summary>原始預算ID</summary>
        public string OriginalBudgetId { get; set; }

        /// <summary>調整金額 (負數)</summary>
        public decimal AdjustmentAmount { get; set; }

        /// <summary>調整原因</summary>
        public string Reason { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    #endregion

    #region " 發票模型 "

    /// <summary>
    /// 發票 - 查詢參數
    /// </summary>
    public class MdInvoice_q
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>業務人員ID</summary>
        public string SalesId { get; set; }

        /// <summary>起始日期</summary>
        public string StartDate { get; set; }

        /// <summary>截止日期</summary>
        public string EndDate { get; set; }

        /// <summary>發票類別 (S=銷項, P=進項)</summary>
        public string InvoiceType { get; set; }
    }

    /// <summary>
    /// 發票 - 新增
    /// </summary>
    public class MdInvoice_i
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票金額 (含稅)</summary>
        public decimal InvoiceAmountTax { get; set; }

        /// <summary>發票金額 (未稅)</summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>發票類別 (S=銷項, P=進項)</summary>
        public string InvoiceType { get; set; }

        /// <summary>付款對象統編</summary>
        public string TaxId { get; set; }

        /// <summary>關聯的預算ID列表</summary>
        public List<string> BudgetIds { get; set; }

        /// <summary>GL傳票號碼</summary>
        public string GLVoucherNo { get; set; }
    }

    /// <summary>
    /// 發票 - 更新
    /// </summary>
    public class MdInvoice_u
    {
        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>GL傳票號碼</summary>
        public string GLVoucherNo { get; set; }
    }

    /// <summary>
    /// 發票 - 顯示
    /// </summary>
    public class MdInvoice
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票金額 (含稅)</summary>
        public decimal InvoiceAmountTax { get; set; }

        /// <summary>發票金額 (未稅)</summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>發票明細</summary>
        public string InvoiceDescription { get; set; }

        /// <summary>發票類別 (S=銷項, P=進項)</summary>
        public string InvoiceType { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>付款對象統編</summary>
        public string TaxId { get; set; }

        /// <summary>GL傳票號碼</summary>
        public string GLVoucherNo { get; set; }

        /// <summary>是否已收款</summary>
        public bool IsReceived { get; set; }

        /// <summary>已收款金額</summary>
        public decimal ReceivedAmount { get; set; }
    }

    /// <summary>
    /// 折讓單
    /// </summary>
    public class MdCreditNote
    {
        /// <summary>折讓金額</summary>
        public decimal CreditAmount { get; set; }

        /// <summary>折讓原因</summary>
        public string Reason { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    #endregion

    #region " 收款模型 "

    /// <summary>
    /// 收款 - 查詢參數
    /// </summary>
    public class MdPayment_q
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>業務人員ID</summary>
        public string SalesId { get; set; }

        /// <summary>起始日期</summary>
        public string StartDate { get; set; }

        /// <summary>截止日期</summary>
        public string EndDate { get; set; }
    }

    /// <summary>
    /// 收款 - 新增
    /// </summary>
    public class MdPayment_i
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>匯款ID</summary>
        public string RemittanceId { get; set; }

        /// <summary>支票號碼</summary>
        public string CheckNo { get; set; }

        /// <summary>收支付日期</summary>
        public string PaymentDate { get; set; }

        /// <summary>收支付金額</summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>傳票號碼</summary>
        public string VoucherNo { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 收款 - 更新
    /// </summary>
    public class MdPayment_u
    {
        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 收款 - 顯示
    /// </summary>
    public class MdPayment
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>匯款ID</summary>
        public string RemittanceId { get; set; }

        /// <summary>支票號碼</summary>
        public string CheckNo { get; set; }

        /// <summary>收支付日期</summary>
        public string PaymentDate { get; set; }

        /// <summary>收支付金額</summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>業務Sales</summary>
        public string Sales { get; set; }

        /// <summary>傳票號碼</summary>
        public string VoucherNo { get; set; }

        /// <summary>備註</summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 收款對應
    /// </summary>
    public class MdPaymentMatch
    {
        /// <summary>收款ID</summary>
        public string PaymentId { get; set; }

        /// <summary>對應的發票ID列表</summary>
        public List<string> InvoiceIds { get; set; }
    }

    /// <summary>
    /// 保證金/押金
    /// </summary>
    public class MdDeposit
    {
        /// <summary>保證金金額</summary>
        public decimal DepositAmount { get; set; }

        /// <summary>收到日期</summary>
        public string ReceivedDate { get; set; }

        /// <summary>歸還日期</summary>
        public string ReturnDate { get; set; }

        /// <summary>說明</summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 押金利息
    /// </summary>
    public class MdDepositInterest
    {
        /// <summary>總利息金額</summary>
        public decimal TotalInterest { get; set; }

        /// <summary>收到日期</summary>
        public string ReceivedDate { get; set; }

        /// <summary>支付次數</summary>
        public int PaymentCount { get; set; }

        /// <summary>說明</summary>
        public string Description { get; set; }
    }

    #endregion

    #region " 輔助模型 "

    /// <summary>
    /// Back Log 顯示
    /// </summary>
    public class MdBackLog
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>預算期別名稱</summary>
        public string PeriodName { get; set; }

        /// <summary>預計收款金額</summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>預計收款日期</summary>
        public string BudgetDate { get; set; }

        /// <summary>業務Sales</summary>
        public string Sales { get; set; }

        /// <summary>業務Sales名稱</summary>
        public string SalesName { get; set; }

        /// <summary>逾期天數</summary>
        public int OverdueDays { get; set; }
    }

    /// <summary>
    /// AR 顯示
    /// </summary>
    public class MdAR
    {
        /// <summary>公司別</summary>
        public string CompId { get; set; }

        /// <summary>合約ID</summary>
        public string ContractId { get; set; }

        /// <summary>客戶ID</summary>
        public string CustomerId { get; set; }

        /// <summary>客戶名稱</summary>
        public string CustomerName { get; set; }

        /// <summary>發票號碼</summary>
        public string InvoiceNo { get; set; }

        /// <summary>發票日期</summary>
        public string InvoiceDate { get; set; }

        /// <summary>發票金額</summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>已收款金額</summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>AR金額</summary>
        public decimal ARAmount { get; set; }

        /// <summary>逾期天數</summary>
        public int OverdueDays { get; set; }

        /// <summary>業務Sales</summary>
        public string Sales { get; set; }

        /// <summary>業務Sales名稱</summary>
        public string SalesName { get; set; }
    }

    #endregion
}
