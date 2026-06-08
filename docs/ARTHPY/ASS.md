# ASS 臨時工發薪檔

## 資料表資訊

- **資料表名稱**: ASS
- **中文名稱**: 臨時工發薪檔
- **用途**: 記錄臨時工薪資發放相關資料

## 欄位定義


| 欄位名稱  | 資料型態   | 長度  | 說明                                                         |
| ----- | ------ | --- | ---------------------------------------------------------- |
| AS001 | Text   | 8   | 建檔日期                                                       |
| AS002 | Text   | 8   | 建檔時間                                                       |
| AS003 | Text   | 10  | 建檔工作站                                                      |
| AS004 | Text   | 10  | 建檔人                                                        |
| AS005 | Text   | 8   | 更改日期                                                       |
| AS006 | Text   | 8   | 更改時間                                                       |
| AS007 | Text   | 10  | 更改工作站                                                      |
| AS008 | Text   | 10  | 更改人                                                        |
| AS01  | Text   | 8   | 公司別                                                        |
| AS02  | Text   | 8   | 部門別                                                        |
| AS03  | Text   | 8   | 日期                                                         |
| AS04  | Text   | 10  | 批次/時段 (HHMM-HHMM)                                          |
| AS05  | Text   | 10  | 身份証號碼                                                      |
| AS06  | Text   | 10  | 員工代號                                                       |
| AS07  | Text   | 20  | 員工姓名                                                       |
| AS08  | Double | -   | 每小時單價                                                      |
| AS09  | Double | -   | 實際工時 (計薪時數)                                                |
| AS10  | Double | -   | 金額                                                         |
| AS11  | Text   | 40  | 備註                                                         |
| AS12  | Text   | 12  | 領取時間 (YYYYMMDDHHMM)                                        |
| AS13  | Text   | 12  | Filler (補撥日期)                                              |
| AS14  | Text   | 12  | 轉薪資系統時間 (YYYYMMDDHHMM)                                     |
| AS15  | Text   | 2   | 轉薪資系統之發薪期別                                                 |
| AS16  | Text   | 4   | 工作類別 (支薪代號)                                                |
| AS17  | Text   | 10  | Filler (發薪年)                                               |
| AS18  | Text   | 10  | Filler (發薪月)                                               |
| AS19  | Text   | 10  | Filler (發薪日)                                               |
| AS20  | Text   | 8   | Date Filler (津貼) (維多麗亞 ABM15 早班津貼)                         |
| AS21  | Text   | 8   | Date Filler (審核時數)                                         |
| AS22  | Text   | 8   | Date Filler                                                |
| AS23  | Double | -   | Filler (刷卡次數)                                              |
| AS24  | Double | -   | Filler (打卡時數)                                              |
| AS25  | Double | -   | Filler (津貼金額) (維多麗亞 ABM15 晚班津貼)                            |
| AS26  | Text   | 1   | Filler (午)                                                 |
| AS27  | Text   | 1   | Filler (晚)                                                 |
| AS28  | Text   | 1   | Filler (宵夜)                                                |
| AS29  | Text   | 30  | 審核備註                                                       |
| AS30  | Text   | 30  | Filler (君悅、日光溫泉、維多麗亞訂製: 最後審核時間，此不為空白表示來源為 PYGHP01、PYRGP01) |
| AS31  | Text   | 30  | Filler                                                     |
| AS32  | Text   | 1   | 預計報到人員 (Y)                                                 |
| AS33  | Text   | 1   | 預計報到人員覆核 (Y)                                               |
| AS34  | Text   | 4   | 預計報到人員時間 (起)                                               |
| AS35  | Text   | 4   | 預計報到人員時間 (迄)                                               |
| AS36  | Double | -   | 預計報到人員工時                                                   |
| AS37  | Double | -   | 預計報到人員早班津貼                                                 |
| AS38  | Double | -   | 預計報到人員晚班津貼                                                 |


## 索引定義

### 主鍵索引

- **索引名稱**: PK_ASS
- **索引欄位**: AS01 + AS02 + AS03 + AS04 + AS05
- **索引類型**: Primary Key (P & U)

## 備註

- 本資料表用於記錄臨時工薪資發放相關資料
- 包含建檔、更改、薪資計算、審核等完整流程記錄
- 支援多種津貼類型（早班、晚班、午、晚、宵夜）
- 可與薪資系統進行資料轉換

