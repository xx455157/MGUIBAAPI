# HTFRB\_食譜生效日期紀錄檔

## 資料表資訊

**系統：** [[HT2000]]  
**資料表代碼：** HTFRB  
**中文名稱：** 食譜生效日期紀錄檔  
**主鍵：** FRB01, FRB02, FRB21 (食譜代碼, 材料號碼, 生效日期)  
**最後更新日期：** 2024-12-30  
**版本：** 1.0

## 資料表說明

本資料表為 HOTEL2000 系統中的食譜生效日期紀錄檔，用於記錄食譜檔案 HTFR 的生效日期資訊。該資料表主要功能是管理食譜材料的生效期間，支援食譜版本控制和成本計算，並記錄食譜編製的相關資訊。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱     | 資料型態 | 長度 | 屬性 | 備註說明                       |
| -------- | ------------ | -------- | ---- | ---- | ------------------------------ |
| FRB001   | 建檔日期     | NVARCHAR | 8    | M    | RDDATE 格式(YYYYMMDD)          |
| FRB002   | 建檔時間     | NVARCHAR | 8    | M    | RDTIME 格式(HHMMSSFF)          |
| FRB003   | 建檔工作站   | NVARCHAR | 10   | M    | RDTERM                         |
| FRB004   | 建檔人       | NVARCHAR | 10   | M    | RDCLRK                         |
| FRB005   | 更改日期     | NVARCHAR | 8    | -    | MDDATE 格式(YYYYMMDD)          |
| FRB006   | 更改時間     | NVARCHAR | 8    | -    | MDTIME 格式(HHMMSSFF)          |
| FRB007   | 更改工作站   | NVARCHAR | 10   | -    | MDTERM                         |
| FRB008   | 更改人       | NVARCHAR | 10   | -    | MDCLRK                         |
| FRB01    | 食譜代碼     | NVARCHAR | 10   | P,F  | Recipe Code，參照 HTFR         |
| FRB02    | 材料號碼     | NVARCHAR | 20   | P,F  | ITEM.NO，參照庫存資料表        |
| FRB03    | 序號         | NUMERIC  | 10   | -    | SERIAL#                        |
| FRB04    | 用量單位     | NVARCHAR | 10   | -    | UNIT                           |
| FRB05    | 使用量       | NUMERIC  | 25,4 | -    | QTY                            |
| FRB06    | 成本因素     | NUMERIC  | 25,4 | -    | COST Factor                    |
| FRB07    | 食譜名稱     | NVARCHAR | 40   | -    | Recipe Description             |
| FRB08    | 編表人       | NVARCHAR | 20   | -    | Creator                        |
| FRB09    | 編表日期     | NVARCHAR | 8    | -    | Create Date 格式(YYYYMMDD)     |
| FRB10    | 材料成本     | NUMERIC  | 25,4 | -    | COST                           |
| FRB11    | 成本參加月份 | NVARCHAR | 6    | -    | Cost Month 格式(YYYYMM)        |
| FRB12    | 市場成本     | NUMERIC  | 25,4 | -    | MarketCost                     |
| FRB13    | 報價廠商     | NVARCHAR | 10   | -    | QuoteSupplier                  |
| FRB14    | 報價日期     | NVARCHAR | 8    | -    | QuoteDate 格式(YYYYMMDD)       |
| FRB15    | 庫存公司別   | NVARCHAR | 2    | F    | Company，參照 ARTHGUI/A01      |
| FRB16    | 提供份數     | NUMERIC  | 25,4 | -    | QTY_Make                       |
| FRB17    | 材料來源     | NVARCHAR | 1    | -    | Source (1:庫存料號 2:食譜編號) |
| FRB18    | 份數單位     | NVARCHAR | 10   | -    | -                              |
| FRB19    | 人工成本     | NUMERIC  | 25,4 | -    | -                              |
| FRB20    | 標準售價     | NUMERIC  | 25,4 | -    | -                              |
| FRB21    | 生效日期     | NVARCHAR | 8    | P    | 格式(YYYYMMDD)                 |
| FRB221   | 圖樣編號 1   | NVARCHAR | 120  | -    | -                              |
| FRB222   | 圖樣編號 2   | NVARCHAR | 120  | -    | -                              |
| FRB223   | 圖樣編號 3   | NVARCHAR | 120  | -    | -                              |

## FRB17 代碼說明 (材料來源)

| 代碼 | 說明     | 詳細說明                     |
| ---- | -------- | ---------------------------- |
| 1    | 庫存料號 | 材料來源為庫存管理系統的料號 |
| 2    | 食譜編號 | 材料來源為其他食譜編號       |

## 索引定義說明

| 索引名稱  | 欄位組成            | 索引類型    | 說明             |
| --------- | ------------------- | ----------- | ---------------- |
| PK_HTFRB  | FRB01, FRB02, FRB21 | PRIMARY KEY | 主鍵索引         |
| IDX_FRB01 | FRB01               | NON-UNIQUE  | 食譜代碼查詢索引 |
| IDX_FRB02 | FRB02               | NON-UNIQUE  | 材料號碼查詢索引 |
| IDX_FRB21 | FRB21               | NON-UNIQUE  | 生效日期查詢索引 |

## 資料關聯說明

### 資料表間關聯

- 與 HTFR 資料表的關聯:

  - HTFRB.FRB01 = HTFR.FR01
  - 關聯類型: 多對一 (多筆生效日期記錄對應一個食譜)

- 與庫存資料表的關聯:

  - HTFRB.FRB02 = 庫存資料表.料號
  - 關聯類型: 多對一 (多筆記錄對應一個庫存料號)

- 與 ARTHGUI.A01 資料表的關聯:
  - HTFRB.FRB15 = ARTHGUI.A01.公司別代碼
  - 關聯類型: 多對一 (多筆記錄對應一個公司別)

### 參照完整性約束

1. 食譜代碼(FRB01)必須存在於食譜主檔(HTFR)中
2. 材料號碼(FRB02)必須存在於對應的庫存資料表中
3. 庫存公司別(FRB15)必須存在於公司基本資料檔(ARTHGUI/A01)中

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- FRB：FRB01、FRB02、FRB21 (主鍵組合)
- 生效日期管理：FRB21 用於控制食譜材料的生效期間
- 成本計算：FRB10、FRB12、FRB19 用於食譜成本分析

## 變更歷史記錄

| 版本 | 日期       | 修改人 | 變更描述                             |
| ---- | ---------- | ------ | ------------------------------------ |
| 1.0  | 2024-12-30 | Chris  | 初始版本建立，確認與 Z:\NewData 一致 |
