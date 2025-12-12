@echo off
REM ========================================
REM 酒店管理系統 (MGUI) - 後端 API 啟動腳本
REM 功能：啟動 ASP.NET Core API 服務
REM ========================================

echo ====================================
echo MGUI Backend API Service
echo Starting ASP.NET Core API...
echo ====================================
echo.

cd /d "%~dp0"

REM 顯示啟動資訊
echo [INFO] Current Directory: %CD%
echo [INFO] Environment: Development
echo [INFO] Service Port: http://localhost:23232
echo [INFO] Swagger Doc: http://localhost:23232/swagger
echo.

REM 啟動 API 服務
echo Starting IIS Express...
dotnet run --launch-profile "IIS Express"

pause
