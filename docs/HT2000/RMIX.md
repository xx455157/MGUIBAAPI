# RMIX - HOTEL2000 檔案說明文件

## 基本資訊
- **系統名稱**：[[HT2000]] 
- **資料表代碼**：RMIX
- **資料表名稱**：DunQian RMS Item Price File
- **版本**：1.0
- **建立日期**：2024/11/18
- **建立人員**：Mike

## 欄位定義

| 欄位代號 | 欄位名稱 | 欄位說明 | 資料型態 | 長度 | 備註 |
|---------|---------|---------|---------|------|------|
| RMIX001 | RDDATE | 建檔日期 | NVARCHAR | 8 | YYYYMMDD |
| RMIX002 | RDTIME | 建檔時間 | NVARCHAR | 9 | HH:MM:SS |
| RMIX003 | RDTERM | 建檔工作站 | NVARCHAR | 3 | 0 |
| RMIX004 | RDCLRK | 建檔人 | NVARCHAR | 3 | 0 |
| RMIX005 | MDDATE | 更改日期 | NVARCHAR | 8 | YYYYMMDD |
| RMIX006 | MDTIME | 更改時間 | NVARCHAR | 9 | HH:MM:SS |
| RMIX007 | MDTERM | 更改工作站 | NVARCHAR | 3 | 0 |
| RMIX008 | MDCLRK | 更改人 | NVARCHAR | 3 | 0 |
| RMIX01 | RVNO | 訂單號碼 | NVARCHAR | 5 | P1 |
| RMIX02 | ROOMTYPE | 房型代碼 | NVARCHAR | 6 | P2 |
| RMIX03 | SRNO | 訂房序號 | NVARCHAR | 10 | P3 |
| RMIX04 | ITEMNO | 項次 | NUMERIC | 25,4 | P4 |
| RMIX05 | DATE | 日期 | NVARCHAR | 8 | YYYYMMDD |
| RMIX06 | ITEMCODE | 項目代碼 | NUMERIC | 25,4 | |
| RMIX07 | UNITPRICE | 單價 | NUMERIC | 25,4 | |
| RMIX08 | QUANTITY | 數量 | NUMERIC | 25,4 | |
| RMIX09 | FILLER | FILLER | NVARCHAR | 1 | |
| RMIX10 | FILLER | FILLER | NVARCHAR | 1 | |
| RMIX11 | FILLER | FILLER | NVARCHAR | 1 | |
| RMIX12 | FILLER | FILLER | NVARCHAR | 8 | |
| RMIX13 | FILLER | FILLER | NVARCHAR | 8 | |
| RMIX14 | FILLER | FILLER | NVARCHAR | 8 | |
| RMIX15 | FILLER | FILLER | NVARCHAR | 10 | |
| RMIX16 | FILLER | FILLER | NVARCHAR | 10 | |
| RMIX17 | FILLER | FILLER | NVARCHAR | 10 | |
| RMIX18 | FILLER | FILLER | NVARCHAR | 50 | |
| RMIX19 | FILLER | FILLER | NVARCHAR | 50 | |
| RMIX20 | FILLER | FILLER | NVARCHAR | 50 | |
| RMIX21 | FILLER | FILLER | NUMERIC | 25,4 | |
| RMIX22 | FILLER | FILLER | NUMERIC | 25,4 | |
| RMIX23 | FILLER | FILLER | NUMERIC | 25,4 | |
| RMIX24 | FILLER | FILLER | NVARCHAR | MAX | |
| RMIX25 | FILLER | FILLER | NVARCHAR | MAX | |
| RMIX26 | FILLER | FILLER | NVARCHAR | MAX | |

## 索引說明
- 主鍵：RMIX01、RMIX02、RMIX03、RMIX04

## 變更歷史記錄
| 版本 | 日期 | 修改人員 | 修改內容 |
|------|------|----------|----------|
| 1.0 | 2024/11/18 | Mike | 初始建立 |


