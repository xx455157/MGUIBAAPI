# RMRT - 房型資料表

## 基本資訊
- **系統名稱**：[[HT2000]] 
- **資料表代碼**：RMRT
- **資料表名稱**：房型資料表
- **主鍵**：RMRT01 (房型代碼)
- **版本**：1.0
- **最後更新**：2024/11/18

## 資料表說明
本資料表用於記錄飯店房型相關資訊，包含房型代碼、說明、容量、價格等資訊。

## 欄位定義

| 欄位代號 | 欄位名稱 | 資料型態 | 長度 | 備註 |
|---------|---------|---------|------|------|
| RMRT001 | 建檔日期 | NVARCHAR | 8 | (YYYYMMDD) |
| RMRT002 | 建檔時間 | NVARCHAR | 9 | (HHMMSS) |
| RMRT003 | 建檔工作站 | NVARCHAR | 30 | |
| RMRT004 | 建檔人 | NVARCHAR | 30 | |
| RMRT005 | 更改日期 | NVARCHAR | 8 | (YYYYMMDD) |
| RMRT006 | 更改時間 | NVARCHAR | 9 | (HHMMSS) |
| RMRT007 | 更改工作站 | NVARCHAR | 30 | |
| RMRT008 | 更改人 | NVARCHAR | 30 | |
| RMRT01 | 房型代碼 | NVARCHAR | 6 | 主鍵 |
| RMRT02 | 房型說明 | NVARCHAR | 50 | |
| RMRT03 | 旅館容量 | NUMERIC | 25,4 | |
| RMRT04 | 人數容量 | NUMERIC | 25,4 | |
| RMRT05 | 房型停用 | NUMERIC | 25,4 | |
| RMRT06 | 最低價格 | NUMERIC | 25,4 | |
| RMRT07 | 最高價格 | NUMERIC | 25,4 | |
| RMRT08 | 訊息唯一碼 | NVARCHAR | 36 | UID |
| RMRT09 | FILLER | NVARCHAR | 1 | |
| RMRT10 | FILLER | NVARCHAR | 1 | |
| RMRT11 | FILLER | NVARCHAR | 1 | |
| RMRT12 | 床型 | NVARCHAR | 8 | |
| RMRT13 | FILLER | NVARCHAR | 8 | |
| RMRT14 | FILLER | NVARCHAR | 8 | |
| RMRT15 | FILLER | NVARCHAR | 10 | |
| RMRT16 | FILLER | NVARCHAR | 10 | |
| RMRT17 | FILLER | NVARCHAR | 10 | |
| RMRT18 | FILLER | NVARCHAR | 50 | |
| RMRT19 | FILLER | NVARCHAR | 50 | |
| RMRT20 | FILLER | NVARCHAR | 50 | |
| RMRT21 | FILLER | NUMERIC | 25,4 | |
| RMRT22 | FILLER | NUMERIC | 25,4 | |
| RMRT23 | FILLER | NUMERIC | 25,4 | |
| RMRT24 | FILLER | NVARCHAR | MAX | |
| RMRT25 | FILLER | NVARCHAR | MAX | |
| RMRT26 | FILLER | NVARCHAR | MAX | |

## 索引定義
- 主鍵索引：RMRT01
- 複合索引：
  - RMRTA1：RMRT08

## 資料關聯說明
無外部關聯

## 變更歷史記錄

| 日期 | 版本 | 變更內容 | 修改人 |
|------|------|----------|--------|
| 2024/11/18 | 1.0 | 建立Table | Mike | 