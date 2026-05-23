NetOA欄位說明-OA21
Key	FieldName	DataType	Length	Description
OA2101	ID	VarChar	20	ID
OA2102	公司別	VarChar	8	公司別
OA2103	客戶編號	VarChar	30	客戶編號
OA2104	合約代號	VarChar	30	合約代號
OA2105	模組類別	VarChar	50	模組類別
OA2106	模組系統	VarChar	50	模組系統
OA2107	備註說明	VarChar	50	備註說明
OA2108	訂製程式名稱	VarChar	100	訂製程式名稱
OA2109	訂製程式內容說明	VarChar	100	訂製程式內容說明
OA2110	FILLER	VarChar	10	FILLER
OA2111	FILLER	VarChar	10	FILLER
OA2112	FILLER	VarChar	10	FILLER
OA2113	FILLER	VarChar	10	FILLER
OA2114	FILLER	VarChar	10	FILLER
OA2115	FILLER	VarChar	10	FILLER
OA2116	FILLER	VarChar	10	FILLER
PK_OA21
GO
OA2101

※ 說明
本資料表為正式既有資料表，不可重新建表。
主鍵：OA2101（ID）
OA21 為產品/服務資料表，記錄合約中的模組項目。
OA2105~OA2109 為目前有用欄位，OA2110~OA2116 為 FILLER 保留欄位。
如需擴展，建議優先使用 FILLER 欄位或使用 ALTER TABLE ADD 新增。

※ API 對照（前端 → DTO → 資料庫）
前端欄位	前端變數名稱	DTO欄位	資料庫欄位	說明	狀態
compId	公司代號	compId	OA2102	公司別	正式既有欄位
customerId	客戶編號	customerId	OA2103	客戶編號	正式既有欄位
contractId	合約編號	contractId	OA2104	合約代號	正式既有欄位
productId	產品代號	productId	OA2101	ID	正式既有欄位（作為產品ID）
productName	產品名稱	productName	-	需JOIN產品主檔取得	需確認
productCategory	產品類別	productCategory	OA2105	模組類別	正式既有欄位
productSystem	產品系統	productSystem	OA2106	模組系統	正式既有欄位
customProgramName	訂製程式名稱	customProgramName	OA2108	訂製程式名稱	正式既有欄位
customProgramDesc	訂製程式內容說明	customProgramDesc	OA2109	訂製程式內容說明	正式既有欄位
remark	備註	remark	OA2107	備註說明	正式既有欄位
salesAmount	銷售單價(含稅)	salesAmount	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
externalCostAmount	外包成本單價	externalCostAmount	-	尚無對應欄位，建議使用ALTER TABLE新增	需確認
warrantyStartDate	保固開始日期	warrantyStartDate	-	建議使用OA2112（暫用FILLER）	FILLER暫用
warrantyEndDate	保固終了日期	warrantyEndDate	-	建議使用OA2113（暫用FILLER）	FILLER暫用
maintenanceStartDate	維護合約起始日	maintenanceStartDate	-	建議使用OA2114（暫用FILLER）	FILLER暫用
maintenanceEndDate	維護合約終了日	maintenanceEndDate	-	建議使用OA2115（暫用FILLER）	FILLER暫用
rentalStartDate	租用合約起始日	rentalStartDate	-	建議使用OA2116（暫用FILLER）	FILLER暫用
rentalEndDate	租用合約終了日	rentalEndDate	-	尚無對應，建議使用ALTER TABLE新增	需確認
expectedMaintenanceAmount	預計維護金額	expectedMaintenanceAmount	-	尚無對應，建議使用ALTER TABLE新增	需確認
currentPM	目前PM	currentPM	-	尚無對應，建議使用ALTER TABLE新增	需確認
※ SQL 補欄位建議（如需擴展）
ALTER TABLE OA21 ADD OA2117 NVARCHAR(20) NULL -- 銷售單價(含稅)
ALTER TABLE OA21 ADD OA2118 NVARCHAR(20) NULL -- 外包成本單價
ALTER TABLE OA21 ADD OA2119 NVARCHAR(10) NULL -- 租用合約終了日
ALTER TABLE OA21 ADD OA2120 NVARCHAR(20) NULL -- 預計維護金額
ALTER TABLE OA21 ADD OA2121 NVARCHAR(20) NULL -- 目前PM
