# ShiftsController 使用說明

## 📋 參數說明（根據 BlCheckOut.cs 實際使用案例）

### 實際使用案例

在 `BlCheckOut.cs` 第 184 行：

```csharp
// 取得會計日期
bkDate = DaoHTKY.GetHotelDate();

// 取得班別
shift = DaoHTSH.GetHotelShift(bkDate, "I", _timeInfo.AddStation);
```

---

## 📝 參數詳細說明

### 1. SH02 - 會計日期

**說明**: 飯店會計日期（通常是營業日期）

**格式**: `YYYYMMDD` (如 '20251030')

**獲取方式**:
```csharp
// 後端 C#
string bkDate = DaoHTKY.GetHotelDate();

// 前端 JavaScript
const businessDate = await BusinessDateAPI.getBusinessDate('Hotel');
const sh02 = businessDate.bkDate.replace(/-/g, ''); // 移除 '-' 符號
```

**API 對應**: `GET /htlpre/BkDate/Hotel`

---

### 2. SH04 - 類型代碼

**說明**: 業務類型代碼

**常見值**:
- `"I"` - Check-in（入住相關）
- 其他值待確認...

**獲取方式**:
```csharp
// 後端：根據業務邏輯固定使用
string sh04 = "I"; // 退房/入住相關
```

```javascript
// 前端：根據當前操作類型
const sh04 = 'I'; // 入住相關操作
```

---

### 3. SH09 - 工作站/機台

**說明**: 當前工作站或機台的唯一標識

**獲取方式**:
```csharp
// 後端：從交易時間物件獲取
MdTimeInfo timeInfo = new MdTimeInfo(clientInfo);
string sh09 = timeInfo.AddStation;
```

```javascript
// 前端：從客戶端資訊獲取
const sh09 = this.clientInfo.station || 'DEFAULT_STATION';

// 或從全域配置獲取
const sh09 = window.g$.ClientInfo.AddStation;
```

---

## 🔧 完整使用示例

### 後端 C# 範例

```csharp
// 在業務邏輯層（BLL）中使用
public class BlYourBusinessLogic : Base
{
    private DaHTKY DaoHTKY => new DaHTKY(this.ClientContent);
    private DaHTSH DaoHTSH => new DaHTSH(this.ClientContent);
    
    public void YourMethod(MdClientInfo clientInfo)
    {
        // 1. 取得會計日期
        string bkDate = DaoHTKY.GetHotelDate();
        
        // 2. 建立交易時間物件
        MdTimeInfo timeInfo = new MdTimeInfo(clientInfo);
        
        // 3. 取得班別
        string shift = DaoHTSH.GetHotelShift(
            bkDate,              // SH02: 會計日期
            "I",                 // SH04: 類型代碼
            timeInfo.AddStation  // SH09: 工作站
        );
        
        Console.WriteLine($"當前班別: {shift}");
    }
}
```

### 前端 JavaScript 範例

```javascript
// 在 Vue 實例的 mounted() 中
async mounted() {
    try {
        // 1. 取得營業日期
        const businessDateData = await BusinessDateAPI.getBusinessDate('Hotel');
        const bkDate = businessDateData.bkDate; // 格式: "20251030"
        
        // 2. 設定類型代碼
        const sh04 = 'I'; // 根據業務邏輯設定
        
        // 3. 取得工作站
        const sh09 = window.g$.ClientInfo.AddStation || 'UNKNOWN';
        
        // 4. 呼叫 API 取得班別
        const shift = ShiftAPI.getShift(bkDate, sh04, sh09);
        
        this.currentShift = shift;
        console.log('當前班別:', shift);
        
    } catch (error) {
        console.error('取得班別失敗:', error);
        this.currentShift = 'A'; // 使用默認班別
    }
}
```

---

## 📡 API 端點

### GET /htlpre/Config/Shifts

**完整請求範例**:
```http
GET /htlpre/Config/Shifts?sh02=20251030&sh04=I&sh09=STATION01 HTTP/1.1
Authorization: Bearer {token}
```

**成功響應**:
```json
{
  "success": true,
  "data": {
    "shift": "A",
    "sh02": "20251030",
    "sh04": "I",
    "sh09": "STATION01"
  },
  "message": "查詢成功"
}
```

