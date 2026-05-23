# vOAM15 合約管理系統 - API 規格文件

**版本：** v1.0.0
**日期：** 2026-04-29
**適用系統：** MGUIBAAPI

---

## 一、API 總覽

| Controller | 路由前綴 | 用途 | 狀態 |
|---|---|---|---|
| `ContractsController` | `/oa/contracts` | 合約主檔 CRUD + 查詢 | 正常 |
| `ContractProductsController` | `/oa/contractProducts` | 產品/服務 CRUD | 正常 |
| `RevenueDetailsController` | `/oa/revenueDetails` | 收支明細 CRUD + 統計（含收支預算/發票/收款） | **已整併** |
| `PaymentStatusController` | `/oa/paymentStatus` | 收支款現況統計 | **已棄用，改由 RevenueDetailsController 計算** |
| `InvoicesController` | `/oa/invoices` | 發票 CRUD | **已刪除，整併至 RevenueDetailsController** |
| `PaymentsController` | `/oa/payments` | 收款 CRUD | **已刪除，整併至 RevenueDetailsController** |

---

## 二、合約主檔 API (ContractsController)

### 2.1 查詢合約分頁資料

```
POST /oa/contracts/query/pages/{pageNo}
```

**Request Body:**
```json
{
  "compId": "GUEST",
  "customerId": "",
  "contractStatus": "",
  "queryText": ""
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "codes": [
      {
        "contractId": "DEMO001",
        "compId": "GUEST",
        "customerId": "C001",
        "customerName": "測試客戶A",
        "newOldCustomer": "N",
        "contractType": "M",
        "contractAmount": 1000000,
        "contractAmountTax": 1050000,
        "externalCostBudget": 200000,
        "contractStatus": "Active",
        "contractEndDate": "2026/12/31",
        "extendControlDate": "2026/10/01",
        "createDate": "2025/01/15",
        "currentSales": "S001",
        "currentSalesName": "王小明",
        "contractFileUrl": "",
        "remark": ""
      }
    ],
    "paging": {
      "totalRows": 100,
      "rowsPerPage": 20,
      "currentPage": 1
    }
  }
}
```

### 2.2 取得合約完整明細

```
GET /oa/contracts/{compId}/{contractId}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "contract": { ... },
    "products": [ ... ],
    "budgets": [ ... ],
    "invoices": [ ... ],
    "payments": [ ... ],
    "paymentStatus": [ ... ],
    "summary": {
      "compId": "GUEST",
      "contractId": "DEMO001",
      "receivedAmount": 550000,
      "arAmount": 330000,
      "blAmount": 200000,
      "accrualExpenseAmount": 0
    }
  }
}
```

### 2.3 新增合約

```
POST /oa/contracts/insert
```

**Request Body:**
```json
{
  "compId": "GUEST",
  "contractId": "CT001",
  "customerId": "C001",
  "newOldCustomer": "N",
  "contractType": "M",
  "contractAmount": 1000000,
  "contractAmountTax": 1050000,
  "externalCostBudget": 200000,
  "contractStatus": "Active",
  "contractEndDate": "2026/12/31",
  "extendControlDate": "2026/10/01",
  "currentSales": "S001",
  "contractFileUrl": "",
  "remark": ""
}
```

### 2.4 更新合約

```
PUT /oa/contracts/{compId}/{contractId}
```

**Request Body:** 同新增（省略唯讀欄位）

### 2.5 刪除合約

```
DELETE /oa/contracts/{compId}/{contractId}
```

### 2.6 其他端點

