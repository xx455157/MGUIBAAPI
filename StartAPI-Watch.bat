@echo off
REM ========================================
REM 酒店管理系統 (MGUI) - 後端 API 啟動腳本
REM 功能：啟動 ASP.NET Core API 服務 (開發模式 - 自動重啟)
REM ========================================

echo ====================================
echo MGUI Backend API Service
echo Development Mode - Auto Restart
echo ====================================
echo.

cd /d "%~dp0"

REM 顯示啟動資訊
echo [INFO] Current Directory: %CD%
echo [INFO] Environment: Development (Watch Mode)
echo [INFO] Service Port: http://localhost:23232
echo [INFO] Swagger Doc: http://localhost:23232/swagger
echo.
echo [TIP] Service will auto-restart when files change
echo [TIP] Press Ctrl+C to stop
echo.

REM 啟動 API 服務 (Watch Mode)
echo Starting API Service with Watch Mode...
dotnet watch run --urls=http://localhost:23232

pause








