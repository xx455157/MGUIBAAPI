NetOA欄位說明-OA20
Key	FieldName	DataType	Length	Description
OA2001	公司別	Nvarchar	8	公司別
OA2002	客戶編號	Nvarchar	9	客戶編號
OA2003	合約代號	Nvarchar	30	合約代號
OA2004	起始日	Nvarchar	8	起始日
OA0405	結束日	Nvarchar	8	結束日
OA2006	合約所屬	Nvarchar	30	合約所屬
OA2007	Nvarchar	30	Nvarchar
OA2008	Nvarchar	30	Nvarchar
OA2009	公司別	Nvarchar	8	公司別
OA2010	簡稱	Nvarchar	10	簡稱
OA2011	注意事項	Nvarchar	100	注意事項
OA2012	購買內容	Nvarchar	100	購買內容
OA2013	FILLER	Nvarchar	10	FILLER
OA2014	付款方式	Nvarchar	1	付款方式 1:月結 2:票期
OA2015	FILLER	Numeric	25,4	FILLER
OA2016	合約狀態	Numeric	1	合約狀態 1:執行中 2:已期滿 3:已終止
OA2017	FILLER	Numeric	25,4	FILLER
OA2018	FILLER	Numeric	25,4	FILLER
OA2019	FILLER	Numeric	25,4	FILLER
OA2020	FILLER	Numeric	25,4	FILLER
OA2021	FILLER	Numeric	25,4	FILLER
OA2022	FILLER	Numeric	25,4	FILLER
OA2023	FILLER	Numeric	25,4	FILLER
OA2024	FILLER	Numeric	25,4	FILLER
OA2025	FILLER	Numeric	25,4	FILLER
OA2026	FILLER	Numeric	25,4	FILLER
OA2027	FILLER	Numeric	25,4	FILLER
OA2028	FILLER	Numeric	25,4	FILLER
OA2029	FILLER	Numeric	25,4	FILLER
OA2030	FILLER	Numeric	25,4	FILLER
OA2031	FILLER	Numeric	25,4	FILLER
OA2032	FILLER	Numeric	25,4	FILLER
PK_OA20
GO
OA2001+ OA2002+ OA2003
IX_OA202
GO
OA2004+OA2005

※ 說明
本資料表為正式既有資料表，不可重新建表。
主鍵：(OA2001 公司別) + (OA2002 客戶編號) + (OA2003 合約代號)
OA20 為合約主檔，記錄所有合約的基本資訊。
如需新增欄位，請使用 ALTER TABLE ADD 語法，勿修改既有欄位定義。

※ API 對照（前端 → DTO → 資料庫）
前端欄位	前端變數名稱	DTO欄位	資料庫欄位	說明	狀態
compId	公司代號	compId	OA2001	公司別	正式既有欄位
contractId	合約編號	contractId	OA2003	合約代號	正式既有欄位
customerId	客戶代號	customerId	OA2002	客戶編號	正式既有欄位
customerName	客戶名稱	customerName	-	需JOIN客戶主檔取得	需確認
contractType	合約類型	contractType	OA2006	合約所屬	正式既有欄位
contractStatus	合約狀態	contractStatus	OA2016	合約狀態	正式既有欄位
contractAmount	合約總價(未稅)	contractAmount	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
contractAmountTax	合約總價(含稅)	contractAmountTax	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
externalCostBudget	外包成本預算(含稅)	externalCostBudget	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
contractEndDate	合約終了日期	contractEndDate	OA0405	結束日	正式既有欄位
contractStartDate	合約起始日	contractStartDate	OA2004	起始日	正式既有欄位
extendControlDate	展期控制日期	extendControlDate	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
currentSales	目前業務	currentSales	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
currentSalesName	業務員姓名	currentSalesName	-	需JOIN員工主檔取得	需確認
createDate	建檔日期	createDate	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
contractFileUrl	合約文件URL	contractFileUrl	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
remark	備註	remark	OA2011	注意事項	正式既有欄位
newOldCustomer	新舊客戶別	newOldCustomer	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
※ SQL 補欄位建議（如需擴展）
ALTER TABLE OA20 ADD OA2033 NVARCHAR(20) NULL -- 合約總價(未稅)
ALTER TABLE OA20 ADD OA2034 NVARCHAR(20) NULL -- 合約總價(含稅)
ALTER TABLE OA20 ADD OA2035 NVARCHAR(20) NULL -- 外包成本預算(含稅)
ALTER TABLE OA20 ADD OA2036 NVARCHAR(10) NULL -- 展期控制日期
ALTER TABLE OA20 ADD OA2037 NVARCHAR(20) NULL -- 目前業務
ALTER TABLE OA20 ADD OA2038 NVARCHAR(10) NULL -- 建檔日期
ALTER TABLE OA20 ADD OA2039 NVARCHAR(500) NULL -- 合約文件URL
ALTER TABLE OA20 ADD OA2040 NVARCHAR(1) NULL -- 新舊客戶別
