# HTLPRE API 文档

## 📋 概述

本目录包含了为 **vHTRGM09** 前端页面开发的后端 API 控制器和数据模型。

## 🏗️ 架构说明

```
MGUIBAAPI/
├── Controllers/
│   └── HTLPRE/
│       ├── BkDateController.cs      # 营业日期管理
│       ├── RoomsController.cs       # 房间管理
│       ├── BillsController.cs       # 账务管理
│       ├── ConfigController.cs      # 配置管理
│       └── ReportsController.cs     # 报表管理
└── Models/
    └── HTLPRE/
        ├── BusinessDateModels.cs    # 营业日期 DTO
        ├── RoomModels.cs            # 房间相关 DTO
        ├── BillModels.cs            # 账务相关 DTO
        ├── ConfigModels.cs          # 配置相关 DTO
        └── ReportModels.cs          # 报表相关 DTO
```

## 📡 API 端点清单

### 1️⃣ BkDateController - 营业日期

| 方法 | 端点 | 说明 | 数据表 |
|------|------|------|--------|
| `GET` | `/htlpre/BkDate/Hotel` | 获取当前营业日期 | HTCA |

**响应示例**:
```json
{
  "businessDate": "2025-10-16T00:00:00",
  "systemDate": "2025-10-16T14:30:00",
  "businessDateString": "2025-10-16",
  "success": true,
  "message": "成功获取营业日期"
}
```

---

### 2️⃣ RoomsController - 房间管理

| 方法 | 端点 | 说明 | 数据表 |
|------|------|------|--------|
| `GET` | `/htlpre/Rooms/PendingArrivals` | 获取应到未到客人列表 | HTRV |
| `GET` | `/htlpre/Rooms/Floorplan` | 获取房间平面图数据 | HTHK, HTRT |

**Floorplan 响应示例**:
```json
{
  "success": true,
  "message": "成功获取房间平面图数据",
  "data": {
    "rooms": [
      {
        "roomNumber": "101",
        "roomTypeCode": "STD",
        "roomTypeName": "标准间",
        "floor": "1F",
        "roomStatus": "OCC",
        "cleanStatus": "CLEAN",
        "guestName": "张三",
        "roomRate": 388.00,
        "balance": 1200.00
      }
    ],
    "floors": ["1F", "2F", "3F"],
    "statusStats": {
      "totalRooms": 50,
      "occupiedRooms": 35,
      "vacantRooms": 15,
      "occupancyRate": 70.00
    }
  }
}
```

---

### 3️⃣ BillsController - 账务管理

| 方法 | 端点 | 说明 | 数据表 |
|------|------|------|--------|
| `GET` | `/htlpre/Bills/Folders` | 获取帐夹配置列表 | HTFO |
| `GET` | `/htlpre/Bills/Room/{roomNumber}` | 获取房间账务明细 | HTFX, HTHK |

**房间账务明细响应示例**:
```json
{
  "success": true,
  "message": "成功获取账务明细",
  "data": {
    "roomNumber": "101",
    "guestName": "张三",
    "totalAmount": 1200.00,
    "billDetails": [
      {
        "transactionNumber": "TX001",
        "transactionDate": "2025-10-16",
        "chargeCode": "ROOM",
        "chargeName": "房费",
        "amount": 388.00,
        "folderCode": "F01"
      }
    ],
    "folderSummary": {
      "F01": 388.00,
      "F02": 150.00
    }
  }
}
```

---

### 4️⃣ ConfigController - 配置管理

| 方法 | 端点 | 说明 | 数据表 |
|------|------|------|--------|
| `GET` | `/htlpre/Config/RoomTypes` | 获取房型配置列表 | HTRT |
| `GET` | `/htlpre/Config/Floors` | 获取楼层配置列表 | HTFL |
| `GET` | `/htlpre/Config/CurrentShift` | 获取当前班别信息 | HTSH |

**房型配置响应示例**:
```json
{
  "success": true,
  "message": "成功获取房型配置",
  "data": {
    "roomTypes": [
      {
        "roomTypeCode": "STD",
        "roomTypeName": "标准间",
        "standardRate": 388.00,
        "maxOccupancy": 2,
        "roomCount": 30,
        "isActive": true
      }
    ]
  }
}
```

---

### 5️⃣ ReportsController - 报表管理

| 方法 | 端点 | 说明 | 数据表 |
|------|------|------|--------|
| `POST` | `/htlpre/Reports/{reportType}` | 获取指定类型报表 | 多表 |

**支持的报表类型**:
- `RoomStatus` - 房态报表 (HTHK, HTRT, HTFL)
- `Revenue` - 营收报表 (HTFX)
- `GuestSource` - 客源分析 (HTVS, HTGR)
- `OccupancyRate` - 出租率报表 (HTHK, HTVS)
- `ARBalance` - 应收账款报表 (HTHF, HTFX)
- `CheckInList` - 入住清单 (HTVS)
- `CheckOutList` - 离店清单 (HTVS)
- `ReservationList` - 订房清单 (HTRV)

**请求示例**:
```json
POST /htlpre/Reports/RoomStatus
{
  "startDate": "2025-10-01",
  "endDate": "2025-10-31",
  "floor": "2F",
  "roomType": "STD"
}
```

