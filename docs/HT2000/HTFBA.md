# HTFBA_菜單與庫存品對照檔

## 資料表資訊

**系統：** [[HT2000]] 
**資料表代碼：** HTFBA  
**中文名稱：** 菜單與庫存品對照檔  
**主鍵：** FBA01, FBA02 (餐廳代碼, 菜單代碼)  
**最後更新日期：** 2024-06-05  
**版本：** 1.1

## 資料表說明

本資料表為 HOTEL2000 系統中的菜單與庫存品對照檔，用於記錄餐廳菜單與庫存商品間的對應關係與相關設定。此資料表主要功能是連結餐飲菜單與後台庫存管理系統。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱 | 資料型態 | 長度 | 屬性 | 備註說明 |
|----------|----------|----------|------|------|----------|
| FBA001 | 建檔日期 | NVARCHAR | 8 | M | RDDATE 格式(YYYYMMDD) |
| FBA002 | 建檔時間 | NVARCHAR | 8 | M | RDTIME |
| FBA003 | 建檔工作站 | NVARCHAR | 10 | M | RDTERM |
| FBA004 | 建檔人 | NVARCHAR | 10 | M | RDCLRK |
| FBA005 | 更改日期 | NVARCHAR | 8 | - | MDDATE 格式(YYYYMMDD) |
| FBA006 | 更改時間 | NVARCHAR | 8 | - | MDTIME |
| FBA007 | 更改工作站 | NVARCHAR | 10 | - | MDTERM |
| FBA008 | 更改人 | NVARCHAR | 10 | - | MDCLRK |
| FBA01 | 餐廳代碼 | NVARCHAR | 6 | P,F | 參照 TBPOS |
| FBA02 | 菜單代碼 | NVARCHAR | 20 | P | MENU |
| FBA03 | 條碼編號 | NVARCHAR | 20 | - | BARCODE，參照 ARTHIV.B10.B1032 |
| FBA04 | 庫存商品編號 | NVARCHAR | 5 | F | ITEM，參照 ARTHIV.B10.B1002 |
| FBA05 | 庫存公司別 | NVARCHAR | 2 | F | COMPANY，參照 ARTHIV.B10.B1001 |
| FBA06 | 標準庫存量 | NUMERIC | 25,4 | - | Standard Storage |
| FBA07 | 安全庫存量 | NUMERIC | 25,4 | - | Safe Storage |
| FBA08 | BO會計科目 | NVARCHAR | 10 | - | Account Code |
| FBA09 | BO專案代碼 | NVARCHAR | 10 | - | PKG Code，對應 GL 專案代碼 |
| FBA10 | 是否活動類菜單 | NVARCHAR | 10 | - | Y/N (上海永豐餘) |
| FBA11 | 拆帳項目 | NVARCHAR | 1 | - | Sepration bill，*表示yes (國賓) |
| FBA12 | 子菜單顯示 | NVARCHAR | 1 | - | ShowSetMenu，*表示不展開 (國賓) |
| FBA13 | 是否設定是菜譜 | NVARCHAR | 1 | - | Y/N (上海) |
| FBA14 | 關聯菜單代碼 | NVARCHAR | MAX | - | - |
| FBA15 | 歸屬餐品群代碼 | NVARCHAR | 8 | - | (上海) |
| FBA16 | 食類分析 | NVARCHAR | 8 | F | 參照 TBFY (上海永豐餘) |
| FBA17 | 備註事項 | NVARCHAR | 50 | - | (上海) |
| FBA18 | 第三語系菜名 | NVARCHAR | 50 | - | 3rdLang Menu (開泰豐) |
| FBA19 | Filler | NVARCHAR | 50 | - | - |
| FBA20 | 廚房顯示器名稱 | NVARCHAR | 20 | - | (華泰) |
| FBA21 | 版本號 | NVARCHAR | 20 | - | (上海) |
| FBA22 | Filler | NVARCHAR | 20 | - | - |
| FBA23 | 預計烹調時間 | NUMERIC | 25,4 | - | (華泰) |
| FBA24 | 標準出菜時間 | NUMERIC | 25,4 | - | (上海) |
| FBA25 | 到貨價/娛樂稅 | NUMERIC | 25,4 | - | (上海/六福) |
| FBA26 | 電子秤重標記 | NVARCHAR | 1 | F | 參照 TB WEITH (上海) |
| FBA27 | 外送價格(含稅) | NUMERIC | 25,4 | - | (RWD) |
| FBA28 | 外帶價格(含稅) | NUMERIC | 25,4 | - | (RWD) |
| FBA29 | FILLER | NUMERIC | 25,4 | - | - |
| FBA30 | 超商取貨 | NVARCHAR | 1 | - | Pickup，空白:否，Y:是 |
| FBA31 | FILLER | NVARCHAR | 1 | - | - |
| FBA32 | FILLER | NVARCHAR | 1 | - | - |
| FBA33 | FILLER | NVARCHAR | 1 | - | - |
| FBA34 | FILLER | NVARCHAR | 1 | - | - |
| FBA35 | FILLER | NVARCHAR | 20 | - | - |
| FBA36 | FILLER | NVARCHAR | 20 | - | - |
| FBA37 | FILLER | NVARCHAR | 20 | - | - |

## 資料關聯說明

### 資料表間關聯

- 與 TBPOS 資料表的關聯:
  - HTFBA.FBA01 = TBPOS.POS代碼
  - 關聯類型: 多對一 (多筆菜單對照記錄對應一個餐廳)

- 與 ARTHIV.B10 資料表的關聯:
  - HTFBA.FBA03 = ARTHIV.B10.B1032 (條碼)
  - HTFBA.FBA04 = ARTHIV.B10.B1002 (商品編號)
  - HTFBA.FBA05 = ARTHIV.B10.B1001 (公司別)
  - 關聯類型: 多對一 (多筆菜單對照記錄對應一個庫存商品)

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- HTFBA：FBA01、FBA02
- HTFBAA：FBA01+FBA03

## 變更歷史記錄

| 版本 | 日期 | 修改人 | 變更描述 |
| ---- | ---- | ------ | -------- |
| 1.0  | 2024-06-05 | System | 初始版本建立，格式標準化 |
| 1.1  | 2024-06-05 | System | 修正資料表中文名稱與說明 |
