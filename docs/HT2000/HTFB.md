# HTFB_餐廳菜單基本資料檔

## 資料表資訊

**系統：** [[HT2000]] 
**資料表代碼：** HTFB  
**中文名稱：** 餐廳菜單基本資料檔  
**主鍵：** FB01, FB02 (餐廳代碼, 菜單代碼)  
**最後更新日期：** 2024-06-05  
**版本：** 1.0

## 資料表說明

本資料表為 HOTEL2000 系統中的餐廳菜單基本資料檔，用於記錄餐廳菜單的基本資料、價格、出菜資訊等設定。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱 | 資料型態 | 長度 | 屬性 | 備註說明 |
|----------|----------|----------|------|------|----------|
| FB001 | 建檔日期 | NVARCHAR | 8 | M | RDDATE 格式(YYYYMMDD) |
| FB002 | 建檔時間 | NVARCHAR | 8 | M | RDTIME |
| FB003 | 建檔工作站 | NVARCHAR | 10 | M | RDTERM |
| FB004 | 建檔人 | NVARCHAR | 10 | M | RDCLRK |
| FB005 | 更改日期 | NVARCHAR | 8 | - | MDDATE 格式(YYYYMMDD) |
| FB006 | 更改時間 | NVARCHAR | 8 | - | MDTIME |
| FB007 | 更改工作站 | NVARCHAR | 10 | - | MDTERM |
| FB008 | 更改人 | NVARCHAR | 10 | - | MDCLRK |
| FB01 | 餐廳代碼 | NVARCHAR | 6 | P,F | POS，參照 TBPOS |
| FB02 | 菜單代碼 | NVARCHAR | 20 | P | MENU |
| FB03 | 菜單名稱 | NVARCHAR | 40 | M | NAME |
| FB04 | 列印名稱 | NVARCHAR | 40 | - | PRINT |
| FB05 | 單位 | NVARCHAR | 6 | F | UNIT，參照 TBUN |
| FB06 | 科目代號 | NVARCHAR | 6 | F | ACCOUNT，參照 HTCA |
| FB07 | 未稅金額 | NUMERIC | 25,4 | - | PRICE2 |
| FB08 | 含稅金額 | NUMERIC | 25,4 | M | PRICE3 |
| FB09 | 廚房代碼 | NVARCHAR | 6 | F | KITCHEN，參照 TBDP |
| FB10 | PLU代碼 | NVARCHAR | 8 | - | PLU Code |
| FB11 | 訂價日期 | NVARCHAR | 8 | - | LPDATE 格式(YYYYMMDD) |
| FB12 | 食譜代碼 | NVARCHAR | 10 | - | RCPNO |
| FB13 | 時價 | NVARCHAR | 1 | - | OPEN(*)，是否為時價 |
| FB14 | 出菜餐廳 | NVARCHAR | 6 | F | M-POS，參照 TBPOS |
| FB15 | 庫存成本 | NUMERIC | 25,4 | - | IVCOST |
| FB16 | 人工成本 | NUMERIC | 25,4 | - | LBCOST |
| FB17 | 市場成本 | NUMERIC | 25,4 | - | MKCOST |
| FB18 | 標準成本 | NUMERIC | 25,4 | - | STD COST |
| FB19 | 利潤設定% | NUMERIC | 25,4 | - | PROFIT% |
| FB20 | 英文菜單 | NVARCHAR | 50 | - | ENGLISH NAME |
| FB21 | 套餐菜單 | NVARCHAR | 1 | - | SETMENU，'*':套餐，' ':促銷餐TBSI |
| FB22 | 客數管制 | NVARCHAR | 1 | - | SOLDOUT，'*':停賣，'N':不顯示 |
| FB23 | 廚房指令 | NVARCHAR | 300 | - | K/C |
| FB24 | 口味類別 | NVARCHAR | 20 | F | FLAVOR MAX，參照 HTFV |
| FB25 | KOT印表機 | NVARCHAR | 20 | F | PRINTERID，參照 HTPC（空白不印KOT） |
| FB26 | 餐飲類別 | NVARCHAR | 6 | F | FBTYPE，參照 TBFB |

## 資料關聯說明

### 資料表間關聯

- 與 TBPOS 資料表的關聯:
  - HTFB.FB01 = TBPOS.POS代碼 (餐廳代碼)
  - HTFB.FB14 = TBPOS.POS代碼 (出菜餐廳)
  - 關聯類型: 多對一 (多筆菜單對應一個餐廳)

- 與 TBUN 資料表的關聯:
  - HTFB.FB05 = TBUN.UN代碼
  - 關聯類型: 多對一 (多筆菜單對應一個單位)

- 與 HTCA 資料表的關聯:
  - HTFB.FB06 = HTCA.CA代碼
  - 關聯類型: 多對一 (多筆菜單對應一個科目)

- 與 TBDP 資料表的關聯:
  - HTFB.FB09 = TBDP.DP代碼
  - 關聯類型: 多對一 (多筆菜單對應一個廚房)

- 與 HTFV 資料表的關聯:
  - HTFB.FB24 = HTFV.FV代碼
  - 關聯類型: 多對一 (多筆菜單對應一個口味類別)

- 與 HTPC 資料表的關聯:
  - HTFB.FB25 = HTPC.PC代碼
  - 關聯類型: 多對一 (多筆菜單對應一個印表機)

- 與 TBFB 資料表的關聯:
  - HTFB.FB26 = TBFB.FB代碼
  - 關聯類型: 多對一 (多筆菜單對應一個餐飲類別)

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- FB：FB01、FB02
- FBA1：FB01+FB09+FB02

## 變更歷史記錄

| 版本 | 日期 | 修改人 | 變更描述 |
| ---- | ---- | ------ | -------- |
| 1.0  | 2024-06-05 | System | 初始版本建立，格式標準化 |
