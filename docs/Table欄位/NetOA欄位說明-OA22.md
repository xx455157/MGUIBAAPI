NetOA欄位說明-OA22（收支明細，含預算/發票/收款）
Key	FieldName	DataType	Length	Description
OA22001	建檔日期	VarChar	10	建檔日期
OA22002	建檔時間	VarChar	8	建檔時間
OA22003	建檔工作站	VarChar	20	建檔工作站
OA22004	建檔人	VarChar	20	建檔人
OA22005	更改日期	VarChar	10	更改日期
OA22006	更改時間	VarChar	8	更改時間
OA22007	更改工作站	VarChar	20	更改工作站
OA22008	更改人	VarChar	20	更改人
OA2201	公司別	VarChar	8	公司別
OA2202	客戶編號	VarChar	20	客戶編號
OA2203	合約代號	VarChar	20	合約代號
OA2204	序號	VarChar	10	序號
OA2205	收支別	VarChar	1	收支別 I:收入 E:支出
OA2206	收支類別	VarChar	20	收支類別
OA2207	收支科目	VarChar	30	收支科目
OA2208	收支項目	VarChar	30	收支項目
OA2209	預算金額	Numeric	25,4	預算金額
OA2210	預算日期	VarChar	10	預算日期
OA2211	實際金額	Numeric	25,4	實際金額
OA2212	實際日期	VarChar	10	實際日期
OA2213	目前業務	VarChar	20	目前業務
OA2214	GL傳票號碼	VarChar	30	GL傳票號碼（收支傳票）
OA2215	備註	VarChar	100	備註
OA2216	是否已開發票	VarChar	1	是否已開發票 Y/N
OA2217	發票號碼	VarChar	30	發票號碼
OA2218	發票日期	VarChar	10	發票日期
OA2219	發票類別	VarChar	10	發票類別
OA2220	發票金額	Numeric	25,4	發票金額
OA2221	發票明細	VarChar	100	發票明細
OA2222	發票GL傳票號碼	VarChar	30	發票GL傳票號碼
OA2223	FILLER	VarChar	10	FILLER
OA2224	是否已收款	VarChar	1	是否已收款 Y/N
OA2225	匯款編號	VarChar	30	匯款編號
OA2226	支票號碼	VarChar	30	支票號碼
OA2227	收款日期	VarChar	10	收款日期
OA2228	收款金額	Numeric	25,4	收款金額
OA2229	收款GL傳票號碼	VarChar	30	收款GL傳票號碼
OA2230	FILLER	VarChar	10	FILLER
OA2231	FILLER	Numeric	25,4	FILLER
OA2232	FILLER	Numeric	25,4	FILLER
PK_OA22
GO
OA2201+ OA2202+ OA2203+ OA2204
IX_OA221
GO
OA2201+ OA2202+ OA2203

※ 說明
本資料表為新增草案，待正式確認後方可建表。
主鍵：(OA2201 公司別) + (OA2202 客戶編號) + (OA2203 合約代號) + (OA2204 序號)
OA22 為收支明細資料表，同一筆收支同時記錄預算、發票、收款狀態。
同一序號的資料為同一筆收支的不同階段狀態。
OA22001~OA22008 為標準 NetOA 建檔/異動欄位。

※ 收支別說明
I = 收入（預計或實際收到的款項）
E = 支出（預計或實際支付的款項）

※ 設計原則
1. 一筆收支明細（同一 seq）同時包含：預算規劃 + 發票狀態 + 收款狀態
2. 發票與收款都對應同一筆收支，不會發生對應不上的問題
3. 前端可依 Tab 分別顯示，但後端為同一筆資料

※ API 對照（前端 → DTO → 資料庫）
前端欄位（收支Tab）	DTO欄位	資料庫欄位	說明	狀態
compId	compId	OA2201	公司別	新增草案欄位
customerId	customerId	OA2202	客戶編號	新增草案欄位
contractId	contractId	OA2203	合約代號	新增草案欄位
seq	seq	OA2204	序號	新增草案欄位
incomeExpenseType	incomeExpenseType	OA2205	收支別	新增草案欄位
budgetCategory	budgetCategory	OA2206	收支類別	新增草案欄位
accountSubject	accountSubject	OA2207	收支科目	新增草案欄位
budgetAmount	budgetAmount	OA2209	預算金額	新增草案欄位
budgetDate	budgetDate	OA2210	預算日期	新增草案欄位
actualAmount	actualAmount	OA2211	實際金額	新增草案欄位
actualDate	actualDate	OA2212	實際日期	新增草案欄位
currentSalesId	currentSalesId	OA2213	目前業務	新增草案欄位
glVoucherNo	glVoucherNo	OA2214	GL傳票號碼	新增草案欄位
remark	remark	OA2215	備註	新增草案欄位
前端欄位（發票Tab）	DTO欄位	資料庫欄位	說明	狀態
isInvoiceIssued	isInvoiceIssued	OA2216	是否已開發票	新增草案欄位
invoiceNo	invoiceNo	OA2217	發票號碼	新增草案欄位
invoiceDate	invoiceDate	OA2218	發票日期	新增草案欄位
invoiceType	invoiceType	OA2219	發票類別	新增草案欄位
invoiceAmountTax	invoiceAmount	OA2220	發票金額	新增草案欄位
invoiceDescription	invoiceDescription	OA2221	發票明細	新增草案欄位
invoiceGlVoucherNo	invoiceGlVoucherNo	OA2222	發票GL傳票號碼	新增草案欄位
前端欄位（收款Tab）	DTO欄位	資料庫欄位	說明	狀態
isReceived	isReceived	OA2224	是否已收款	新增草案欄位
remittanceId	remittanceId	OA2225	匯款編號	新增草案欄位
checkNo	checkNo	OA2226	支票號碼	新增草案欄位
paymentDate	paymentDate	OA2227	收款日期	新增草案欄位
paymentAmount	paymentAmount	OA2228	收款金額	新增草案欄位
paymentGlVoucherNo	paymentGlVoucherNo	OA2229	收款GL傳票號碼	新增草案欄位