| Method | URL | 說明 |
|---|---|---|
| GET | `/oa/contracts/help/{compId}/{queryText}/pages/{pageNo}` | 合約輔助查詢 |
| GET | `/oa/contracts/stats/{compId}/{customerId}` | 客戶合約統計 |
| PATCH | `/oa/contracts/{compId}/{contractId}/status` | 更新合約狀態 |
| POST | `/oa/contracts/{compId}/{contractId}/extend` | 展期合約 |
| GET | `/oa/contracts/backlog/{compId}` | Back Log 報表 |
| GET | `/oa/contracts/ar/{compId}` | AR 應收帳款報表 |
| GET | `/oa/contracts/expiring/{compId}` | 即将到期合約 |
| GET | `/oa/contracts/report/revenue/{compId}` | 營收統計報表 |
| GET | `/oa/contracts/report/cashflow/{compId}` | 資金預測報表 |

---

## 三、產品/服務 API (ContractProductsController)

### 3.1 取得產品列表

```
GET /oa/contractProducts/list/{compId}/{contractId}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "codes": [
      {
        "productId": "P001",
        "productName": "年度維護服務",
        "productCategory": "MA",
        "salesAmount": 500000,
        "externalCostAmount": 100000,
        "warrantyStartDate": "2025/01/01",
        "warrantyEndDate": "2025/12/31",
        "maintenanceStartDate": "2025/01/01",
        "maintenanceEndDate": "2025/12/31",
        "rentalStartDate": "",
        "rentalEndDate": "",
        "expectedMaintenanceAmount": 50000,
        "currentPM": "PM001"
      }
    ]
  }
}
```

### 3.2 新增產品

```
POST /oa/contractProducts/insert
```

**Request Body:**
```json
{
  "compId": "GUEST",
  "contractId": "DEMO001",
  "productId": "P001",
  "productName": "年度維護服務",
  "productCategory": "MA",
  "salesAmount": 500000,
  "externalCostAmount": 100000,
  "warrantyStartDate": "2025/01/01",
  "warrantyEndDate": "2025/12/31",
  "maintenanceStartDate": "2025/01/01",
  "maintenanceEndDate": "2025/12/31",
  "rentalStartDate": "",
  "rentalEndDate": "",
  "expectedMaintenanceAmount": 50000,
  "currentPM": "PM001",
  "remark": ""
}
```

### 3.3 更新產品

```
PUT /oa/contractProducts/{compId}/{contractId}/{productId}
```

### 3.4 刪除產品

```
DELETE /oa/contractProducts/{compId}/{contractId}/{productId}
```

### 3.5 批次新增產品

```
POST /oa/contractProducts/batch/{compId}/{contractId}
```

**Request Body:** `List<MdContractProduct_i>`

### 3.6 取得產品類別下拉

```
GET /oa/contractProducts/categories/{compId}
```

---

## 四、收支明細 API (RevenueDetailsController)

> **※ 重要變更：** OA22 已整併收支預算、發票、收款為同一表。
> OA23 發票、OA24 收款、OA25 收支款現況 皆已廢止，統一使用本控制器。

### 4.1 取得收支明細列表（OA22，含收支預算+發票+收款）

```
GET /oa/revenueDetails/list/{compId}/{customerId}/{contractId}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "codes": [
      {
        "compId": "GUEST",
        "contractId": "DEMO001",
        "seq": 1,
        "incomeExpenseType": "I",
        "budgetCategory": "維護費",
        "accountSubject": "4101",
        "itemName": "Q1維護費",
        "budgetAmount": 500000,
        "budgetDate": "2025/01/15",
        "actualAmount": 500000,
        "actualDate": "2025/01/15",
        "currentSalesId": "S001",
        "glVoucherNo": "GL-2025-001",
        "remark": "",
        "isInvoiceIssued": "Y",
        "invoiceNo": "INV-2025-001",
        "invoiceDate": "2025/01/15",
        "invoiceType": "S",
        "invoiceAmount": 525000,
        "invoiceDescription": "Q1維護費",
        "invoiceGlVoucherNo": "GL-2025-002",
        "isReceived": "Y",
        "remittanceId": "REM-2025-001",
        "checkNo": "",
        "paymentDate": "2025/01/20",
        "paymentAmount": 525000,
        "paymentGlVoucherNo": "GL-2025-003"
      }
    ]
  }
}
```

