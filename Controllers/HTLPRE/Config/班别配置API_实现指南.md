# 班別配置 API - 實現指南（簡化版）

## 📋 概述

本指南說明班別配置 API 的簡化實現方案，完全參照 **CustomersController.cs** 的做法，**僅提供查詢功能**，不支持新增、修改、刪除操作。

---

## 🎯 設計原則

### 1. 參照 CustomersController.cs 模式

- ✅ **僅查詢（GET）** - 獲取班別列表
- ❌ **不支持新增（POST）** - 班別配置在資料庫中手動維護
- ❌ **不支持修改（PUT）** - 班別配置在資料庫中手動維護
- ❌ **不支持刪除（DELETE）** - 班別配置在資料庫中手動維護

### 2. 使用現有的系統代碼表

- 使用 `BlCodes` 業務邏輯層
- 使用 `MdCode` 資料模型（來自 `GUIStd.DAL.AllNewGUI.Models`）
- HelpType 固定為 `"SH"` (Shift)

### 3. API 路由規範

```
GET /htlpre/Config/Shifts?includeEmptyRow={bool}&includeId={bool}
```

---

## 📂 文件結構

```
MGUIBAAPI/
├── Controllers/
│   └── HTLPRE/
│       └── Config/
│           ├── ShiftsController.cs          ✅ 簡化版控制器（僅查詢）
│           └── 班別配置API_實現指南.md        ✅ 本文檔
```

**不需要單獨的 MdShift 模型**，因為直接使用系統的 `MdCode` 模型。

---

## ✅ ShiftsController.cs 實現

### API 端點

| 方法 | 路由                                                            | 說明         |
| ---- | --------------------------------------------------------------- | ------------ |
| GET  | `/htlpre/Config/Shifts?includeEmptyRow={bool}&includeId={bool}` | 獲取班別列表 |

### 查詢參數

- `includeEmptyRow` (bool) - 是否包含空白列（"全部"選項），預設 `true`
- `includeId` (bool) - 是否包含代碼（Id 欄位），預設 `false`

### 返回格式

```json
{
  "success": true,
  "data": {
    "shifts": [
      { "id": "", "name": "全部" },
      { "id": "A", "name": "A班" },
      { "id": "B", "name": "B班" },
      { "id": "C", "name": "C班" },
      { "id": "D", "name": "D班" }
    ]
  },
  "message": "查詢成功"
}
```

---

## 🔧 後端實現詳情

### 1. 控制器架構

```csharp
[Route("htlpre/Config/[controller]")]
public class ShiftsController : GUIAppAuthController
{
    private BlCodes BlCodes => new BlCodes(ClientContent);

    [HttpGet]
    public IActionResult Get(bool includeEmptyRow = true, bool includeId = false)
    {
        try
        {
            // 使用 BlCodes.GetHelp() 獲取班別配置
            var shifts = BlCodes.GetHelp("SH", CurrentLang, includeEmptyRow, includeId);

            return Ok(new {
                success = true,
                data = new { shifts }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {
                success = false,
                message = $"獲取班別配置失敗: {ex.Message}"
            });
        }
    }
}
```

### 2. 使用的 BLL 方法

```csharp
// BlCodes.GetHelp() 方法簽名
public List<MdCode> GetHelp(
    string helpType,      // "SH" - Shift
    string lang,          // 語言代碼（如 "zh-TW"）
    bool includeEmptyRow, // 是否包含空白列
    bool includeId        // 是否包含代碼欄位
)
```

### 3. 資料模型 (MdCode)

```csharp
// 來自 GUIStd.DAL.AllNewGUI.Models.MdCode
public class MdCode
{
    public string Id { get; set; }    // 班別代碼（A, B, C, D）
    public string Name { get; set; }  // 班別名稱（A班, B班...）
}
```

---

## 🌐 前端整合

### 1. ShiftAPI（vhtrgm09.js）

```javascript
const ShiftAPI = {
  defaultShifts: [
    { code: "", name: "全部" },
    { code: "A", name: "A班" },
    { code: "B", name: "B班" },
    { code: "C", name: "C班" },
    { code: "D", name: "D班" },
  ],

  getShifts(includeAll = true) {
    try {
      const result = g$.CallRestAPI({
        apiUrl: `htlpre/Config/Shifts?includeEmptyRow=${includeAll}&includeId=true`,
        callType: g$.Const.apiMethods.get,
        authRequired: true,
      });

      if (result && result.success && result.data && result.data.shifts) {
        // 轉換 id -> code
        return result.data.shifts.map((shift) => ({
          code: shift.id,
          name: shift.name,
        }));
      } else {
        return this.getDefaultShifts(includeAll);
      }
    } catch (error) {
      return this.getDefaultShifts(includeAll);
    }
  },

  getDefaultShifts(includeAll = true) {
    return includeAll
      ? [...this.defaultShifts]
      : this.defaultShifts.filter((s) => s.code !== "");
  },
};
```