**响应示例**:
```json
{
  "success": true,
  "message": "成功生成房态报表",
  "reportType": "RoomStatus",
  "reportTitle": "房态报表",
  "generatedAt": "2025-10-16T14:30:00",
  "data": {
    // 报表具体数据
  }
}
```

---

## 🗄️ 数据表映射

| 数据表 | 说明 | 主要字段 | 用途 |
|--------|------|----------|------|
| **HTHK** | 房间主档 | HK01(房号), HK02(房型), HK04(状态) | 房间信息、房态查询 |
| **HTRT** | 房型主档 | RT01(房型代码), RT02(房型名称), RT03(标准房价) | 房型配置 |
| **HTFL** | 楼层主档 | FL01(楼层代码), FL02(楼层名称) | 楼层配置 |
| **HTFO** | 帐夹主档 | FO01(帐夹代码), FO02(帐夹名称) | 帐夹配置 |
| **HTHF** | 客人账卡 | HF01(帐夹号), HF02(客人姓名) | 账务主档 |
| **HTFX** | 账目明细 | FX01(交易号), FX03(房号), FX05(金额) | 账务明细 |
| **HTCA** | 日历主档 | CA01(日期), CA02(营业日期) | 营业日期 |
| **HTRV** | 订房主档 | RV01(订房号), RV02(客人姓名), RV05(到店日期) | 订房、应到未到 |
| **HTVS** | 入住主档 | VS01(入住号), VS02(房号), VS03(客人姓名) | 入住、离店 |
| **HTSH** | 班别主档 | SH01(班别代码), SH02(班别名称) | 班别信息 |

---

## 🔧 待完成工作 (TODO)

### 高优先级

1. **实现数据库访问逻辑**
   - [ ] 在 `SRC` 项目中创建对应的 DAL (Data Access Layer)
   - [ ] 在 `SRC` 项目中创建对应的 BLL (Business Logic Layer)
   - [ ] 在 Controllers 中调用 BLL 获取真实数据

2. **配置依赖注入**
   - [ ] 在 `Startup.cs` 中注册 BLL 服务
   - [ ] 配置数据库连接字符串
   - [ ] 注入到 Controllers 构造函数

3. **实现认证授权**
   - [ ] 添加 JWT Token 认证
   - [ ] 为 API 端点添加 `[Authorize]` 特性
   - [ ] 实现权限验证

### 中优先级

4. **完善异常处理**
   - [ ] 创建统一的异常处理中间件
   - [ ] 添加日志记录 (使用 Serilog 或 NLog)
   - [ ] 实现友好的错误响应格式

5. **添加数据验证**
   - [ ] 为 Request DTO 添加验证特性 (`[Required]`, `[Range]` 等)
   - [ ] 实现自定义验证器
   - [ ] 添加 ModelState 验证

6. **性能优化**
   - [ ] 实现分页功能 (PagedList)
   - [ ] 添加缓存机制 (Redis 或 MemoryCache)
   - [ ] 数据库查询优化 (索引、存储过程)

### 低优先级

7. **API 文档**
   - [ ] 配置 Swagger/OpenAPI
   - [ ] 为所有 API 添加 XML 注释
   - [ ] 生成 Postman Collection

8. **单元测试**
   - [ ] 为 Controllers 编写单元测试
   - [ ] 为 BLL 编写单元测试
   - [ ] Mock 数据库访问层

---

## 📝 使用示例

### 前端调用示例 (vHTRGM09.html)

```javascript
// 获取营业日期
async fetchBusinessDateFromAPI() {
  const response = await fetch('/htlpre/BkDate/Hotel');
  const data = await response.json();
  if (data.success) {
    this.currentBusinessDate = data.businessDateString;
  }
}

// 获取房间平面图
async fetchFloorplanFromAPI() {
  const response = await fetch('/htlpre/Rooms/Floorplan');
  const result = await response.json();
  if (result.success) {
    this.rooms = result.data.rooms;
    this.statusStats = result.data.statusStats;
  }
}

// 查询报表
async fetchReportFromAPI(reportType, params) {
  const response = await fetch(`/htlpre/Reports/${reportType}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(params)
  });
  const result = await response.json();
  return result;
}
```

---

## 🚀 快速开始

### 1. 启动项目
```bash
cd D:\GUIMobile\WebCoreAPI\MGUIBAAPI\MGUIBAAPI
dotnet run
```

### 2. 访问 Swagger 文档
```
http://localhost:5000/swagger
```

### 3. 测试 API
```bash
# 获取营业日期
curl http://localhost:5000/htlpre/BkDate/Hotel

# 获取房间平面图
curl http://localhost:5000/htlpre/Rooms/Floorplan

# 查询报表
curl -X POST http://localhost:5000/htlpre/Reports/RoomStatus \
  -H "Content-Type: application/json" \
  -d '{"startDate":"2025-10-01","endDate":"2025-10-31"}'
```

---

## 📚 参考文档

- [ASP.NET Core Web API 官方文档](https://docs.microsoft.com/aspnet/core/web-api/)
- [Swagger/OpenAPI](https://swagger.io/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [前端项目 README](../../../../../GUINet/WebRWD/GUIVueBA/README.md)

---

## 👥 维护者

- **创建时间**: 2025-10-16
- **版本**: v1.0.0
- **状态**: 开发中 (框架已完成，待实现数据访问逻辑)

---

## 📞 联系方式

如有问题或建议，请联系开发团队。