---

## 🔄 完整工作流程

```
1. 取得會計日期
   ↓
   DaoHTKY.GetHotelDate() → "20251030"
   
2. 取得工作站
   ↓
   timeInfo.AddStation → "STATION01"
   
3. 設定類型代碼
   ↓
   "I" (入住相關)
   
4. 查詢班別
   ↓
   DaoHTSH.GetHotelShift("20251030", "I", "STATION01")
   ↓
   SQL: SELECT SH08 FROM HTSH 
        WHERE SH02 = '20251030'
          AND SH04 = 'I'
          AND SH05 = '03'
          AND SH09 = 'STATION01'
        ORDER BY SH02 DESC, SH001 DESC, SH03 DESC
   ↓
   返回: "A" (班別代碼)
```

---

## 🎯 前端集成建議

### 方式 1: 在 Vue 實例初始化時獲取

```javascript
new Vue({
    el: '#app',
    data() {
        return {
            businessDate: '',
            currentShift: 'A',
            station: ''
        };
    },
    async mounted() {
        // 初始化時一次性獲取所有必要資訊
        await this.initializeShiftData();
    },
    methods: {
        async initializeShiftData() {
            try {
                // 1. 取得營業日期
                const businessDateData = await BusinessDateAPI.getBusinessDate('Hotel');
                this.businessDate = businessDateData.bkDate;
                
                // 2. 取得工作站
                this.station = window.g$.ClientInfo.AddStation || 'DEFAULT';
                
                // 3. 取得班別
                const shift = ShiftAPI.getShift(
                    this.businessDate,
                    'I',
                    this.station
                );
                
                this.currentShift = shift;
                
            } catch (error) {
                console.error('初始化班別資料失敗:', error);
                this.currentShift = 'A';
            }
        }
    }
});
```

### 方式 2: 透過 VueHelpers 封裝

```javascript
// 在 VueHelpers 中封裝完整邏輯
VueHelpers = {
    async initializeBusinessData(vueInstance) {
        // 1. 取得營業日期
        await this.fetchBusinessDate(vueInstance, 'Hotel');
        
        // 2. 取得班別（自動使用營業日期和工作站）
        const bkDate = vueInstance.businessDate.replace(/-/g, '');
        const station = window.g$.ClientInfo.AddStation || 'DEFAULT';
        this.fetchShift(vueInstance, bkDate, 'I', station);
    }
};

// 在 Vue 實例中使用
async mounted() {
    await VueHelpers.initializeBusinessData(this);
    console.log('營業日期:', this.businessDate);
    console.log('當前班別:', this.currentShift);
}
```

---

## ⚠️ 注意事項

### 1. 參數必填驗證

所有三個參數都是必填的，缺少任何一個都會導致查詢失敗：

```javascript
// ❌ 錯誤：缺少參數
const shift = ShiftAPI.getShift('20251030', 'I'); // 缺少 sh09

// ✅ 正確：提供所有參數
const shift = ShiftAPI.getShift('20251030', 'I', 'STATION01');
```

### 2. 日期格式

- ✅ 正確格式: `'20251030'` (YYYYMMDD)
- ❌ 錯誤格式: `'2025-10-30'`, `'2025/10/30'`

```javascript
// 正確的格式轉換
const formattedDate = '2025-10-30'.replace(/-/g, ''); // '20251030'
```

### 3. 工作站獲取

確保工作站資訊已正確初始化：

```javascript
// 檢查工作站是否存在
const station = window.g$?.ClientInfo?.AddStation;
if (!station) {
    console.warn('工作站資訊未初始化，使用默認值');
}
const sh09 = station || 'DEFAULT_STATION';
```

---

## 📚 相關文件

- **BlCheckOut.cs** - 實際使用案例（第 184 行）
- **DaHTSH.cs** - 資料存取層實現
- **ShiftsController.cs** - API 控制器
- **vhtrgm09.js** - 前端 ShiftAPI 實現

---

**版本**: 1.1（基於實際使用案例）  
**更新日期**: 2025-10-30  
**參考**: BlCheckOut.cs Line 184