### 4.2 收支款現況統計（由 OA22 即時計算，不再獨立存 OA25）

```
GET /oa/revenueDetails/statusSummary/{compId}/{customerId}/{contractId}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "compId": "GUEST",
    "contractId": "DEMO001",
    "receivedAmount": 525000,
    "arAmount": 0,
    "blAmountNote": "需確認來源",
    "accrualExpenseAmount": 0,
    "payableAmount": 0
  }
}
```

### 4.3 新增收支明細

```
POST /oa/revenueDetails/insert
```

**Request Body:**
```json
{
  "compId": "GUEST",
  "contractId": "DEMO001",
  "seq": 0,
  "incomeExpenseType": "I",
  "budgetCategory": "維護費",
  "accountSubject": "4101",
  "itemName": "Q1維護費",
  "budgetAmount": 500000,
  "budgetDate": "2025/01/15",
  "actualAmount": 0,
  "actualDate": "",
  "currentSalesId": "S001",
  "glVoucherNo": "",
  "remark": "",
  "isInvoiceIssued": "N",
  "invoiceNo": "",
  "invoiceDate": "",
  "invoiceType": "",
  "invoiceAmount": 0,
  "invoiceDescription": "",
  "invoiceGlVoucherNo": "",
  "isReceived": "N",
  "remittanceId": "",
  "checkNo": "",
  "paymentDate": "",
  "paymentAmount": 0,
  "paymentGlVoucherNo": ""
}
```

### 4.4 更新收支明細

```
PUT /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}
```

### 4.5 刪除收支明細

```
DELETE /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}
```

### 4.6 批次新增收支明細

```
POST /oa/revenueDetails/batchSave/{compId}/{customerId}/{contractId}
```

---

## 五、收支款現況 API (PaymentStatusController - 已棄用)

> **※ 已廢止：** OA25 不再獨立存表，改由 RevenueDetailsController 即時計算。
> 以下端點已改為回傳 410 Gone，請改用 `/oa/revenueDetails/statusSummary`。

| 端點 | 狀態 |
|---|---|
| `GET /oa/paymentStatus/list/{compId}/{contractId}` | 改用 GET `/oa/revenueDetails/list` |
| `GET /oa/paymentStatus/summary/{compId}/{contractId}` | 改用 GET `/oa/revenueDetails/statusSummary` |
| `POST /oa/paymentStatus/insert` | 改用 POST `/oa/revenueDetails/insert` |
| `PUT /oa/paymentStatus/{compId}/{contractId}/{seq}` | 改用 PUT `/oa/revenueDetails/update` |
| `DELETE /oa/paymentStatus/{compId}/{contractId}/{seq}` | 改用 DELETE `/oa/revenueDetails/delete` |

---

## 六、發票 API (InvoicesController - 已刪除)

> **※ 已刪除：** OA23 發票已整併至 OA22，請改用 RevenueDetailsController。

| 舊端點 | 替代方案 |
|---|---|
| `GET /oa/invoices/list/{compId}/{contractId}` | GET `/oa/revenueDetails/list` |
| `POST /oa/invoices/insert` | POST `/oa/revenueDetails/insert` |
| `PUT /oa/invoices/{compId}/{contractId}/{seq}` | PUT `/oa/revenueDetails/update` |
| `DELETE /oa/invoices/{compId}/{contractId}/{seq}` | DELETE `/oa/revenueDetails/delete` |
| `POST /oa/invoices/batch/{compId}/{contractId}` | POST `/oa/revenueDetails/batchSave` |

---

## 七、收款 API (PaymentsController - 已刪除)

> **※ 已刪除：** OA24 收款已整併至 OA22，請改用 RevenueDetailsController。

