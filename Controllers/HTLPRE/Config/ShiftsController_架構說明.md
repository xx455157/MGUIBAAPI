# ShiftsController 架構說明

## ✅ 正確的架構模式

### 三層架構

```
Controller (ShiftsController)
    ↓
BL (BlHTSH)
    ↓
DA (DaHTSH)
    ↓
Database (HTSH Table)
```

---

## 📂 文件結構

```
Controllers/
└── HTLPRE/
    └── Config/
        └── ShiftsController.cs          ✅ API 控制器

Packages/SRC/
├── GUIStd.BLL.AllNewHTL/
│   └── Share/
│       └── BlHTSH.cs                    ✅ 業務邏輯層（新增）
│
└── GUIStd.DAL.AllNewHTL/
    └── DAO/
        └── Share/
            └── DaHTSH.cs                ✅ 資料存取層（已存在）
```

---

## 🔧 完整實現

### 1. Controller 層 (ShiftsController.cs)

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Config
{
    /// <summary>
    /// 班別資料控制器
    /// </summary>
    [Route("htlpre/Config/[controller]")]
    public class ShiftsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTSH BlHTSH => new BlHTSH(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得班別資料
        /// </summary>
        /// <param name="sh02">會計日期（格式: YYYYMMDD）</param>
        /// <param name="sh04">類型代碼</param>
        /// <param name="sh09">工作站/機台</param>
        /// <returns>班別代碼</returns>
        [HttpGet]
        public string Get([FromQuery] string sh02, [FromQuery] string sh04, [FromQuery] string sh09)
        {
            return BlHTSH.GetHotelShift(sh02, sh04, sh09);
        }

        #endregion
    }
}
```

**關鍵特點**:

- ✅ 使用 **BL 層** (`BlHTSH`)，而不是直接使用 DA 層
- ✅ 直接返回 **`string`** 類型，而不是 `IActionResult`
- ✅ 不使用 `Ok()` 或 `{ success: true, ... }` 包裹
- ✅ 遵循 **EmployeesController.cs** 的模式

---

### 2. Business Logic 層 (BlHTSH.cs)

```csharp
#region " 匯入的名稱空間：Framework "

using GUIStd.Models;
using GUIStd.DAL.AllNewHTL;

#endregion

namespace GUIStd.BLL.AllNewHTL
{
    /// <summary>
    /// 班別商業邏輯類別
    /// </summary>
    public class BlHTSH : Base
    {
        #region " 私用變數/屬性 "

        /// <summary>
        /// HTSH資料存取物件屬性
        /// </summary>
        private DaHTSH DaoHTSH => mDaoHTSH = mDaoHTSH ?? new DaHTSH(this.ClientContent);
        private DaHTSH mDaoHTSH;

        #endregion

        #region " 建構子 "

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="clientContent">目前請求的前端資訊模型物件</param>
        public BlHTSH(MdClientInfo clientContent) : base(clientContent) { }

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得飯店班別
        /// </summary>
        /// <param name="sh02">會計日期（格式: YYYYMMDD）</param>
        /// <param name="sh04">類型代碼</param>
        /// <param name="sh09">工作站/機台</param>
        /// <returns>班別代碼</returns>
        public string GetHotelShift(string sh02, string sh04, string sh09)
        {
            return DaoHTSH.GetHotelShift(sh02, sh04, sh09);
        }

