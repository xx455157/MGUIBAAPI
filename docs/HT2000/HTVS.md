# HTVS - 訂房資料表

## 基本資訊

- **系統名稱**：[[HT2000]] 
- **資料表代碼**：HTVS
- **資料表名稱**：訂房資料表
- **主鍵**：VS24 (住宿號碼)
- **版本**：1.0
- **最後更新**：2024/09/22

## 資料表說明

本資料表用於記錄飯店訂房相關資訊，包含訂房、入住、退房等完整住宿記錄。

## 欄位定義

| 欄位代號 | 欄位名稱       | 資料型態 | 長度 | 備註                      |
| -------- | -------------- | -------- | ---- | ------------------------- |
| VS001    | 建檔日期       | NVARCHAR | 8    | (YYYYMMDD)                |
| VS002    | 建檔時間       | NVARCHAR | 8    | (HHMMSS)                  |
| VS003    | 建檔工作站     | NVARCHAR | 10   |                           |
| VS004    | 建檔人         | NVARCHAR | 10   |                           |
| VS005    | 更改日期       | NVARCHAR | 8    | (YYYYMMDD)                |
| VS006    | 更改時間       | NVARCHAR | 8    | (HHMMSS)                  |
| VS007    | 更改工作站     | NVARCHAR | 10   |                           |
| VS008    | 更改人         | NVARCHAR | 10   |                           |
| VS01     | 訂房號碼       | NVARCHAR | 5    |                           |
| VS02     | 客戶號碼       | NVARCHAR | 10   |                           |
| VS03     | 到達日期       | NVARCHAR | 8    | (YYYYMMDD)                |
| VS04     | 退房日期       | NVARCHAR | 8    | (YYYYMMDD)                |
| VS05     | 消費單位       | NVARCHAR | 6    | 參照 TBPOS                |
| VS06     | 房號           | NVARCHAR | 6    |                           |
| VS07     | 專案名稱       | NVARCHAR | 40   |                           |
| VS08     | 折扣比         | NUMERIC  | 25,4 |                           |
| VS09     | 旅遊名稱       | NVARCHAR | 50   |                           |
| VS10     | 合約公司       | NVARCHAR | 10   |                           |
| VS11     | 房價列印       | NVARCHAR | 1    | '*'=YES                   |
| VS12     | 長期住宿       | NVARCHAR | 1    | '*'=YES                   |
| VS13     | 登帳管制       | NVARCHAR | 1    | '*'=YES                   |
| VS14     | 付款方式       | NVARCHAR | 6    | 參照 TBPT                 |
| VS15     | 訂房類別       | NVARCHAR | 6    | 參照 TBVT (Fit/Git)       |
| VS16     | 訂房狀況       | NVARCHAR | 6    | 參照 TBBK (Act/Nosh/Cxnl) |
| VS17     | 登記狀況       | NVARCHAR | 6    | 參照 TBCI (Rv/IH/Co)      |
| VS18     | 登記記錄       | NVARCHAR | 14   | HHMM+UserID               |
| VS19     | 遷出記錄       | NVARCHAR | 14   | HHMM+UserID               |
| VS20     | 備註           | NVARCHAR | 100  |                           |
| VS21     | 訂房量         | NUMERIC  | 25,4 |                           |
| VS22     | 住宿量         | NUMERIC  | 25,4 |                           |
| VS23     | 登記類別       | NVARCHAR | 6    | 參照 TBCT (Nrm/Lat/Erl)   |
| VS231    | 遷出類別       | NVARCHAR | 6    | 參照 TBCT (Nrm/Lat/Erl)   |
| VS24     | 住宿號碼       | NVARCHAR | 10   | 主鍵                      |
| VS25     | 業務員         | NVARCHAR | 10   | 參照 HTUS (RM)            |
| VS26     | 訂宴碼         | NVARCHAR | 1    | '*'=BQ                    |
| VS27     | 價格類別       | NVARCHAR | 6    |                           |
| VS28     | 人數           | NUMERIC  | 25,4 |                           |
| VS29     | 兒童           | NUMERIC  | 25,4 |                           |
| VS30     | 房價           | NUMERIC  | 25,4 |                           |
| VS31     | 服務費         | NUMERIC  | 25,4 |                           |
| VS32     | 客房消費       | NUMERIC  | 25,4 | Member 不作統計           |
| VS33     | 餐飲消費       | NUMERIC  | 25,4 | Member 不作統計           |
| VS34     | 其它消費       | NUMERIC  | 25,4 | Member 不作統計           |
| VS35     | 業務碼         | NVARCHAR | 6    | 參照 TBSR                 |
| VS36     | 信用卡預刷額度 | NUMERIC  | 25,4 |                           |
| VS37     | DoNotMove      | NVARCHAR | 1    | 1=鎖定, 0=開啟            |
| VS38     | 是否接送       | NVARCHAR | 1    | 1=已接送                  |
| VS39     | FILLER         | NVARCHAR | 1    |                           |
| VS40     | 原始房號       | NVARCHAR | 8    | 轉H房之前最後實體房間房號 |
| VS41     | FILLER         | NVARCHAR | 8    |                           |
| VS42     | FILLER         | NVARCHAR | 8    |                           |
| VS43     | 條款碼         | NVARCHAR | 10   |                           |
| VS44     | 舊旅客號碼     | NVARCHAR | 10   | 用於旅客合併              |
| VS45     | FILLER         | NVARCHAR | 10   |                           |
| VS46     | FILLER         | NUMERIC  | 25,4 |                           |
| VS47     | FILLER         | NUMERIC  | 25,4 |                           |
| VS48     | FILLER         | NUMERIC  | 25,4 |                           |

## 索引定義

- 主鍵索引：VS24
- 複合索引：
  - VSA1：VS06 + VS17
  - VSA2：VS02
  - VSA3：VS01
  - VSA4：VS10
  - VSA5：VS03
  - VSA6：VS17
  - VSA7：VS04
  - VSA8：VS06、VS24、VS16、VS17
  - VSA9：VS03、VS16、VS04、VS24、VS01、VS06
  - VSA10：VS17、VS03、VS04
  - VSA11：VS06、VS03、VS16、VS17、VS04、VS24

## 資料關聯說明

- VS05 (消費單位) 參照 TBPOS 資料表
- VS14 (付款方式) 參照 TBPT 資料表
- VS15 (訂房類別) 參照 TBVT 資料表
- VS16 (訂房狀況) 參照 TBBK 資料表
- VS17 (登記狀況) 參照 TBCI 資料表
- VS23/VS231 (登記/遷出類別) 參照 TBCT 資料表
- VS25 (業務員) 參照 HTUS 資料表
- VS35 (業務碼) 參照 TBSR 資料表

## 變更歷史記錄

| 日期       | 版本 | 變更內容                             | 修改人 |
| ---------- | ---- | ------------------------------------ | ------ |
| 2014/01/09 | 1.0  | 確認與Z:\NewData一致                 | Chris  |
| 2017/11/07 | 1.1  | 拿VC38來當"是否接送"欄位             | 小白   |
| 2019/10/23 | 1.2  | 將VS44設定為舊旅客號碼，用以旅客合併 | 小白   |
| 2021/09/30 | 1.3  | 不再從VS07看專案從AC14查看專案       | 博文   |
| 2021/12/20 | 1.4  | 增加INDEX(VSA11)                     | 博文   |
| 2024/09/22 | 1.5  | VS40紀錄轉H房之前最後實體房間房號    | Mike   |
