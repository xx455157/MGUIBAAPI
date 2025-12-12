# 班別配置 API - 實現指南（HTSH 表）

## 📋 概述

本文檔說明基於 **HTSH 表**的班別配置 API 實現，使用 `DaHTSH.GetHotelShift()` 方法查詢班別。

---

## 🎯 設計原則

### 1. 使用 HTSH 專用表

- ✅ **表名**: `HTSH` （HTL Shift History）
- ✅ **查詢字段**: `SH08` （班別代碼）
- ✅ **條件字段**: `SH02`（日期）, `SH04`, `SH05`（固定='03'）, `SH09`（機台/位置）
- ✅ **排序字段**: `SH02 DESC, SH001 DESC, SH03 DESC`

### 2. SQL 查詢邏輯

```sql
-- 主查詢：根據條件查詢班別
SELECT SH08 
FROM HTSH 
WHERE SH02 = @SH02        -- 日期參數
  AND SH04 = @SH04        -- 參數 SH04
  AND SH05 = '03'         -- 固定條件
  AND SH09 = @SH09        -- 機台/位置參數
ORDER BY SH02 DESC, SH001 DESC, SH03 DESC;

-- 降級查詢：若查不到，查該機台最後一筆班別
SELECT TOP 1 SH08 
FROM HTSH 
WHERE SH05 = '03'
  AND SH08 <> ''
  AND SH09 = @SH09
ORDER BY SH02 DESC, SH001 DESC, SH03 DESC;
```

---

## 📂 文件結構

```
MGUIBAAPI/
├── Controllers/
│   └── HTLPRE/
│       └── Config/
│           ├── ShiftsController.cs                   ✅ 班別控制器
│           └── 班別配置API_實現指南_HTSH.md           ✅ 本文檔

GUIMobile/Packages/SRC/
├── GUIStd.DAL.AllNewHTL/
│   └── DAO/
│       └── Share/
│           └── DaHTSH.cs                            ✅ 資料存取層（已存在）
```

---

## 🔧 後端實現

