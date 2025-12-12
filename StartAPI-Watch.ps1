# PowerShell 啟動腳本 - 後端 API 服務 (開發模式 - 自動重啟)
# 酒店管理系統 (MGUI)
# 使用 dotnet watch 監控文件變化，自動重新編譯並重啟服務

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "[MGUI] 開發模式 - 自動重啟" -ForegroundColor Magenta
Write-Host "[MGUI] 正在啟動後端 API 服務..." -ForegroundColor Green
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

Set-Location -Path $PSScriptRoot

# 顯示啟動資訊
Write-Host "[資訊] 當前目錄: $(Get-Location)" -ForegroundColor Yellow
Write-Host "[資訊] 環境模式: Development (Watch Mode)" -ForegroundColor Yellow
Write-Host "[資訊] 服務端口: http://localhost:23232" -ForegroundColor Yellow
Write-Host "[資訊] Swagger: http://localhost:23232/swagger" -ForegroundColor Yellow
Write-Host ""
Write-Host "[提示] 檔案變更時會自動重新編譯並重啟服務" -ForegroundColor Green
Write-Host "[提示] 按 Ctrl+C 停止服務" -ForegroundColor Green
Write-Host ""

# 設定環境變數
$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ASPNETCORE_URLS = "http://localhost:23232"

# 使用 dotnet watch 啟動服務
Write-Host "正在啟動監控模式..." -ForegroundColor Green
Write-Host ""
dotnet watch run --urls=http://localhost:23232

Read-Host "按 Enter 鍵退出"








