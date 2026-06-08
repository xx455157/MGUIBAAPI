# HTFA_有效期間菜單與庫存品項對照檔

## 資料表資訊

**系統：** [[HT2000]] 
**資料表代碼：** HTFA  
**中文名稱：** 有效期間菜單與庫存品項對照檔  
**主鍵：** FA01, FA02, FA03, FA04 (餐廳代碼, 菜單代碼, 有效起日, 有效迄日)  
**最後更新日期：** 2024-06-05  
**版本：** 1.1

## 資料表說明

本資料表為 HOTEL2000 系統中的有效期間菜單與庫存品項對照檔，用於記錄在特定有效期間內餐廳菜單與庫存商品之間的對應關係。該資料表主要功能是連結餐飲菜單與後台庫存管理系統，並支援設定不同時段的菜單與庫存品項對照。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱 | 資料型態 | 長度 | 屬性 | 備註說明 |
|----------|----------|----------|------|------|----------|
| FA001 | 建檔日期 | NVARCHAR | 8 | M | RDDATE 格式(YYYYMMDD) |
| FA002 | 建檔時間 | NVARCHAR | 8 | M | RDTIME |
| FA003 | 建檔工作站 | NVARCHAR | 10 | M | RDTERM |
| FA004 | 建檔人 | NVARCHAR | 10 | M | RDCLRK |
| FA005 | 更改日期 | NVARCHAR | 8 | - | MDDATE 格式(YYYYMMDD) |
| FA006 | 更改時間 | NVARCHAR | 8 | - | MDTIME |
| FA007 | 更改工作站 | NVARCHAR | 10 | - | MDTERM |
| FA008 | 更改人 | NVARCHAR | 10 | - | MDCLRK |
| FA01 | 餐廳代碼 | NVARCHAR | 6 | P,F | 參照 TBPOS |
| FA02 | 菜單代碼 | NVARCHAR | 20 | P,F | 參照 HTFB |
| FA03 | 有效起日 | NVARCHAR | 8 | P | STARTDATE 格式(YYYYMMDD) |
| FA04 | 有效迄日 | NVARCHAR | 8 | P | ENDDATE 格式(YYYYMMDD) |
| FA05 | 公司別 | NVARCHAR | 2 | F | 參照 GUI/A01 |
| FA06 | 產品編號 | NVARCHAR | 20 | F | 參照 GUI/B10 |
| FA07 | FILLER | NVARCHAR | 10 | - | - |
| FA08 | FILLER | NVARCHAR | 10 | - | - |
| FA09 | FILLER | NVARCHAR | 10 | - | - |
| FA10 | FILLER | NVARCHAR | 1 | - | - |
| FA11 | FILLER | NVARCHAR | 1 | - | - |
| FA12 | FILLER | NVARCHAR | 1 | - | - |
| FA13 | FILLER | NVARCHAR | 8 | - | - |
| FA14 | FILLER | NVARCHAR | 8 | - | - |
| FA15 | FILLER | NVARCHAR | 8 | - | - |
| FA16 | FILLER | NUMERIC | 25,4 | - | - |
| FA17 | FILLER | NUMERIC | 25,4 | - | - |
| FA18 | FILLER | NUMERIC | 25,4 | - | - |

## 資料關聯說明

### 資料表間關聯

- 與 TBPOS 資料表的關聯:
  - HTFA.FA01 = TBPOS.POS代碼
  - 關聯類型: 多對一 (多筆菜單庫存品項對照記錄對應一個餐廳)

- 與 HTFB 資料表的關聯:
  - HTFA.FA02 = HTFB.FB02
  - 關聯類型: 多對一 (多筆對照記錄對應一個菜單)

- 與 ARTHGUI.A01 資料表的關聯:
  - HTFA.FA05 = ARTHGUI.A01.公司別代碼
  - 關聯類型: 多對一 (多筆記錄對應一個公司別)

- 與 ARTHGUI.B10 資料表的關聯:
  - HTFA.FA06 = ARTHGUI.B10.產品編號
  - 關聯類型: 多對一 (多筆記錄對應一個庫存品項)

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- FA：FA01、FA02、FA03、FA04

## 變更歷史記錄

| 版本 | 日期 | 修改人 | 變更描述 |
| ---- | ---- | ------ | -------- |
| 1.0  | 2024-06-05 | System | 初始版本建立，格式標準化 |
| 1.1  | 2024-06-05 | System | 修正資料表中文名稱及說明 |
