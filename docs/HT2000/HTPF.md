# HTPF_餐廳帳單主檔

## 資料表資訊

**系統：** [[HT2000]] 
**資料表代碼：** HTPF
**中文名稱：** 餐廳帳單主檔
**主鍵：** PF001, PF002, PF003 (建檔日期, 建檔時間, 建檔工作站)
**最後更新日期：** 2024-06-05
**版本：** 1.0

## 資料表說明

本資料表為 HOTEL2000 系統中的餐廳帳單主檔，用於記錄餐廳消費的帳單資料，包含客戶資訊、消費明細、結帳資訊等內容。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱             | 資料型態 | 長度 | 屬性 | 備註說明                                           |
| -------- | -------------------- | -------- | ---- | ---- | -------------------------------------------------- |
| PF001    | 建檔日期             | NVARCHAR | 8    | P,M  | RDDATE 格式(YYYYMMDD)                              |
| PF002    | 建檔時間             | NVARCHAR | 8    | P,M  | RDTIME                                             |
| PF003    | 建檔工作站           | NVARCHAR | 10   | P,M  | RDTERM                                             |
| PF004    | 建檔人               | NVARCHAR | 10   | M    | RDCLRK                                             |
| PF005    | 更改日期             | NVARCHAR | 8    | -    | MDDATE 格式(YYYYMMDD)                              |
| PF006    | 更改時間             | NVARCHAR | 8    | -    | MDTIME                                             |
| PF007    | 更改工作站           | NVARCHAR | 10   | -    | MDTERM                                             |
| PF008    | 更改人               | NVARCHAR | 10   | -    | MDCLRK                                             |
| PF01     | 餐廳代碼             | NVARCHAR | 6    | M,F  | POS ID，參照 TBPOS                                 |
| PF02     | 會計日期             | NVARCHAR | 8    | M    | HTDATE 格式(YYYYMMDD)                              |
| PF03     | 桌號                 | NVARCHAR | 10   | M    | TABLE                                              |
| PF04     | 序號                 | NVARCHAR | 2    | M    | SQNO                                               |
| PF05     | 帳單名稱             | NVARCHAR | 16   | -    | PFNAME                                             |
| PF06     | 點單號碼             | NVARCHAR | 10   | -    | KOT#，HTP03 使用 'M'=跨日修改                      |
| PF07     | 統一編號             | NVARCHAR | 10   | -    | REGISTED#                                          |
| PF08     | 發票號碼             | NVARCHAR | 12   | -    | INVOICE#                                           |
| PF09     | 折扣比               | NUMERIC  | 25,4 | -    | DISCOUNT                                           |
| PF10     | 備註                 | NVARCHAR | 50   | -    | REMARKS，國賓:合併至哪帳單會員過帳=會員姓名        |
| PF11     | 套餐代碼             | NVARCHAR | 6    | F    | SETMENU，參照 HTSM                                 |
| PF12     | 住客帳單             | NVARCHAR | 8    | F    | HFNO，參照 HTHF                                    |
| PF13     | 登錄管制             | NVARCHAR | 1    | -    | CLOSED，'*'-CLOSED (物業接口)                      |
| PF14     | 應收帳碼             | NVARCHAR | 20   | -    | ARNO                                               |
| PF15     | 團名/旅客名          | NVARCHAR | 50   | -    | GROUP/GUEST                                        |
| PF16     | 用餐人數             | NUMERIC  | 25,4 | -    | PAX，0:加點,分帳,外帶                              |
| PF17     | 開桌時間             | NVARCHAR | 4    | -    | CITIME，格式(HHMM)                                 |
| PF18     | 結帳時間             | NVARCHAR | 4    | -    | COTIME，格式(HHMM)                                 |
| PF19     | 服務人員             | NVARCHAR | 10   | F    | SERVER，參照 HTPS                                  |
| PF20     | 會員號碼             | NVARCHAR | 20   | F    | MEMBER ID，參照 HTGR (上海已增加至20)              |
| PF21     | 訂宴號碼             | NVARCHAR | 10   | F    | BQNO，參照 RV01+HP28                               |
| PF22     | 客戶來源             | NVARCHAR | 6    | F    | SOURCE，參照 TBPS                                  |
| PF23     | 預計結帳時間         | NVARCHAR | 8    | -    | 格式(HHMM0000)                                     |
| PF24     | 消費性質             | NVARCHAR | 8    | F    | 參照 HC02 (HC01的第一碼是'B'者)                    |
| PF25     | FILLER               | NVARCHAR | 8    | -    | -                                                  |
| PF26     | 合約公司FLAG         | NVARCHAR | 1    | -    | 若有*號則PF20代表合約公司代碼GR01                  |
| PF27     | 來客區分             | NVARCHAR | 6    | F    | 參照TB01='CUSTYP'(For上海一茶一坐)方案代碼         |
| PF28     | 客人關係             | NVARCHAR | 1    | F    | 參照TB01='CUSREL'(For上海一茶一坐)Hold單待處理="*" |
| PF29     | 男性年齡/PR01        | NVARCHAR | 10   | -    | (For上海一茶一坐)國賓:PR01                         |
| PF30     | 女性年齡             | NVARCHAR | 10   | -    | (For上海一茶一坐)                                  |
| PF31     | 整桌口味             | NVARCHAR | 10   | -    | 0                                                  |
| PF32     | 用餐人數-男          | NUMERIC  | 25,4 | -    | -                                                  |
| PF33     | 用餐人數-女          | NUMERIC  | 25,4 | -    | -                                                  |
| PF34     | 桌數                 | NUMERIC  | 25,4 | -    | Table Count (For國賓)                              |
| PF35     | 點餐時間             | NVARCHAR | 8    | -    | HHMM(上海)/HHMMss(QRCode點餐時間)                  |
| PF36     | 劃單時間             | NVARCHAR | 8    | -    | HHMM(上海)/HHMMss(QRCode最後點餐時間)              |
| PF37     | 呼叫器號碼           | NVARCHAR | 8    | -    | FOR上海魯肉飯                                      |
| PF38     | 語系                 | NVARCHAR | 8    | -    | Locale，IETF語言標籤(空白=zh-tw)                   |
| PF39     | FILLER               | NVARCHAR | 8    | -    | FOR上海魯肉飯                                      |
| PF40     | FILLER               | NVARCHAR | 8    | -    | FOR上海魯肉飯                                      |
| PF41     | FILLER               | NUMERIC  | 25,4 | -    | FOR上海魯肉飯                                      |
| PF42     | FILLER               | NUMERIC  | 25,4 | -    | FOR上海魯肉飯                                      |
| PF43     | FILLER               | NUMERIC  | 25,4 | -    | FOR上海魯肉飯                                      |
| PF44     | 用餐人數-小孩        | NUMERIC  | 25,4 | -    | For上海一茶一坐                                    |
| PF45     | Jkopay訂單流水序號   | NUMERIC  | 25,4 | -    | S101220029                                         |
| PF46     | FILLER               | NUMERIC  | 25,4 | -    | -                                                  |
| PF47     | FILLER               | NUMERIC  | 25,4 | -    | -                                                  |
| PF48     | FILLER               | NUMERIC  | 25,4 | -    | -                                                  |
| PF49     | FILLER               | NUMERIC  | 25,4 | -    | -                                                  |
| PF50     | 後台拋轉記號         | NVARCHAR | 1    | -    | TransFlag，'*'=已拋轉後台                          |
| PF51     | 帳單調整FLAG         | NVARCHAR | 1    | -    | FOR世貿('*':調整)                                  |
| PF52     | 備餐狀態             | NVARCHAR | 1    | -    | STATUS，空白=備餐中,1=叫號,2=已取                  |
| PF53     | 原帳單PosID          | NVARCHAR | 8    | -    | (退款作業)                                         |
| PF54     | 原帳單會計日期       | NVARCHAR | 8    | -    | (退款作業)                                         |
| PF55     | FILLER               | NVARCHAR | 8    | -    | For上海導購ID                                      |
| PF56     | 國籍                 | NVARCHAR | 10   | F    | ORIGIN，參照TBOR                                   |
| PF57     | 宴會類別             | NVARCHAR | 10   | F    | BQTYPE，參照TBBT                                   |
| PF58     | 原桌號               | NVARCHAR | 10   | -    | (退款作業)For天成一卡通Z                           |
| PF59     | 原帳單號碼           | NVARCHAR | 50   | -    | (退款作業)                                         |
| PF60     | 外交官免稅證憑證號碼 | NVARCHAR | 50   | -    | For開泰豐                                          |
| PF61     | FILLER               | NVARCHAR | 50   | -    | S051003060使用                                     |