### 2. Vue 實例使用

```javascript
// 在 Vue 實例的 mounted() 中
mounted() {
    // 獲取班別配置（包含「全部」）
    this.shifts = ShiftAPI.getShifts(true);

    // 或透過 VueHelpers
    await VueHelpers.fetchShifts(this, true);
}
```

---

## 🗄️ 資料庫配置

### 1. Codes 表結構（系統代碼表）

| 欄位      | 類型         | 說明                      |
| --------- | ------------ | ------------------------- |
| HelpType  | varchar(20)  | 代碼類型（固定為 "SH"）   |
| Id        | varchar(20)  | 班別代碼（A, B, C, D）    |
| Name      | nvarchar(50) | 班別名稱（A 班, B 班...） |
| SortOrder | int          | 排序順序                  |
| IsActive  | bit          | 是否啟用                  |
| Lang      | varchar(10)  | 語言代碼                  |

### 2. 初始化 SQL 腳本

請使用提供的 `初始化班別數據.sql` 腳本來設置初始班別配置：

```sql
-- 插入默認班別
INSERT INTO Codes (HelpType, Id, Name, SortOrder, IsActive, Lang)
VALUES
    ('SH', '', '全部', 0, 1, 'zh-TW'),
    ('SH', 'A', 'A班', 1, 1, 'zh-TW'),
    ('SH', 'B', 'B班', 2, 1, 'zh-TW'),
    ('SH', 'C', 'C班', 3, 1, 'zh-TW'),
    ('SH', 'D', 'D班', 4, 1, 'zh-TW');
```

---

## 📋 測試檢查清單

### 1. API 測試

- [ ] GET `/htlpre/Config/Shifts` - 返回所有班別（包含「全部」）
- [ ] GET `/htlpre/Config/Shifts?includeEmptyRow=false` - 返回所有班別（不含「全部」）
- [ ] GET `/htlpre/Config/Shifts?includeId=true` - 返回班別及代碼
- [ ] 驗證返回格式符合規範
- [ ] 測試當資料庫無資料時的降級處理

### 2. 前端測試

- [ ] 班別下拉選單正確顯示
- [ ] API 失敗時自動使用默認班別配置
- [ ] 班別選擇功能正常
- [ ] 多語言支持正常

### 3. 整合測試

- [ ] 前後端資料格式轉換正確（`id` ↔ `code`）
- [ ] 查詢條件中的班別篩選正常
- [ ] 報表中的班別顯示正常

---

## 🔍 與 CustomersController.cs 的對比

| 項目         | CustomersController.cs | ShiftsController.cs                         |
| ------------ | ---------------------- | ------------------------------------------- |
| 路由         | `/gui/[controller]`    | `/htlpre/Config/[controller]`               |
| 業務層       | `BlCustomers`          | `BlCodes`                                   |
| 資料模型     | `MdCustomer`           | `MdCode`（系統模型）                        |
| GET 方法     | `Get()`                | `Get(bool includeEmptyRow, bool includeId)` |
| 包含空白列   | 透過業務層參數         | 透過 `includeEmptyRow` 參數                 |
| 包含代碼欄位 | 透過業務層參數         | 透過 `includeId` 參數                       |
| CRUD 操作    | 僅 GET                 | 僅 GET                                      |

---

## 📝 總結

### 優點

1. ✅ **簡單明瞭** - 僅提供查詢功能，邏輯清晰
2. ✅ **符合規範** - 完全參照 CustomersController.cs 的實現
3. ✅ **使用系統模型** - 不需要單獨的 MdShift 模型
4. ✅ **易於維護** - 班別配置在資料庫中手動維護
5. ✅ **降級機制** - 前端 API 失敗時自動使用默認配置

### 注意事項

1. ⚠️ **班別管理** - 班別的新增、修改、刪除需要在資料庫中手動操作
2. ⚠️ **多語言支持** - 需要在 Codes 表中為每種語言插入對應記錄
3. ⚠️ **參照完整性** - 刪除班別前需確保沒有其他資料引用該班別

---

## 📚 相關文件

- `CustomersController.cs` - 參考實現
- `BlCodes.cs` - 業務邏輯層
- `MdCode.cs` - 資料模型
- `初始化班別數據.sql` - 資料庫初始化腳本
- `ShiftAPI_使用說明.md` - 前端 API 使用文檔

---

**版本**: 2.0（簡化版）  
**更新日期**: 2025-10-30  
**作者**: System  
**參考**: CustomersController.cs, HTSH.pdf


















