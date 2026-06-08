# HTFRC\_食譜成本中心生效日期紀錄檔

## 資料表資訊

**系統：** [[HT2000]] 
**資料表代碼：** HTFRC  
**中文名稱：** 食譜成本中心生效日期紀錄檔  
**主鍵：** FRC01, FRC02, FRC03, FRC04 (食譜代碼, 材料號碼, 公司別, 成本中心)  
**最後更新日期：** 2024-12-30  
**版本：** 1.0

## 資料表說明

本資料表為 HOTEL2000 系統中的食譜成本中心生效日期紀錄檔，用於記錄食譜檔案 HTFRA 的生效日期資訊。該資料表主要功能是管理食譜材料在不同成本中心的生效期間，支援食譜成本分析和成本中心管理，並記錄食譜編製的相關資訊。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱     | 資料型態 | 長度 | 屬性 | 備註說明                        |
| -------- | ------------ | -------- | ---- | ---- | ------------------------------- |
| FRC001   | 建檔日期     | NVARCHAR | 8    | M    | RDDATE 格式(YYYYMMDD)           |
| FRC002   | 建檔時間     | NVARCHAR | 8    | M    | RDTIME 格式(HHMMSSFF)           |
| FRC003   | 建檔工作站   | NVARCHAR | 10   | M    | RDTERM                          |
| FRC004   | 建檔人       | NVARCHAR | 10   | M    | RDCLRK                          |
| FRC005   | 更改日期     | NVARCHAR | 8    | -    | MDDATE 格式(YYYYMMDD)           |
| FRC006   | 更改時間     | NVARCHAR | 8    | -    | MDTIME 格式(HHMMSSFF)           |
| FRC007   | 更改工作站   | NVARCHAR | 10   | -    | MDTERM                          |
| FRC008   | 更改人       | NVARCHAR | 10   | -    | MDCLRK                          |
| FRC01    | 食譜代碼     | NVARCHAR | 10   | P,F  | Recipe Code，參照 HTFRA         |
| FRC02    | 材料號碼     | NVARCHAR | 20   | P,F  | ITEM.NO，參照庫存資料表         |
| FRC03    | 公司別       | NVARCHAR | 2    | P,F  | Company，參照 ARTHGUI/A01       |
| FRC04    | 成本中心     | NVARCHAR | 8    | P,F  | Cost Center，參照成本中心資料表 |
| FRC05    | 材料成本     | NUMERIC  | 25,4 | -    | COST                            |
| FRC06    | 成本參加月份 | NVARCHAR | 6    | -    | Cost Month 格式(YYYYMM)         |
| FRC07    | 市場成本     | NUMERIC  | 25,4 | -    | MarketCost                      |
| FRC08    | 報價廠商     | NVARCHAR | 10   | -    | QuoteSupplier                   |
| FRC09    | 報價日期     | NVARCHAR | 8    | -    | QuoteDate 格式(YYYYMMDD)        |
| FRC10    | 材料來源     | NVARCHAR | 1    | -    | Flag Free                       |
| FRC11    | 標記欄位 1   | NVARCHAR | 1    | -    | Flag Free                       |
| FRC12    | 標記欄位 2   | NVARCHAR | 1    | -    | Flag Free                       |
| FRC13    | 生效日期     | NVARCHAR | 8    | P    | Date Free 格式(YYYYMMDD)        |
| FRC14    | 日期欄位 1   | NVARCHAR | 8    | -    | Date Free 格式(YYYYMMDD)        |
| FRC15    | 日期欄位 2   | NVARCHAR | 8    | -    | Date Free 格式(YYYYMMDD)        |
| FRC16    | 備用欄位 1   | NVARCHAR | 10   | -    | Filler                          |
| FRC17    | 備用欄位 2   | NVARCHAR | 10   | -    | Filler                          |
| FRC18    | 備用欄位 3   | NVARCHAR | 10   | -    | Filler                          |
| FRC19    | 備註欄位 1   | NVARCHAR | 50   | -    | Memo                            |
| FRC20    | 備註欄位 2   | NVARCHAR | 50   | -    | Memo                            |
| FRC21    | 備註欄位 3   | NVARCHAR | 50   | -    | Memo                            |
| FRC22    | 數值欄位 1   | NUMERIC  | 25,4 | -    | Numeric Free                    |
| FRC23    | 數值欄位 2   | NUMERIC  | 25,4 | -    | Numeric Free                    |
| FRC24    | 數值欄位 3   | NUMERIC  | 25,4 | -    | Numeric Free                    |

## 索引定義說明

| 索引名稱  | 欄位組成                   | 索引類型    | 說明             |
| --------- | -------------------------- | ----------- | ---------------- |
| PK_HTFRC  | FRC01, FRC02, FRC03, FRC04 | PRIMARY KEY | 主鍵索引         |
| IDX_FRC01 | FRC01                      | NON-UNIQUE  | 食譜代碼查詢索引 |
| IDX_FRC02 | FRC02                      | NON-UNIQUE  | 材料號碼查詢索引 |
| IDX_FRC03 | FRC03                      | NON-UNIQUE  | 公司別查詢索引   |
| IDX_FRC04 | FRC04                      | NON-UNIQUE  | 成本中心查詢索引 |
| IDX_FRC13 | FRC13                      | NON-UNIQUE  | 生效日期查詢索引 |

## 資料關聯說明

### 資料表間關聯

- 與 HTFRA 資料表的關聯:

  - HTFRC.FRC01 = HTFRA.FRA01
  - 關聯類型: 多對一 (多筆成本中心記錄對應一個食譜)

- 與庫存資料表的關聯:

  - HTFRC.FRC02 = 庫存資料表.料號
  - 關聯類型: 多對一 (多筆記錄對應一個庫存料號)

- 與 ARTHGUI.A01 資料表的關聯:

  - HTFRC.FRC03 = ARTHGUI.A01.公司別代碼
  - 關聯類型: 多對一 (多筆記錄對應一個公司別)

- 與成本中心資料表的關聯:
  - HTFRC.FRC04 = 成本中心資料表.成本中心代碼
  - 關聯類型: 多對一 (多筆記錄對應一個成本中心)

### 參照完整性約束

1. 食譜代碼(FRC01)必須存在於食譜主檔(HTFRA)中
2. 材料號碼(FRC02)必須存在於對應的庫存資料表中
3. 公司別(FRC03)必須存在於公司基本資料檔(ARTHGUI/A01)中
4. 成本中心(FRC04)必須存在於成本中心資料表中

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- FRC：FRC01、FRC02、FRC03、FRC04 (主鍵組合)
- 成本中心管理：FRC04 用於區分不同成本中心的食譜材料
- 成本分析：FRC05、FRC07 用於食譜成本分析
- 生效日期管理：FRC13 用於控制食譜材料的生效期間

## 變更歷史記錄

| 版本 | 日期       | 修改人 | 變更描述                             |
| ---- | ---------- | ------ | ------------------------------------ |
| 1.0  | 2024-12-30 | Chris  | 初始版本建立，確認與 Z:\NewData 一致 |