## 資料關聯說明

### 資料表間關聯

- 與 TBPOS 資料表的關聯:

  - HTPF.PF01 = TBPOS.POS代碼
  - 關聯類型: 多對一 (多筆帳單對應一個餐廳)
- 與 HTSM 資料表的關聯:

  - HTPF.PF11 = HTSM.SM代碼
  - 關聯類型: 多對一 (多筆帳單對應一個套餐)
- 與 HTHF 資料表的關聯:

  - HTPF.PF12 = HTHF.HF代碼
  - 關聯類型: 多對一 (多筆帳單對應一個住客帳單)
- 與 HTPS 資料表的關聯:

  - HTPF.PF19 = HTPS.PS代碼
  - 關聯類型: 多對一 (多筆帳單對應一個服務人員)
- 與 HTGR 資料表的關聯:

  - HTPF.PF20 = HTGR.GR代碼
  - 關聯類型: 多對一 (多筆帳單對應一個會員)
- 與 TBPS 資料表的關聯:

  - HTPF.PF22 = TBPS.PS代碼
  - 關聯類型: 多對一 (多筆帳單對應一個客戶來源)
- 與 HC02 資料表的關聯:

  - HTPF.PF24 = HC02.HC代碼
  - 關聯類型: 多對一 (多筆帳單對應一個消費性質)
- 與 TBOR 資料表的關聯:

  - HTPF.PF56 = TBOR.OR代碼
  - 關聯類型: 多對一 (多筆帳單對應一個國籍)
- 與 TBBT 資料表的關聯:

  - HTPF.PF57 = TBBT.BT代碼
  - 關聯類型: 多對一 (多筆帳單對應一個宴會類別)

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- PF：PF001、PF002、PF003
- PFA1：PF01+PF02+PF03+PF04
- PFA2：PF01+PF06
- PFA3：PF21
- PFA4：PF01+PF02+PF03+PF05
- PFA5：PF53+PF54+PF58+PF59
- PFA6：PF01+PF02+PF18

### 特殊欄位說明

- PF13: (SHPSP01.EXE) (SHPSP14.EXE) 當轉出的資料會自動上*號 (FOR上海接口程式)
- PF51: 當有進行帳單內容調整作業時，其該欄位上記號。
- PF60: 當有零稅率時，帶供USER登打外交官免稅證憑證號碼並寫入PF60，列印電子發票時在銷貨明細表明細表中需列印。

## 變更歷史記錄

| 版本 | 日期       | 修改人 | 變更描述                 |
| ---- | ---------- | ------ | ------------------------ |
| 1.0  | 2024-06-05 | System | 初始版本建立，格式標準化 |
