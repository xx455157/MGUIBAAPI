# 报表 API 实现指南

本文档基于 PATTERN 文件夹中的实现模式，提供报表相关 API 的开发规范和最佳实践。

---

## 目录

1. [架构概述](#架构概述)
2. [报表控制器实现模式](#报表控制器实现模式)
3. [标准 CRUD 控制器模式](#标准-crud-控制器模式)
4. [命名规范](#命名规范)
5. [代码结构规范](#代码结构规范)
6. [响应处理规范](#响应处理规范)
7. [示例代码](#示例代码)

---

## 架构概述

### 基础架构

- **基类**: `GUIAppAuthController` - 所有控制器必须继承此类
- **命名空间**: `MGUIBAAPI.Controllers.{模块名}`
- **路由规范**: 
  - 公开 API: `{模块名}/[controller]`
  - 私有报表: `{模块名}/private/[controller]`

### 文件组织

```
Controllers/
├── PATTERN/                    # 参考实现
│   ├── CustomersController.cs  # CRUD 示例
│   ├── EmployeesController.cs   # CRUD 示例
│   ├── ChartsController.cs      # 图表数据示例
│   └── Private/
│       └── vSCR01Controller.cs  # 报表示例
└── {模块名}/
    └── Private/
        └── v{报表代码}Controller.cs
```

---

## 报表控制器实现模式

### 1. 基础报表控制器（文件输出型）

适用于需要生成文件（PDF、Excel等）的报表。

#### 文件结构

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.GUI.Private;
using GUIStd.DAL.Base.Models.Reports;
using GUIStd.DAL.GUI.Models.Private.v{报表代码};

#endregion

namespace MGUIBAAPI.Controllers.Pattern.Private
{
    /// <summary>
    /// 【需經驗證】{模块名}{报表名称}控制器
    /// </summary>
    [Route("pattern/private/[controller]")]
    public class v{报表代码}Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private Blv{报表代码} BlMain => new Blv{报表代码}(ClientContent);

        #endregion

        #region " 共用屬性 "

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.NETGUI;

        #endregion

        #region " 共用函式 - 查詢資料 "

        // 可选的查询页面数据方法
        [HttpGet("page")]
        public Md{报表代码}_h GetUIData()
        {
            return BlMain.GetUIData();
        }

        #endregion

        #region " 共用函式 - 報表查詢 "

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<Md{报表代码}_q> obj)
        {
            // 建立報表
            var _info = await BlMain.GetReport(obj);
            
            // 回傳報表檔案
            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);
            
            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }

        #endregion
    }
}
```

#### 关键要点

1. **路由**: 使用 `[Route("pattern/private/[controller]")]` 或 `[Route("{模块名}/private/[controller]")]`
2. **SystemId**: 必须设置 `SystemId` 属性
3. **异步方法**: `GetReport` 使用 `async Task<IActionResult>`
4. **参数模型**: 使用 `MdReportQuery<T>` 包装查询条件
5. **响应处理**: 
   - 成功: 返回文件流 `SendFile`
   - 错误: 返回 `BadRequest` + `SendReportFailed`
   - 无数据: 返回 `BadRequest` + `SendReportNoQueryData`

---

### 2. 数据查询型报表控制器

适用于返回 JSON 数据的报表查询。

#### 文件结构

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.{模块名}.Private;
using GUIStd.DAL.{模块名}.Models.Private.v{报表代码};

#endregion

namespace MGUIBAAPI.Controllers.{模块名}
{
    /// <summary>
    /// {报表名称}控制器
    /// </summary>
    [Route("{模块名}/private/[controller]")]
    public class v{报表代码}Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private Bl{业务逻辑类} BlMain => new Bl{业务逻辑类}(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>頁面初始化資料模型</returns>
        [HttpGet("page")]
        public Md{报表代码}_h GetUIData()
        {
            return BlMain.GetUIData();
        }

        /// <summary>
        /// 查詢報表資料列表
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>報表資料集合</returns>
        [HttpPost("Q_GetList")]
        public IEnumerable<Md{报表代码}> Q_GetList([FromBody] Md{报表代码}_q queryParams)
        {
            return BlMain.Q_GetList(queryParams);
        }

        /// <summary>
        /// 匯出報表資料 (可選)
        /// </summary>
        /// <param name="queryParams">查詢條件</param>
        /// <returns>匯出資料集合</returns>
        [HttpPost("Q_GetList/export")]
        public IEnumerable<Md{报表代码}> Q_GetListExport([FromBody] Md{报表代码}_q queryParams)
        {
            return BlMain.Q_GetListExport(queryParams);
        }

        #endregion
    }
}
```

#### 关键要点

1. **查询方法命名**: 
   - `GetUIData()` - 获取页面初始化数据
   - `Q_GetList()` - 查询列表数据
   - `Q_GetListExport()` - 导出数据（可选）

2. **参数绑定**: 使用 `[FromBody]` 接收复杂对象

3. **返回类型**: 
   - 单条数据: `Md{报表代码}_h`
   - 列表数据: `IEnumerable<Md{报表代码}>`

---

## 标准 CRUD 控制器模式

### 文件结构

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.GUI;
using GUIStd.DAL.GUI.Models;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.Pattern
{
    /// <summary>
    /// 【需經驗證】{模块名}{实体名称}控制器
    /// </summary>
    [Route("pattern/[controller]")]
    public class {实体名称}Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private Bl{业务逻辑类} Bl{业务逻辑类} => new Bl{业务逻辑类}(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的{实体名称}資料
        /// </summary>
        /// <param name="idStart">{主键}(起)</param>
        /// <param name="idEnd">{主键}(迄)</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns>分頁{实体名称}資料模型物件</returns>
        [HttpPost("query/{idStart}/{idEnd}/pages/{pageNo}")]
        public Md{实体名称}_p GetData(string idStart, string idEnd,
            [DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0)
        {
            return Bl{业务逻辑类}.GetData(idStart, idEnd, ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 取得唯一的{实体名称}資料
        /// </summary>
        /// <param name="id">{主键}路徑參數</param>
        /// <returns>{实体名称}資料模型物件</returns>
        [HttpGet("{id}")]
        public Md{实体名称} GetRow(string id)
        {
            return Bl{业务逻辑类}.GetRow(id);
        }

        /// <summary>
        /// 判斷{主键}是否已存在
        /// </summary>
        /// <param name="id">{主键}路徑參數</param>
        /// <returns></returns>
        [HttpGet("exists/{id}")]
        public bool IsExist(string id)
        {
            return Bl{业务逻辑类}.IsExist(id);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">{实体名称}資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] Md{实体名称} obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = Bl{业务逻辑类}.ProcessInsert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="id">{主键}</param>
        /// <param name="obj">{实体名称}資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{id}")]
        public MdApiMessage Update(string id, [FromBody] Md{实体名称} obj)
        {
            // 檢查鍵值路徑參數與本文中的鍵值是否相同
            if (!id.EqualsIgnoreCase(obj.{主键属性}))
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            }

            try
            {
                // 呼叫商業元件執行修改作業
                int _result = Bl{业务逻辑类}.ProcessUpdate(id, obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id">{主键}</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{id}")]
        public MdApiMessage Delete(string id)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = Bl{业务逻辑类}.ProcessDelete(id);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
```

---

## 命名规范

### 控制器命名

- **报表控制器**: `v{报表代码}Controller`
  - 示例: `vSCR01Controller`, `vHTRGM09Controller`
- **CRUD 控制器**: `{实体名称}Controller`
  - 示例: `CustomersController`, `EmployeesController`

### 方法命名

- **查询方法**:
  - `GetData()` - 分页查询
  - `GetRow()` - 单笔查询
  - `GetUIData()` - 页面初始化数据
  - `Q_GetList()` - 报表列表查询
  - `IsExist()` - 存在性检查

- **异动方法**:
  - `Insert()` - 新增
  - `Update()` - 修改
  - `Delete()` - 删除

- **报表方法**:
  - `GetReport()` - 生成报表文件

### 模型命名

- **查询条件模型**: `Md{报表代码}_q`
- **页面数据模型**: `Md{报表代码}_h`
- **数据模型**: `Md{报表代码}`
- **分页模型**: `Md{实体名称}_p`
- **报表查询包装**: `MdReportQuery<T>`

### 业务逻辑类命名

- **报表业务逻辑**: `Blv{报表代码}`
- **CRUD 业务逻辑**: `Bl{实体代码}` (如 `BlT01`, `BlT02`)

---

## 代码结构规范

### 1. 区域划分

```csharp
#region " 匯入的名稱空間：Framework "
// Framework 命名空间
#endregion

#region " 匯入的名稱空間：GoldenUp "
// GoldenUp 命名空间
#endregion

namespace MGUIBAAPI.Controllers.{模块名}
{
    public class {控制器名} : GUIAppAuthController
    {
        #region " 私用屬性 "
        // 私有属性
        #endregion

        #region " 共用屬性 "
        // 公共属性
        #endregion

        #region " 共用函式 - 查詢資料 "
        // 查询方法
        #endregion

        #region " 共用函式 - 異動資料 "
        // 增删改方法
        #endregion

        #region " 共用函式 - 報表查詢 "
        // 报表方法
        #endregion
    }
}
```

### 2. HTTP 方法使用

- `[HttpGet]` - 查询操作
- `[HttpPost]` - 新增、复杂查询、报表生成
- `[HttpPut]` - 修改操作
- `[HttpDelete]` - 删除操作

### 3. 路由设计

- **查询路由**: `GET /{模块名}/private/{controller}/{id}`
- **分页查询**: `POST /{模块名}/private/{controller}/query/{idStart}/{idEnd}/pages/{pageNo}`
- **报表生成**: `POST /{模块名}/private/{controller}/report`
- **列表查询**: `POST /{模块名}/private/{controller}/Q_GetList`

### 4. 参数验证

使用 `[DARange]` 属性验证参数范围：

```csharp
[DARange(1, int.MaxValue)] int pageNo
```

---

## 响应处理规范

### 1. 成功响应

#### CRUD 操作

```csharp
// 新增成功
return HttpContext.Response.InsertSuccess(_result);

// 修改成功
return HttpContext.Response.UpdateSuccess(_result);

// 刪除成功
return HttpContext.Response.DeleteSuccess(_result);
```

#### 报表生成

```csharp
// 返回文件流
return HttpContext.Response.SendFile(_info.Contents, _info.FileName);
```

### 2. 错误响应

```csharp
// 新增失败
return HttpContext.Response.InsertFailed(ex);

// 修改失败
return HttpContext.Response.UpdateFailed(ex);

// 删除失败
return HttpContext.Response.DeleteFailed(ex);

// 键值不一致
return HttpContext.Response.UpdateFailedWhenKeyNotSame();

// 报表生成失败
return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

// 无查询数据
return BadRequest(HttpContext.Response.SendReportNoQueryData());
```

### 3. 异常处理

所有异动操作必须使用 try-catch：

```csharp
try
{
    // 业务逻辑
    return HttpContext.Response.Success(...);
}
catch (Exception ex)
{
    return HttpContext.Response.Failed(ex);
}
```

---

## 示例代码

### 示例 1: 完整报表控制器（文件输出）

参考: `PATTERN/Private/vSCR01Controller.cs`

```csharp
[Route("pattern/private/[controller]")]
public class vSCR01Controller : GUIAppAuthController
{
    private BlvSCR01 BlMain => new BlvSCR01(ClientContent);
    
    public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.NETGUI;

    [HttpPost("report")]
    public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdSCR01_q> obj)
    {
        var _info = await BlMain.GetReport(obj);
        
        if (_info.Contents != null)
            return HttpContext.Response.SendFile(_info.Contents, _info.FileName);
        
        if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
            return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

        return BadRequest(HttpContext.Response.SendReportNoQueryData());
    }
}
```

### 示例 2: 数据查询型报表控制器

参考: `AS/Private/vASR01Controller.cs`

```csharp
[Route("as/private/[controller]")]
public class vASR01Controller : GUIAppAuthController
{
    private BlDepreciation BlDepreciation => new BlDepreciation(ClientContent);

    [HttpGet("page")]
    public MdASR01_h GetUIData()
    {
        return BlDepreciation.GetUIData();
    }

    [HttpPost("Q_GetList")]
    public IEnumerable<MdASR01> Q_GetList([FromBody] MdASR01_q queryParams)
    {
        return BlDepreciation.Q_GetList(queryParams);
    }
}
```

### 示例 3: 标准 CRUD 控制器

参考: `PATTERN/CustomersController.cs`

```csharp
[Route("pattern/[controller]")]
public class CustomersController : GUIAppAuthController
{
    private BlT01 BlT01 => new BlT01(ClientContent);

    [HttpPost("query/{customerIds}/{customerIde}/pages/{pageNo}")]
    public MdPTNCustomers_p GetData(string customerIds, string customerIde,
        [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] countries, int rowsPerPage = 0)
    {
        return BlT01.GetData(customerIds, customerIde, ..., ControlName, pageNo, ref rowsPerPage);
    }

    [HttpGet("{customerId}")]
    public MdPTNCustomer GetRow(string customerId)
    {
        return BlT01.GetRow(customerId);
    }

    [HttpPost]
    public MdApiMessage Insert([FromBody] MdPTNCustomer obj)
    {
        try
        {
            int _result = BlT01.ProcessInsert(obj);
            return HttpContext.Response.InsertSuccess(_result);
        }
        catch (Exception ex)
        {
            return HttpContext.Response.InsertFailed(ex);
        }
    }
}
```

---

## 注意事项

1. **认证**: 所有控制器继承 `GUIAppAuthController`，自动处理认证
2. **路由**: 报表控制器使用 `private` 子路径
3. **异步**: 报表生成方法使用 `async Task<IActionResult>`
4. **异常处理**: 所有异动操作必须包含异常处理
5. **参数验证**: 使用 `[DARange]` 等属性验证参数
6. **命名空间**: 严格按照模块划分命名空间
7. **注释**: 所有公共方法必须包含 XML 注释

---

## 相关文件

- `PATTERN/CustomersController.cs` - CRUD 标准实现
- `PATTERN/EmployeesController.cs` - CRUD 标准实现
- `PATTERN/Private/vSCR01Controller.cs` - 报表标准实现
- `AS/Private/vASR01Controller.cs` - 数据查询型报表实现

---

**最后更新**: 2024年
**维护者**: 开发团队