        #endregion
    }
}
```

**關鍵特點**:

- ✅ 繼承自 **`Base`**
- ✅ 使用 **懶加載模式** (`DaoHTSH => mDaoHTSH ?? new DaHTSH(...)`)
- ✅ 調用 **DA 層** (`DaoHTSH`)

---

### 3. Data Access 層 (DaHTSH.cs - 已存在)

```csharp
namespace GUIStd.DAL.AllNewHTL
{
    public class DaHTSH : AllNewHTLBase2
    {
        public string GetHotelShift(string SH02, string SH04, string SH09)
        {
            string _shift;

            // 主查詢
            _shift = this.QuerySingle<string>(
                @"
                    SELECT SH08 FROM HTSH WHERE 1 = 1
                    AND SH02 = @SH02
                    AND SH04 = @SH04
                    AND SH05 = '03'
                    AND SH09 = @SH09
                    ORDER BY SH02 DESC, SH001 DESC, SH03 DESC",
                new List<Parameter>
                {
                    new Parameter("SH02", SH02),
                    new Parameter("SH04", SH04),
                    new Parameter("SH09", SH09)
                }
            );

            // 降級查詢：若為空白取該機台最後一筆班別
            if (string.IsNullOrWhiteSpace(_shift))
            {
                _shift = this.QuerySingle<string>(
                @"
                    SELECT TOP 1 SH08 FROM HTSH WHERE 1 = 1
                    AND SH05 = '03'
                    AND SH08 <> ''
                    AND SH09 = @SH09
                    ORDER BY SH02 DESC, SH001 DESC, SH03 DESC",
                    new List<Parameter>
                    {
                        new Parameter("SH09", SH09)
                    }
                );
            }

            return _shift;
        }
    }
}
```

**關鍵特點**:

- ✅ 執行實際的 **SQL 查詢**
- ✅ 包含 **降級邏輯**（主查詢無結果時查最後一筆）

---

## 📡 API 端點規範

### HTTP GET /htlpre/Config/Shifts

**請求參數**:

| 參數   | 類型   | 必填 | 說明        | 範例        |
| ------ | ------ | ---- | ----------- | ----------- |
| `sh02` | string | ✅   | 會計日期    | '20251030'  |
| `sh04` | string | ✅   | 類型代碼    | 'I'         |
| `sh09` | string | ✅   | 工作站/機台 | 'STATION01' |

**請求範例**:

```http
GET /htlpre/Config/Shifts?sh02=20251030&sh04=I&sh09=STATION01 HTTP/1.1
Authorization: Bearer {token}
```

**響應格式**:

```
"A"
```

**說明**:

- ✅ 直接返回字串（班別代碼）
- ❌ 不包裹在物件中
- ❌ 沒有 `{ success: true, data: {...} }` 結構

---

## 🌐 前端整合

### ShiftAPI 實現

```javascript
const ShiftAPI = {
  getShift(sh02, sh04, sh09) {
    try {
      // API 直接返回字串
      const shift = g$.CallRestAPI({
        apiUrl: `htlpre/Config/Shifts?sh02=${sh02}&sh04=${sh04}&sh09=${sh09}`,
        callType: g$.Const.apiMethods.get,
        authRequired: true,
      });

      // 檢查返回值類型
      if (shift && typeof shift === "string") {
        return shift || "A";
      } else {
        return "A";
      }
    } catch (error) {
      console.error("獲取班別失敗:", error);
      return "A";
    }
  },
};
```

### Vue 實例使用

```javascript
async mounted() {
    // 1. 取得營業日期
    const businessDate = await BusinessDateAPI.getBusinessDate('Hotel');
    const sh02 = businessDate.bkDate; // YYYYMMDD 格式

    // 2. 設定類型代碼（參考 BlCheckOut.cs）
    const sh04 = 'I';

    // 3. 取得工作站
    const sh09 = window.g$.ClientInfo.AddStation;

    // 4. 取得班別
    this.currentShift = ShiftAPI.getShift(sh02, sh04, sh09);
}
```

---

## 📊 架構對比

### ❌ 錯誤的做法（之前）

```csharp
// Controller 直接調用 DA
public class ShiftsController : GUIAppAuthController
{
    private DaHTSH DaHTSH => new DaHTSH(ClientContent); // ❌ 跳過 BL

    [HttpGet]
    public IActionResult Get(...)  // ❌ 使用 IActionResult
    {
        string shift = DaHTSH.GetHotelShift(...); // ❌ 直接調用 DA
        return Ok(new { success = true, data = new { shift } }); // ❌ 包裹在物件中
    }
}
```

**問題**:

1. ❌ 跳過 BL 層，直接從 Controller 調用 DA
2. ❌ 使用 `IActionResult` 和 `Ok()` 包裹
3. ❌ 響應格式不符合標準

---

### ✅ 正確的做法（現在）

```csharp
// Controller 調用 BL
public class ShiftsController : GUIAppAuthController
{
    private BlHTSH BlHTSH => new BlHTSH(ClientContent); // ✅ 使用 BL

    [HttpGet]
    public string Get(...)  // ✅ 直接返回 string
    {
        return BlHTSH.GetHotelShift(...); // ✅ 調用 BL
    }
}
```

**優點**:

1. ✅ 遵循三層架構（Controller → BL → DA）
2. ✅ 直接返回數據類型，不包裹
3. ✅ 符合專案標準（參考 EmployeesController.cs）

---

## 📚 參考實現

### 相同模式的控制器

#### EmployeesController.cs

```csharp
public class EmployeesController : GUIAppAuthController
{
    private BlPA BlPA => new BlPA(ClientContent);

    [HttpGet("{employeeId}")]
    public MdEmployee GetRow(string employeeId)
    {
        return BlPA.GetRowById(employeeId); // 直接返回數據
    }
}
```

#### BkDateController.cs

```csharp
public class BkDateController : GUIAppAuthController
{
    private BlBkDate BlBkDate => new BlBkDate(ClientContent);

    [HttpGet("{typeId}")]
    public MdBKDate GetHelp(string typeId)
    {
        return BlBkDate.GetHelp(typeId); // 直接返回數據
    }
}
```

#### ShiftsController.cs（當前實現）

```csharp
public class ShiftsController : GUIAppAuthController
{
    private BlHTSH BlHTSH => new BlHTSH(ClientContent);

    [HttpGet]
    public string Get([FromQuery] string sh02, [FromQuery] string sh04, [FromQuery] string sh09)
    {
        return BlHTSH.GetHotelShift(sh02, sh04, sh09); // 直接返回數據
    }
}
```

---

## ✅ 總結

### 關鍵改進

1. **架構層次**: Controller → BL → DA ✅
2. **返回類型**: 直接返回 `string`，不使用 `IActionResult` ✅
3. **響應格式**: 不包裹在 `{ success: true, ... }` 中 ✅
4. **命名空間**: 正確引用 `GUIStd.BLL.AllNewHTL` ✅
5. **模式一致**: 與 EmployeesController.cs 保持一致 ✅

### 文件清單

- ✅ **ShiftsController.cs** - API 控制器
- ✅ **BlHTSH.cs** - 業務邏輯層（新增）
- ✅ **DaHTSH.cs** - 資料存取層（已存在）
- ✅ **vhtrgm09.js** - 前端 ShiftAPI（已更新）

---

**版本**: 2.0（正確架構版本）  
**更新日期**: 2025-10-30  
**參考**: EmployeesController.cs, BkDateController.cs, BlCheckOut.cs


















