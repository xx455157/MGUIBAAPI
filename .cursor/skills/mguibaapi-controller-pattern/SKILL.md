# MGUIBAAPI Controller Pattern Skill

本 Skill 用來指導在 `MGUIBAAPI` 專案中新增/維護 API Controller 時，遵循既有 `Controllers/PATTERN` 的標準寫法（路由、查詢、CRUD、報表輸出、統一回應格式）。

## Source of truth（本 Skill 的來源）
以下檔案是本 Skill 規範與模板的直接依據（有疑義以這些檔案為準）：

- `Controllers/PATTERN/EmployeesController.cs`
- `Controllers/PATTERN/CustomersController.cs`
- `Controllers/PATTERN/AuthProgramsController.cs`
- `Controllers/PATTERN/Private/vQPatternController.cs`
- `Controllers/PATTERN/Private/vSCR01Controller.cs`

## When to use
- 你要新增一支新 API controller（特別是 Query / Help / CRUD）。
- 你要把既有 controller 風格統一成與 `PATTERN` 一致。
- 你要做報表檔案下載（回傳檔案 stream）。
- 你要做私用頁面（private）「初次載入輔助資料」API。

## Core principles（核心原則）
1. Controller 保持薄層：只做參數接收、最小 guard；主要邏輯交給 BLL。
2. 需要驗證的 controller 一律繼承 `GUIAppAuthController`。
3. 回應格式統一：CRUD 用 `HttpContext.Response.*Success/*Failed` 等 extension 回傳 `MdApiMessage`。
4. 分頁參數一定要做範圍驗證：`[DARange(1, int.MaxValue)]`。

## Naming / Namespace / Base class
- Namespace（依 PATTERN）：
  - public：`namespace MGUIBAAPI.Controllers.Pattern`
  - private：`namespace MGUIBAAPI.Controllers.Pattern.Private`
- Base class：`GUIAppAuthController`
- BLL 建構慣例（依 PATTERN）：
  - `private BlXXXX BlMain => new BlXXXX(ClientContent);`

常用共通屬性（由 base controller 提供）：
- `ClientContent`：當前請求/登入上下文，提供給 BLL。
- `ControlName`：控制器/功能代號，常用於 helpv2 的 `FuncName`。

## Routing conventions（路由規範）
### Public pattern
- Controller 路由：`[Route("pattern/[controller]")]`

常見 action：
- `GET help`
- `GET helpv2/pages/{pageNo}`（query string：`queryText`、`sortByName`）
- `POST query/{idStart}/{idEnd}/pages/{pageNo}`（body：多選字串陣列）
- `GET {id}`
- `GET exists/{id}`
- `POST`（insert）
- `PUT {id}`（update）
- `DELETE {id}`（delete）

### Private pattern
- Controller 路由：`[Route("pattern/private/[controller]")]`

常見 action：
- `GET paged?isStateAdd={bool}`（明細頁初始化輔助資料）
- `POST report`（產生報表並回傳檔案）

## Query / Paging conventions
### helpv2 paging（標準）
- action route：`helpv2/pages/{pageNo}`
- `pageNo` 必須加上：`[DARange(1, int.MaxValue)]`
- `queryText`、`sortByName` 使用 `[FromQuery]`
- 呼叫 BLL 時將參數包成 `MdHelpPaging`，其中：
  - `FuncName = this.ControlName`

參考：`EmployeesController.GetSHelpv2`、`AuthProgramsController.GetSHelpv2`

### query paging（標準）
- action route：`query/{idStart}/{idEnd}/pages/{pageNo}`
- 多選條件使用 `[FromBody] string[] xxx`
- `pageNo` 必須加上 `DARange`

參考：`EmployeesController.GetData`、`CustomersController.GetData`

## CRUD response conventions
CRUD 統一回傳 `MdApiMessage`，並使用 `HttpContext.Response.*` extension：
- Insert：`InsertSuccess` / `InsertFailed`
- Update：`UpdateSuccess` / `UpdateFailed`
- Delete：`DeleteSuccess` / `DeleteFailed`

Update 額外規範：
- 如果路徑 key 與 body key 不一致，應回傳 `UpdateFailedWhenKeyNotSame()`

參考：`CustomersController.Update`

## Report download conventions
報表產生與下載（private controller 常見）：
- action route：`POST report`
- BLL 回傳內容若有 `Contents`，使用 `HttpContext.Response.SendFile(contents, fileName)` 回檔案
- 若 `ErrorMessage` 有值，回 `BadRequest(HttpContext.Response.SendReportFailed(errorMessage))`
- 若查無資料，回 `BadRequest(HttpContext.Response.SendReportNoQueryData())`

參考：`vSCR01Controller.GetReport`

此外：必要時覆寫 `SystemId`，例如：
- `public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.NETGUI;`

## Do / Don’t checklist
### Do
- Controller 只做：接參數、簡單檢查（例如 key 一致性）、呼叫 BLL、包裝回應。
- `pageNo` 一律加 `DARange`。
- helpv2 一律包成 `MdHelpPaging` 並帶 `FuncName = ControlName`。