| 舊端點 | 替代方案 |
|---|---|
| `GET /oa/payments/list/{compId}/{contractId}` | GET `/oa/revenueDetails/list` |
| `POST /oa/payments/insert` | POST `/oa/revenueDetails/insert` |
| `PUT /oa/payments/{compId}/{contractId}/{seq}` | PUT `/oa/revenueDetails/update` |
| `DELETE /oa/payments/{compId}/{contractId}/{seq}` | DELETE `/oa/revenueDetails/delete` |
| `POST /oa/payments/batch/{compId}/{contractId}` | POST `/oa/revenueDetails/batchSave` |

---

## 八、前端呼叫對照表

| 畫面 Tab | 讀取 API | 新增 API | 更新 API | 刪除 API | 狀態 |
|---|---|---|---|---|---|
| 篩選/列表 | `POST /oa/contracts/query/pages/{pageNo}` | - | - | - | 正常 |
| 合約主檔 | `GET /oa/contracts/{compId}/{contractId}` | `POST /oa/contracts/insert` | `PUT /oa/contracts/{compId}/{contractId}` | `DELETE /oa/contracts/{compId}/{contractId}` | 正常 |
| 產品/服務 | `GET /oa/contractProducts/list/{compId}/{contractId}` | `POST /oa/contractProducts/insert` | `PUT /oa/contractProducts/{compId}/{contractId}/{productId}` | `DELETE /oa/contractProducts/{compId}/{contractId}/{productId}` | 正常 |
| 收支預算 | `GET /oa/revenueDetails/list/{compId}/{customerId}/{contractId}` | `POST /oa/revenueDetails/insert` | `PUT /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}` | `DELETE /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}` | **已整併至OA22** |
| 發票 | `GET /oa/revenueDetails/list/{compId}/{customerId}/{contractId}` | `POST /oa/revenueDetails/insert` | `PUT /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}` | `DELETE /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}` | **已整併至OA22** |
| 收款 | `GET /oa/revenueDetails/list/{compId}/{customerId}/{contractId}` | `POST /oa/revenueDetails/insert` | `PUT /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}` | `DELETE /oa/revenueDetails/{compId}/{customerId}/{contractId}/{seq}` | **已整併至OA22** |
| 收支款現況 | `GET /oa/revenueDetails/statusSummary/{compId}/{customerId}/{contractId}` | - | - | - | **由OA22計算** |

※ 收支預算、發票、收款三個 Tab 共用同一份 OA22 資料，分別顯示對應欄位。

---

## 九、資料表對照

| 資料表 | 用途 | Controller | 狀態 |
|---|---|---|---|
| OA20 | 合約主檔 | ContractsController | 正常 |
| OA21 | 產品/服務 | ContractProductsController | 正常 |
| OA22 | 收支明細（含收支預算/發票/收款） | RevenueDetailsController | **已整併** |
| OA23 | 發票 | - | **草案已棄用（整併至OA22）** |
| OA24 | 收款 | - | **草案已棄用（整併至OA22）** |
| OA25 | 收支款現況 | - | **草案已棄用（改由OA22計算）** |

---

## 十、Swagger 測試方式

1. 啟動 MGUIBAAPI 專案
2. 開啟 Swagger UI：`http://localhost:5000/swagger`
3. 找到 `OA` 分組
4. 展開各 Controller 端點
5. 點擊 "Try it out" 執行測試

**測試前置條件：**
- 需先執行 `docs/vOAM15_SQL_Schema.sql` 建立資料表
- 需有有效的登入 session（Headers 包含認證 token）
- 預設測試公司代號：`GUEST`

---

## 十一、已知限制

1. **OA22 產品名稱**：目前 OA21 無 `productName` 欄位，需透過 OA22 產品主檔 JOIN取得。如無 OA22 產品主檔，產品名稱將顯示空白。
2. **收支款現況統計**：`receivedAmount` / `arAmount` / `blAmount` 由 OA23(發票) + OA24(收款) + OA22(收支預算) 計算而來，需確保這三張表的資料完整性。
3. **外幣**：目前所有金額欄位均為 decimal(18,2)，如有外幣需求需擴展欄位。
