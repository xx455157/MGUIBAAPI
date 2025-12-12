# 帳單明細表 API 設計說明

## 概述

帳單明細表 (vHTRGM09) 是一個用於查詢帳務明細資料的報表功能。本設計基於前端頁面 `vHTRGM09.html` 的實際需求，參考 `CustomersController` 的實現方式。

---

## 檔案結構

```
Controllers/HTLPRE/Reports/
└── ReportsController.cs           # 報表控制器

GUIStd.DAL.AllNewGUI/Models/Private/HTL/vHTRGM09/
├── MdAccountDetailReportQuery.cs  # 查詢條件模型 (DAL)
└── MdAccountDetailReport.cs       # 報表資料模型 (DAL)
```

---

## API 端點設計

### 查詢帳單明細表資料

**端點**: `POST /htlpre/Reports/AccountDetail`

**請求 Body**: `MdAccountDetailReportQuery`

```json
{
  "dateRange": ["20241226", "20241227"],
  "shift": "A",
  "roomNumber": "R201"
}
```

**回應**: `IEnumerable<MdAccountDetailReport>`

```json
[
  {
    "roomBooking": "R201-34567",
    "shift": "A班",
    "subject": "01房租收入",
    "amount": 3200,
    "billName": "房租",
    "receiptNumber": "12345",
    "receivableCode": "AC001",
    "remark": "標準房房租",
    "internalRemark": "VIP客戶",
    "createDateTime": "2024-12-27 08:00:45",
    "registrar": "王小明"
  }
]
```

---

## 資料模型設計

### 1. 查詢條件模型 (MdAccountDetailReportQuery)

| 屬性名稱   | 類型     | 說明                                     | 必填 |
| ---------- | -------- | ---------------------------------------- | ---- |
| DateRange  | string[] | 日期範圍，格式: ["YYYYMMDD", "YYYYMMDD"] | 是   |
| Shift      | string   | 班別代碼，最大長度 10                    | 否   |
| RoomNumber | string   | 房號，最大長度 10                        | 否   |

### 2. 報表資料模型 (MdAccountDetailReport)

| 屬性名稱       | 類型    | 說明                                  |
| -------------- | ------- | ------------------------------------- |
| RoomBooking    | string  | 房號/訂單號                           |
| Shift          | string  | 班別                                  |
| Subject        | string  | 科目                                  |
| Amount         | decimal | 金額 (正數為收入，負數為支出)         |
| BillName       | string  | 帳務名稱                              |
| ReceiptNumber  | string  | 收據號碼                              |
| ReceivableCode | string  | 應收代碼                              |
| Remark         | string  | 備註                                  |
| InternalRemark | string  | 內部備註                              |
| CreateDateTime | string  | 建立時間，格式: "YYYY-MM-DD HH:mm:ss" |
| Registrar      | string  | 登記人                                |

---

## 控制器實現

### 類別名稱

`ReportsController`

### 路由

`[Route("htlpre/[controller]")]`

### 命名空間

`MGUIBAAPI.Controllers.HTLPRE`

### 主要方法

#### AccountDetail

```csharp
[HttpPost("AccountDetail")]
public IEnumerable<MdAccountDetailReport> AccountDetail([FromBody] MdAccountDetailReportQuery queryParams)
```

根據查詢條件返回帳單明細表資料列表。

**API 路徑**: `POST /htlpre/Reports/AccountDetail`

---

## 業務邏輯層設計

### 類別名稱

`BlAccountDetailReport`

### 命名空間

`GUIStd.BLL.AllNewHTL.Private`

### 主要方法

#### Query

```csharp
public IEnumerable<MdAccountDetailReport> Query(MdAccountDetailReportQuery queryParams)
```

根據查詢條件查詢帳單明細表資料。

**參數說明**:

- `queryParams.DateRange`: 日期範圍，如果為空或長度不等於 2，則不限制日期
- `queryParams.Shift`: 班別代碼，如果為空，則不限制班別
- `queryParams.RoomNumber`: 房號，如果為空，則不限制房號

**返回**: 符合條件的帳單明細表資料列表

---

## 資料庫查詢邏輯

### 主要資料表

- **帳務交易表**: 儲存帳務明細資料
- **班別代碼表**: 班別代碼與名稱對應
- **科目代碼表**: 科目代碼與名稱對應

### 查詢重點

1. 根據日期範圍查詢 (DateRange[0] 到 DateRange[1])
2. 如果 Shift 不為空，過濾班別
3. 如果 RoomNumber 不為空，過濾房號
4. 金額欄位：正數表示收入，負數表示支出
5. 按建立時間排序（最新的在前）

---

## 前端整合

### API 呼叫範例

```javascript
// 查詢帳單明細表
const params = {
  dateRange: ["20241226", "20241227"],
  shift: "A",
  roomNumber: "R201",
};

const result = await g$.CallRestAPI({
  apiUrl: "htlpre/Reports/AccountDetail",
  callType: g$.Const.apiMethods.post,
  bodyContent: params,
  authRequired: true,
});

if (result && result.success && result.data) {
  const accountDetailResults = result.data;
  // 處理資料...
}
```

---

## 命名規範說明

### 控制器命名

- 使用 `ReportsController` 符合前端 API 命名規範
- 路由為 `htlpre/Reports`，方法為 `AccountDetail`
- 前端呼叫路徑：`htlpre/Reports/AccountDetail`

### 模型命名

- `MdAccountDetailReportQuery` - 查詢條件模型 (Md = Model)
- `MdAccountDetailReport` - 資料模型

### 屬性命名

- 使用有意義的英文名稱，對應前端欄位
- 使用 `[JsonProperty]` 標註 JSON 序列化屬性名
- 使用 `[DADisplayName]` 標註顯示名稱

---

## 注意事項

1. **日期格式**:

   - 查詢條件使用 `YYYYMMDD` 格式 (8 位數字)
   - 返回資料的建立時間使用 `YYYY-MM-DD HH:mm:ss` 格式

2. **金額欄位**:

   - 使用 `decimal` 型別
   - 正數表示收入，負數表示支出

3. **空值處理**:

   - Shift 和 RoomNumber 為空時，不限制該條件
   - DateRange 為空或長度不等於 2 時，不限制日期

4. **排序**:

   - 預設按建立時間降序排序（最新的在前）

5. **權限控制**:
   - 報表功能需驗證使用者權限

---

## 後續開發步驟

1. **建立業務邏輯層**

   - 在 `GUIStd.BLL.AllNewHTL.Private` 命名空間下建立 `BlAccountDetailReport` 類別
   - 實作 `Query` 方法

2. **建立資料存取層**

   - 建立查詢 SQL 或使用 Entity Framework
   - 處理日期範圍、班別、房號的過濾條件
   - 關聯查詢班別和科目代碼表取得名稱

3. **前端整合**
   - 確認 API 端點路徑正確
   - 處理返回資料的顯示

---

**設計日期**: 2024 年
**設計者**: 開發團隊
**參考文件**: vHTRGM09.html (前端頁面)