### 1. ShiftsController.cs

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.DAL.AllNewHTL;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Config
{
    /// <summary>
    /// 【需經驗證】HTL 班別資料控制器
    /// 使用 HTSH 表查詢班別
    /// </summary>
    [Route("htlpre/Config/[controller]")]
    public class ShiftsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 資料存取物件屬性
        /// </summary>
        private DaHTSH DaHTSH => new DaHTSH(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得班別資料
        /// SQL: SELECT SH08 FROM HTSH 
        ///      WHERE SH02 = @SH02 AND SH04 = @SH04 AND SH05 = '03' AND SH09 = @SH09
        ///      ORDER BY SH02 DESC, SH001 DESC, SH03 DESC
        /// </summary>
        /// <param name="sh02">日期參數（格式: YYYYMMDD）</param>
        /// <param name="sh04">參數 SH04</param>
        /// <param name="sh09">機台/位置參數</param>
        /// <returns>班別代碼</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string sh02, [FromQuery] string sh04, [FromQuery] string sh09)
        {
            try
            {
                // 使用 DaHTSH.GetHotelShift 查詢班別
                // 若查不到，會自動查詢該機台最後一筆班別
                string shift = DaHTSH.GetHotelShift(sh02, sh04, sh09);

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        shift = shift ?? "",
                        sh02 = sh02,
                        sh04 = sh04,
                        sh09 = sh09
                    },
                    message = "查詢成功"
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"獲取班別失敗: {ex.Message}"
                });
            }
        }

        #endregion
    }
}
```

### 2. DaHTSH.cs（已存在）

路徑: `GUIMobile/Packages/SRC/GUIStd.DAL.AllNewHTL/DAO/Share/DaHTSH.cs`

```csharp
/// <summary>
/// 取得班別資料
/// </summary>		
/// <returns>SHIFT</returns>
public string GetHotelShift(string SH02, string SH04, string SH09)
{
    string _shift;

    // 主查詢：根據完整條件查詢
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
```

---

## 🌐 前端整合

### 1. ShiftAPI（vhtrgm09.js）

```javascript
const ShiftAPI = {
    defaultShifts: [
        { code: '', name: '全部' },
        { code: 'A', name: 'A班' },
        { code: 'B', name: 'B班' },
        { code: 'C', name: 'C班' },
        { code: 'D', name: 'D班' },
    ],
    
    /**
     * 獲取當前班別
     * @param {string} sh02 - 日期參數（格式: YYYYMMDD）
     * @param {string} sh04 - 參數 SH04
     * @param {string} sh09 - 機台/位置參數
     * @returns {string} 班別代碼（如 'A', 'B', 'C'...）
     */
    getShift(sh02, sh04, sh09) {
        try {
            const result = g$.CallRestAPI({
                apiUrl: `htlpre/Config/Shifts?sh02=${sh02}&sh04=${sh04}&sh09=${sh09}`,
                callType: g$.Const.apiMethods.get,
                authRequired: true,
            });
            
            if (result && result.success && result.data && result.data.shift !== undefined) {
                return result.data.shift;
            } else {
                return 'A'; // 默認返回 A 班
            }
        } catch (error) {
            console.error('❌ ShiftAPI.getShift: 獲取班別失敗:', error);
            return 'A'; // 默認返回 A 班
        }
    },
    
    getDefaultShifts(includeAll = true) {
        return includeAll 
            ? [...this.defaultShifts] 
            : this.defaultShifts.filter(s => s.code !== '');
    },
};
```

### 2. Vue 實例使用

```javascript
// 在 Vue 實例的 mounted() 中
mounted() {
    // 準備參數
    const sh02 = this.businessDate.replace(/-/g, ''); // '2025-10-30' -> '20251030'
    const sh04 = this.someParameter; // 根據實際業務邏輯設置
    const sh09 = this.machineId; // 機台/位置 ID
    
    // 方式 1: 直接調用 ShiftAPI
    this.currentShift = ShiftAPI.getShift(sh02, sh04, sh09);
    
    // 方式 2: 透過 VueHelpers
    VueHelpers.fetchShift(this, sh02, sh04, sh09);
}
```

---

## 📋 API 端點規範

### GET /htlpre/Config/Shifts

**請求參數**:

| 參數 | 類型 | 必填 | 說明 | 範例 |
|------|------|------|------|------|
| `sh02` | string | ✅ | 日期參數 | '20251030' |
| `sh04` | string | ✅ | 參數 SH04 | 待確認 |
| `sh09` | string | ✅ | 機台/位置參數 | 待確認 |

**請求範例**:
```http
GET /htlpre/Config/Shifts?sh02=20251030&sh04=XXX&sh09=YYY HTTP/1.1
Authorization: Bearer {token}
```

**成功響應** (HTTP 200):
```json
{
  "success": true,
  "data": {
    "shift": "A",
    "sh02": "20251030",
    "sh04": "XXX",
    "sh09": "YYY"
  },
  "message": "查詢成功"
}
```

**失敗響應** (HTTP 500):
```json
{
  "success": false,
  "message": "獲取班別失敗: {錯誤訊息}"
}
```

---

## 🗄️ HTSH 表結構

### 主要字段說明

| 字段 | 類型 | 說明 | 備註 |
|------|------|------|------|
| `SH001` | varchar | 序號字段 | 排序使用 |
| `SH02` | varchar | 日期參數 | 格式: YYYYMMDD，主要篩選條件 |
| `SH03` | varchar | 字段 SH03 | 排序使用 |
| `SH04` | varchar | 參數 SH04 | 篩選條件之一 |
| `SH05` | varchar | 類型字段 | 固定值='03'，代表班別類型 |
| `SH08` | varchar | **班別代碼** | **查詢目標字段**（A, B, C, D...） |
| `SH09` | varchar | 機台/位置參數 | 篩選條件之一 |

### 查詢邏輯

1. **主查詢**: 根據 `SH02`, `SH04`, `SH05='03'`, `SH09` 精確查詢
2. **降級查詢**: 若主查詢無結果，則查詢該機台（`SH09`）最後一筆非空班別
3. **排序規則**: `SH02 DESC, SH001 DESC, SH03 DESC`（最新的在前）

---

## 📝 參數說明與獲取方式

### SH02 - 日期參數

**格式**: `YYYYMMDD` (如 '20251030')

**獲取方式**:
```javascript
// 從營業日期轉換
const businessDate = '2025-10-30'; // 從 BusinessDateAPI 獲取
const sh02 = businessDate.replace(/-/g, ''); // '20251030'

// 或使用當天日期
const today = new Date();
const sh02 = today.toISOString().split('T')[0].replace(/-/g, '');
```

### SH04 - 參數 SH04

**說明**: 待確認業務邏輯

**獲取方式**: 待確認（可能是用戶 ID、部門代碼等）

```javascript
const sh04 = this.userId; // 範例：使用用戶 ID
```

### SH09 - 機台/位置參數

**說明**: 機台或工作位置的唯一標識

**獲取方式**: 待確認（可能是機台 ID、櫃檯編號等）

```javascript
const sh09 = this.machineId; // 範例：使用機台 ID
```

---

## 🧪 測試案例

### 測試 1: 基本查詢

```bash
# 請求
GET /htlpre/Config/Shifts?sh02=20251030&sh04=TEST001&sh09=MACHINE01

# 預期響應
{
  "success": true,
  "data": {
    "shift": "A",
    "sh02": "20251030",
    "sh04": "TEST001",
    "sh09": "MACHINE01"
  },
  "message": "查詢成功"
}
```

### 測試 2: 降級查詢（主查詢無結果）

```bash
# 請求（使用不存在的 SH02）
GET /htlpre/Config/Shifts?sh02=19990101&sh04=TEST001&sh09=MACHINE01

# 預期響應（返回該機台最後一筆班別）
{
  "success": true,
  "data": {
    "shift": "C",
    "sh02": "19990101",
    "sh04": "TEST001",
    "sh09": "MACHINE01"
  },
  "message": "查詢成功"
}
```

### 測試 3: 前端集成測試

```javascript
// 測試基本查詢
const shift = ShiftAPI.getShift('20251030', 'TEST001', 'MACHINE01');
console.assert(shift !== undefined, '應返回班別代碼');

// 測試降級機制（模擬 API 失敗）
g$.CallRestAPI = () => { throw new Error('Test Error'); };
const fallbackShift = ShiftAPI.getShift('20251030', 'TEST001', 'MACHINE01');
console.assert(fallbackShift === 'A', '應返回默認班別 A');
```

---

## ⚠️ 注意事項

### 1. 參數必填

- ❌ **所有 3 個參數都必須提供**，否則 API 無法正確查詢
- ⚠️ **前端需確保參數值正確**，避免查詢不到數據

### 2. 日期格式

- ✅ 必須使用 `YYYYMMDD` 格式（如 '20251030'）
- ❌ 不可使用 `YYYY-MM-DD` 格式（如 '2025-10-30'）

### 3. 降級邏輯

- 主查詢無結果時，會自動查詢該機台最後一筆非空班別
- 若兩次查詢都無結果，返回空字符串 `""`
- 前端會將空字符串處理為默認班別 `'A'`

### 4. 資料一致性

- HTSH 表數據由前台系統維護
- 班別變更需在前台系統中記錄到 HTSH 表

---

## 🔍 問題排查

### 問題 1: 查詢總是返回空字符串

**可能原因**:
1. HTSH 表中沒有對應的數據
2. SH05 <> '03' (固定條件不匹配)
3. 參數格式錯誤（特別是 SH02 日期格式）

**解決方案**:
```sql
-- 檢查 HTSH 表數據
SELECT * FROM HTSH 
WHERE SH05 = '03' 
  AND SH09 = 'MACHINE01'
ORDER BY SH02 DESC;

-- 確認是否有數據
```

### 問題 2: 降級查詢返回舊班別

**說明**: 這是正常行為

**原因**: 主查詢無結果時，降級查詢會返回該機台最後一筆非空班別

**解決方案**: 確保 HTSH 表有當天的數據記錄

---

## 📚 相關資源

- **DaHTSH.cs** - 資料存取層實現
- **ShiftsController.cs** - API 控制器
- **HTSH.pdf** - HTSH 表結構說明（如有）
- **vhtrgm09.js** - 前端 ShiftAPI 實現

---

**版本**: 1.0（HTSH 表版本）  
**更新日期**: 2025-10-30  
**作者**: System  
**參考**: DaHTSH.cs, 用戶提供的 SQL 查詢
