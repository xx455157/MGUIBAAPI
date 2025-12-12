# PowerShell 啟動腳本 - 後端 API 服務
# 酒店管理系統 (MGUI)

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "[MGUI] 正在啟動後端 API 服務..." -ForegroundColor Green
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

Set-Location -Path $PSScriptRoot

# 顯示啟動資訊
Write-Host "[資訊] 當前目錄: $(Get-Location)" -ForegroundColor Yellow
Write-Host "[資訊] 環境模式: Development" -ForegroundColor Yellow
Write-Host "[資訊] 服務端口: http://localhost:23232" -ForegroundColor Yellow
Write-Host "[資訊] Swagger: http://localhost:23232/swagger" -ForegroundColor Yellow
Write-Host ""

# 啟動服務
Write-Host "正在啟動服務..." -ForegroundColor Green
dotnet run --launch-profile "IIS Express"

Read-Host "按 Enter 鍵退出"