### Don’t
- 不要在 Controller 直接寫 SQL / SP 呼叫。
- 不要在 Controller 自行拼接回應 JSON（使用 Response extensions）。
- 不要忽略 key 一致性檢查（update 時）。

---

# Copy/Paste Templates（可複製模板）
以下模板以 `Controllers/PATTERN` 的寫法為準，複製後只需要替換：Controller 名稱、BLL 類別、Model 類別、參數名稱。

## Template 0: Controller skeleton（public）

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.DAL.Base.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.Pattern
{
    /// <summary>
    /// 【需經驗證】PTNXXX控制器
    /// </summary>
    [Route("pattern/[controller]")]
    public class XxxController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlXxx BlMain => new BlXxx(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        #endregion

        #region " 共用函式 - 異動資料 "

        #endregion
    }
}
```

## Template 1: helpv2/pages/{pageNo}

```csharp
/// <summary>
/// 取得分頁頁次的輔助資料
/// </summary>
/// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
/// <param name="pageNo">查詢頁次</param>
/// <param name="sortByName">是否依名稱排序</param>
/// <returns>分頁輔助資料模型物件</returns>
[HttpGet("helpv2/pages/{pageNo}")]
public MdCode_p GetSHelpv2(
    [DARange(1, int.MaxValue)] int pageNo,
    [FromQuery] string queryText,
    [FromQuery] bool sortByName)
{
    return BlMain.GetSHelpv2(new MdHelpPaging
    {
        QueryText = queryText,
        FuncName = this.ControlName,
        SortByName = sortByName,
        PageNo = pageNo
    });
}
```

## Template 2: query/{idStart}/{idEnd}/pages/{pageNo} + body string[]

```csharp
/// <summary>
/// 取得分頁頁次的資料
/// </summary>
[HttpPost("query/{idStart}/{idEnd}/pages/{pageNo}")]
public MdXxx_p GetData(
    string idStart,
    string idEnd,
    string queryText,
    [DARange(1, int.MaxValue)] int pageNo,
    [FromBody] string[] multiSelectItems)
{
    return BlMain.GetData(idStart, idEnd, queryText, multiSelectItems, ControlName, pageNo);
}
```

## Template 3: CRUD (POST/PUT/DELETE) with standard response

```csharp
[HttpPost]
public MdApiMessage Insert([FromBody] MdXxx obj)
{
    try
    {
        int result = BlMain.ProcessInsert(obj);
        return HttpContext.Response.InsertSuccess(result);
    }
    catch (Exception ex)
    {
        return HttpContext.Response.InsertFailed(ex);
    }
}

[HttpPut("{id}")]
public MdApiMessage Update(string id, [FromBody] MdXxx obj)
{
    // 可選：若你的 obj 有鍵值欄位，建議做 key 一致性檢查
    // if (!id.EqualsIgnoreCase(obj.IdField))
    //     return HttpContext.Response.UpdateFailedWhenKeyNotSame();

    try
    {
        int result = BlMain.ProcessUpdate(id, obj);
        return HttpContext.Response.UpdateSuccess(result);
    }
    catch (Exception ex)
    {
        return HttpContext.Response.UpdateFailed(ex);
    }
}

[HttpDelete("{id}")]
public MdApiMessage Delete(string id)
{
    try
    {
        int result = BlMain.ProcessDelete(id);
        return HttpContext.Response.DeleteSuccess(result);
    }
    catch (Exception ex)
    {
        return HttpContext.Response.DeleteFailed(ex);
    }
}
```

## Template 4: Private controller skeleton + GET paged

```csharp
#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;

#endregion

namespace MGUIBAAPI.Controllers.Pattern.Private
{
    /// <summary>
    /// 【需經驗證】PTN私用頁面控制器
    /// </summary>
    [Route("pattern/private/[controller]")]
    public class vXxxController : GUIAppAuthController
    {
        private BlvXxx BlMain => new BlvXxx(ClientContent);

        /// <summary>
        /// 取得明細頁面首次載入所需的所有輔助資料
        /// </summary>
        /// <param name="isStateAdd">是否為新增作業狀態</param>
        [HttpGet("paged")]
        public MdXxxd_h GetUIDataForDetail([FromQuery] bool isStateAdd) =>
            BlMain.GetUIDataForDetail(isStateAdd);
    }
}
```

## Template 5: Report download (POST report)

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;

[Route("pattern/private/[controller]")]
public class vXxxReportController : GUIAppAuthController
{
    private BlvXxxReport BlMain => new BlvXxxReport(ClientContent);

    // 필요時可覆寫報表系統代號
    // public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.NETGUI;

    [HttpPost("report")]
    public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdXxx_q> obj)
    {
        var info = await BlMain.GetReport(obj);

        if (info.Contents != null)
            return HttpContext.Response.SendFile(info.Contents, info.FileName);

        if (!string.IsNullOrWhiteSpace(info.ErrorMessage))
            return BadRequest(HttpContext.Response.SendReportFailed(info.ErrorMessage));

        return BadRequest(HttpContext.Response.SendReportNoQueryData());
    }
}
```
