# vOAM15 - 合約主檔 API 規格

## 一、API 服務終點摘要

| 功能 | Method | Endpoint | 說明 |
|------|--------|----------|------|
| 查詢分頁 | POST | `/oa/contracts/query/pages/{pageNo}` | 查詢合約分頁資料 |
| 取得單筆 | GET | `/oa/contracts/{compId}/{contractId}` | 取得合約單筆資料 |
| 輔助查詢 | GET | `/oa/contracts/help/{compId}/{queryText}/pages/{pageNo}` | 合約輔助查詢（分頁） |
| 新增 | POST | `/oa/contracts` | 新增合約 |
| 更新 | PUT | `/oa/contracts/{compId}/{contractId}` | 更新合約 |
| 刪除 | DELETE | `/oa/contracts/{compId}/{contractId}` | 刪除合約 |

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

**Response:** `MdContract_p`
```json
{
  "codes": [
    {
      "contractId": "DEMO001",
      "compId": "GUEST",
      "customerId": "C001",
      "customerName": "測試客戶A",
      "newOldCustomer": "N",
      "contractAmount": 1000000,
      "contractAmountTax": 1050000,
      "externalCostBudget": 200000,
      "contractStatus": "Active",
      "contractEndDate": "2026/12/31",
      "extendControlDate": "2026/10/01",
      "createDate": "2025/01/15",
      "remark": ""
    }
  ],
  "paging": {
    "currentPage": 1,
    "rowsPerPage": 20,
    "totalRows": 100
  }
}
```

### 2.2 取得合約單筆資料

```
GET /oa/contracts/{compId}/{contractId}
```

**Response:** `MdContract`

### 2.3 輔助查詢（分頁）

```
GET /oa/contracts/help/{compId}/{queryText}/pages/{pageNo}
```

**Response:** `MdContract_p`

### 2.4 新增合約

```
POST /oa/contracts
```

**Request Body:** `MdContract`

**Response:** `MdApiMessage`

### 2.5 更新合約

```
PUT /oa/contracts/{compId}/{contractId}
```

**Request Body:** `MdContract`

**Response:** `MdApiMessage`

### 2.6 刪除合約

```
DELETE /oa/contracts/{compId}/{contractId}
```

**Response:** `MdApiMessage`

---

## 三、模型定義

### MdContract_q（查詢參數）
| 欄位 | 類型 | 說明 |
|------|------|------|
| CompId | string | 公司別 |
| CustomerId | string | 客戶編號 |
| ContractStatus | string | 合約狀態 |
| QueryText | string | 關鍵字查詢 |

### MdContract（合約資料）
| 欄位 | 類型 | 說明 |
|------|------|------|
| CompId | string | 公司別 |
| ContractId | string | 合約編號 |
| CustomerId | string | 客戶編號 |
| CustomerName | string | 客戶名稱 |
| NewOldCustomer | string | 新舊客戶 (N/O) |
| ContractEndDate | string | 合約終了日 |
| ContractAmount | decimal | 合約金額 |
| ContractAmountTax | decimal | 含稅金額 |
| ExternalCostBudget | decimal | 外包成本預算 |
| ContractStatus | string | 合約狀態 |
| Remark | string | 備註 |
| ExtendControlDate | string | 展期控制日 |
| ContractFileUrl | string | 合約檔案URL |
| CreateDate | string | 建檔日期 |

### MdContract_p（分頁結果）
| 欄位 | 類型 | 說明 |
|------|------|------|
| Codes | List\<MdContract\> | 合約資料清單 |
| Paging | PagingInfo | 分頁資訊 |

### MdApiMessage（系統訊息）
| 欄位 | 類型 | 說明 |
|------|------|------|
| Success | bool | 是否成功 |
| Message | string | 訊息內容 |
| Result | object | 結果資料 |
