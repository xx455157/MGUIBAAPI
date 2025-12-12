# MGUI Backend API - 啟動說明

## 📋 啟動方式

### 1️⃣ 一般模式（手動重啟）

適用於：生產環境或不需要頻繁修改代碼時

**Windows 批處理:**

```batch
StartAPI.bat
```

**PowerShell:**

```powershell
.\StartAPI.ps1
```

---

### 2️⃣ 開發模式（自動重啟）⭐ **推薦開發時使用**

適用於：開發過程中，修改代碼後自動重啟服務

**Windows 批處理:**

```batch
StartAPI-Watch.bat
```

**PowerShell:**

```powershell
.\StartAPI-Watch.ps1
```

**特點：**

- ✅ 監控源代碼文件變化（.cs 文件）
- ✅ 自動重新編譯
- ✅ 自動重啟服務
- ✅ 無需手動停止和啟動
- ✅ 提高開發效率

---

## 🔄 自動重啟工作原理

當您使用 `StartAPI-Watch` 啟動服務後：

1. **監控文件**: `dotnet watch` 會監控項目中的所有源代碼文件
2. **檢測變化**: 當您保存修改的 `.cs` 文件時
3. **自動編譯**: 系統自動重新編譯項目
4. **自動重啟**: 編譯成功後自動重啟 API 服務
5. **即時測試**: 您可以立即測試新的修改

**監控的文件類型:**

- `*.cs` - C# 源代碼文件
- `*.cshtml` - Razor 視圖文件
- `*.json` - 配置文件（如 appsettings.json）
- `*.csproj` - 項目文件

---

## 🖥️ 服務資訊

**服務地址:** http://localhost:23232

**Swagger 文檔:** http://localhost:23232/swagger

**環境模式:** Development

**目標框架:** .NET Core 2.2

---

## ⚠️ 注意事項

### 停止服務

- 按 `Ctrl + C` 停止服務
- 或直接關閉 PowerShell 窗口

### Watch 模式限制

- 僅在開發環境使用
- 首次啟動時間較長（需要編譯）
- 每次文件變化都會觸發重新編譯

### 最佳實踐

1. **開發時**: 使用 `StartAPI-Watch.ps1` - 自動重啟，提高效率
2. **測試時**: 使用 `StartAPI.ps1` - 穩定運行，不會意外重啟
3. **生產環境**: 使用正式的部署方式（IIS 或 Docker）

---

## 🚀 快速開始

### 第一次使用（開發模式）

1. 開啟 PowerShell 或命令提示字元
2. 切換到項目目錄：
   ```powershell
   cd d:\GUIMobile\WebCoreAPI\MGUIBAAPI\MGUIBAAPI
   ```
3. 運行開發模式啟動腳本：
   ```powershell
   .\StartAPI-Watch.ps1
   ```
4. 等待編譯和啟動完成
5. 打開瀏覽器訪問: http://localhost:23232/swagger
6. 開始開發！修改代碼並保存，服務會自動重啟

---

## 📝 開發流程示例

```
1. 運行 StartAPI-Watch.ps1
   └─> 服務啟動在 http://localhost:23232

2. 修改 Controller 或 Model 文件
   └─> 保存文件 (Ctrl+S)

3. dotnet watch 檢測到變化
   └─> 自動重新編譯項目
   └─> 編譯成功
   └─> 自動重啟服務

4. 刷新瀏覽器測試新功能
   └─> 新的修改已生效 ✅

5. 繼續開發...
```

---

## 🛠️ 常見問題

**Q: Watch 模式下修改文件沒有自動重啟？**
A: 確認文件已保存，且修改的是源代碼文件（.cs）

**Q: 編譯失敗怎麼辦？**
A: Watch 模式會顯示編譯錯誤，修復錯誤並保存後會自動重試

**Q: 如何查看詳細的重啟日誌？**
A: 控制台會顯示所有編譯和重啟信息

**Q: 可以同時運行多個實例嗎？**
A: 不可以，端口 23232 同時只能被一個服務佔用

---

## 📚 相關資源

- [dotnet watch 官方文檔](https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch)
- [ASP.NET Core 開發工具](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/tools)
- [Swagger UI 使用指南](https://swagger.io/tools/swagger-ui/)

---

**創建日期:** 2025-10-23  
**版本:** 1.0  
**維護者:** GoldenUp Information