※ 收支款現況統計（由 OA22 計算，不再獨立存 OA25）
統計欄位	計算公式	說明	狀態
receivedAmount（已收款）	SUM(OA2228) WHERE OA2224='Y'	所有已收款的收款金額合計	新增草案欄位
arAmount（應收帳款）	SUM(OA2220) WHERE OA2216='Y' - SUM(OA2228) WHERE OA2224='Y'	已開發票但未收款的差額	計算欄位
blAmount（銀行借款）	-	需確認是否有對應欄位	需確認來源
accrualExpenseAmount（應計支出）	SUM(OA2211) WHERE OA2205='E' AND 實際未付款	支出類型但尚未實際付款的金額	計算欄位

※ 建表 SQL（草案，待確認）
CREATE TABLE OA22 (
    OA22001 NVARCHAR(10) NULL,  -- 建檔日期
    OA22002 NVARCHAR(8) NULL,    -- 建檔時間
    OA22003 NVARCHAR(20) NULL,   -- 建檔工作站
    OA22004 NVARCHAR(20) NULL,   -- 建檔人
    OA22005 NVARCHAR(10) NULL,   -- 更改日期
    OA22006 NVARCHAR(8) NULL,    -- 更改時間
    OA22007 NVARCHAR(20) NULL,   -- 更改工作站
    OA22008 NVARCHAR(20) NULL,   -- 更改人
    OA2201 NVARCHAR(8) NOT NULL,  -- 公司別
    OA2202 NVARCHAR(20) NOT NULL, -- 客戶編號
    OA2203 NVARCHAR(20) NOT NULL, -- 合約代號
    OA2204 NVARCHAR(10) NOT NULL, -- 序號
    OA2205 NVARCHAR(1) NULL,     -- 收支別
    OA2206 NVARCHAR(20) NULL,    -- 收支類別
    OA2207 NVARCHAR(30) NULL,    -- 收支科目
    OA2208 NVARCHAR(30) NULL,    -- 收支項目
    OA2209 NUMERIC(25,4) NULL,   -- 預算金額
    OA2210 NVARCHAR(10) NULL,    -- 預算日期
    OA2211 NUMERIC(25,4) NULL,   -- 實際金額
    OA2212 NVARCHAR(10) NULL,    -- 實際日期
    OA2213 NVARCHAR(20) NULL,    -- 目前業務
    OA2214 NVARCHAR(30) NULL,    -- GL傳票號碼
    OA2215 NVARCHAR(100) NULL,   -- 備註
    OA2216 NVARCHAR(1) NULL,     -- 是否已開發票
    OA2217 NVARCHAR(30) NULL,    -- 發票號碼
    OA2218 NVARCHAR(10) NULL,    -- 發票日期
    OA2219 NVARCHAR(10) NULL,    -- 發票類別
    OA2220 NUMERIC(25,4) NULL,   -- 發票金額
    OA2221 NVARCHAR(100) NULL,   -- 發票明細
    OA2222 NVARCHAR(30) NULL,    -- 發票GL傳票號碼
    OA2223 NVARCHAR(10) NULL,    -- FILLER
    OA2224 NVARCHAR(1) NULL,     -- 是否已收款
    OA2225 NVARCHAR(30) NULL,    -- 匯款編號
    OA2226 NVARCHAR(30) NULL,    -- 支票號碼
    OA2227 NVARCHAR(10) NULL,    -- 收款日期
    OA2228 NUMERIC(25,4) NULL,   -- 收款金額
    OA2229 NVARCHAR(30) NULL,    -- 收款GL傳票號碼
    OA2230 NVARCHAR(10) NULL,    -- FILLER
    OA2231 NUMERIC(25,4) NULL,   -- FILLER
    OA2232 NUMERIC(25,4) NULL,   -- FILLER
    PRIMARY KEY (OA2201, OA2202, OA2203, OA2204)
);
CREATE INDEX IX_OA221 ON OA22(OA2201, OA2202, OA2203);
