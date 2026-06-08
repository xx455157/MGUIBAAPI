# DS_資料同步指示檔

## 資料表資訊

**系統：** [ARTHGUI](../ARTHGUI.md) (金旭共用系統)  
**資料表代碼：** DS  
**中文名稱：** 資料同步指示檔  
**主鍵：** DS01  
**最後更新日期：** 2024-06-05  
**版本：** 1.0

## 資料表說明

本資料表為金旭共用系統資料同步指示檔，用於記錄系統間資料同步的指示與狀態。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱 | 資料型態 | 長度 | 屬性 | 備註說明 |
|----------|----------|----------|------|------|----------|
| DS01 | 異動指示識別碼 | uniqueidentifier | - | P | 主鍵 |
| DS02 | 館別、公司別、店別 | Nvarchar | 10 | M | - |
| DS03 | 功能名稱 | Nvarchar | 64 | M | - |
| DS04 | 建立日期 | Nvarchar | 8 | M | 格式(YYYYMMDD) |
| DS05 | 建立時間 | Nvarchar | 9 | M | - |
| DS06 | 預計截止日期 | Nvarchar | 8 | - | 格式(YYYYMMDD) |
| DS07 | 預計截止時間 | Nvarchar | 9 | - | - |
| DS08 | 指示別 | Nvarchar | 10 | M | Put/Remove/Insert |
| DS0901 | PK1 | Nvarchar | 64 | - | 主鍵值1 |
| DS0902 | PK2 | Nvarchar | 64 | - | 主鍵值2 |
| DS0903 | PK3 | Nvarchar | 64 | - | 主鍵值3 |
| DS0904 | PK4 | Nvarchar | 64 | - | 主鍵值4 |
| DS0905 | PK5 | Nvarchar | 64 | - | 主鍵值5 |
| DS0906 | PK6 | Nvarchar | 64 | - | 主鍵值6 |
| DS0907 | PK7 | Nvarchar | 64 | - | 主鍵值7 |
| DS0908 | PK8 | Nvarchar | 64 | - | 主鍵值8 |
| DS0909 | PK9 | Nvarchar | 64 | - | 主鍵值9 |
| DS0910 | PK10 | Nvarchar | 64 | - | 主鍵值10 |
| DS0911 | PK11 | Nvarchar | 64 | - | 主鍵值11 |
| DS0912 | PK12 | Nvarchar | 64 | - | 主鍵值12 |
| DS0913 | PK13 | Nvarchar | 64 | - | 主鍵值13 |
| DS0914 | PK14 | Nvarchar | 64 | - | 主鍵值14 |
| DS0915 | PK15 | Nvarchar | 64 | - | 主鍵值15 |
| DS0916 | PK16 | Nvarchar | 64 | - | 主鍵值16 |
| DS10 | Fail Message | Nvarchar | 100 | - | 失敗訊息 |
| DS11 | RetryInterval HHMMSS | Nvarchar | 6 | - | 重試間隔時間格式(HHMMSS) |
| DS12 | NextRetryDateTime | Nvarchar | 17 | - | 下次重試日期時間 |

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- DS：DS01
- DS1：DS02+DS12
- DS2：DS02+DS06+DS07
- DS3：DS02+DS0901+DS0902+DS0903+DS0904+DS0905+DS0906
- DS4：DS04+DS05+DS02
- DS5：DS02+DS03+DS04+DS05
- DS6：DS03+DS04+DS05

## 變更歷史記錄

| 版本 | 日期 | 修改人 | 變更描述 |
| ---- | ---- | ------ | -------- |
| 1.0  | 2024-06-05 | System | 初始版本建立，格式標準化 |
