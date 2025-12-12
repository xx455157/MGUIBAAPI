# MGUIBAAPI 打包腳本
# 用途：將 API 打包成可部署的版本

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  MGUIBAAPI 打包工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 設定輸出目錄
$publishPath = "D:\GUIMobile\WebCoreAPI\MGUIBAAPI\MGUIBAAPI\bin\Release\Publish"
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$outputPath = "$publishPath\$timestamp"

Write-Host "📦 開始打包 API..." -ForegroundColor Yellow
Write-Host "   輸出目錄: $outputPath" -ForegroundColor Gray
Write-Host ""

# 清理舊的發布目錄
if (Test-Path $outputPath) {
    Write-Host "🗑️  清理舊的發布目錄..." -ForegroundColor Yellow
    Remove-Item -Path $outputPath -Recurse -Force
}

# 執行發布
Write-Host "🚀 執行 dotnet publish..." -ForegroundColor Yellow
dotnet publish `
    --configuration Release `
    --output $outputPath `
    --self-contained false `
    --runtime win-x64

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ 打包成功！" -ForegroundColor Green
    Write-Host ""
    Write-Host "📁 輸出位置: $outputPath" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "📋 打包內容包含:" -ForegroundColor Yellow
    Write-Host "   - MGUIBAAPI.dll (主程式)" -ForegroundColor Gray
    Write-Host "   - MGUIBAAPI.Views.dll (視圖)" -ForegroundColor Gray
    Write-Host "   - 所有依賴的 DLL 檔案" -ForegroundColor Gray
    Write-Host "   - appsettings.json (配置檔案)" -ForegroundColor Gray
    Write-Host "   - Content 目錄 (檔案和日誌)" -ForegroundColor Gray
    Write-Host "   - wwwroot 目錄 (靜態資源)" -ForegroundColor Gray
    Write-Host ""
    Write-Host "💡 部署說明:" -ForegroundColor Yellow
    Write-Host "   1. 將整個目錄複製到目標伺服器" -ForegroundColor Gray
    Write-Host "   2. 確保目標伺服器已安裝 .NET Core 2.2 Runtime" -ForegroundColor Gray
    Write-Host "   3. 修改 appsettings.json 中的資料庫連線字串" -ForegroundColor Gray
    Write-Host "   4. 執行: dotnet MGUIBAAPI.dll" -ForegroundColor Gray
    Write-Host ""
    
    # 詢問是否要打開輸出目錄
    $openFolder = Read-Host "是否要打開輸出目錄? (Y/N)"
    if ($openFolder -eq "Y" -or $openFolder -eq "y") {
        explorer $outputPath
    }
} else {
    Write-Host ""
    Write-Host "❌ 打包失敗！請檢查錯誤訊息。" -ForegroundColor Red
    exit 1
}


