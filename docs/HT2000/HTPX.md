# HTPX_餐廳交易明細檔

## 資料表資訊

**系統：** [[HT2000]] 
**資料表代碼：** HTPX  
**中文名稱：** 餐廳交易明細檔  
**主鍵：** PX001, PX002, PX003 (建檔日期, 建檔時間, 建檔工作站)  
**最後更新日期：** 2024-06-05  
**版本：** 1.0

## 資料表說明

本資料表為 HOTEL2000 系統中的餐廳交易明細檔 (Pos Transaction File)，用於記錄餐廳消費的交易明細，包含點餐資訊、價格、付款方式等內容。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱 | 資料型態 | 長度 | 屬性 | 備註說明 |
|----------|----------|----------|------|------|----------|
| PX001 | 建檔日期 | NVARCHAR | 8 | P,M | RDDATE 格式(YYYYMMDD) |
| PX002 | 建檔時間 | NVARCHAR | 8 | P,M | RDTIME |
| PX003 | 建檔工作站 | NVARCHAR | 10 | P,M | RDTERM |
| PX004 | 建檔人 | NVARCHAR | 10 | M | RDCLRK |
| PX005 | 更改日期 | NVARCHAR | 8 | - | MDDATE 格式(YYYYMMDD) |
| PX006 | 更改時間 | NVARCHAR | 8 | - | MDTIME |
| PX007 | 更改工作站 | NVARCHAR | 10 | - | MDTERM |
| PX008 | 更改人 | NVARCHAR | 10 | - | MDCLRK |
| PX01 | 餐廳代碼 | NVARCHAR | 6 | M,F | POS，參照 TBPOS |
| PX02 | 會計日期 | NVARCHAR | 8 | M | HTDATE 格式(YYYYMMDD) |
| PX03 | 班別 | NVARCHAR | 1 | F | SHIFT，參照 HTSH |
| PX031 | 區域 | NVARCHAR | 10 | F | SECTION，參照 TBSC |
| PX04 | 桌號 | NVARCHAR | 10 | M | TABLE (湯屋系統-票號) |
| PX05 | 序號 | NVARCHAR | 2 | M | SQNO |
| PX06 | 帳單名稱 | NVARCHAR | 16 | - | PFNAME |
| PX07 | 科目 | NVARCHAR | 8 | F | ACC#，參照 HTCA |
| PX08 | 數量 | NUMERIC | 25,4 | M | QUANTITY |
| PX09 | 套餐碼 | NVARCHAR | 8 | F | SETMENU，參照 HTFB (for國賓套餐拆帳) |
| PX10 | 金額 | NUMERIC | 25,4 | M | AMOUNT |
| PX11 | 點菜次數 | NUMERIC | 25,4 | - | Order No, For PSM27 |
| PX12 | 原訂價/折扣 | NUMERIC | 25,4 | - | SPRICE, For PSM27 銷售科目 |
| PX13 | 點單號碼 | NVARCHAR | 10 | - | Kot/Iou (湯屋系統-票號) |
| PX14 | 用餐時段 | NVARCHAR | 6 | F | SalesType，參照 TBST |
| PX15 | 備註 | NVARCHAR | 23 | - | REMARKS，詳見表1 |
| PX16 | 應收帳碼 | NVARCHAR | 20 | - | ARNO，詳見表1 |
| PX19 | 房客帳號 | NVARCHAR | 8 | - | HFNO (票券的折讓金額) |
| PX20 | 出菜餐廳 | NVARCHAR | 6 | F | M-POS，參照 TBPOS |
| PX21 | 出菜現況 | NVARCHAR | 1 | - | STATUS，印/下鍋/可出/已出 |
| PX22 | 出菜順序 | NVARCHAR | 2 | - | KOTSQ |
| PX23 | 廚房指令/免稅證證號 | NVARCHAR | 100 | - | for零稅率 |
| PX24 | 口味選擇 | NVARCHAR | 100 | - | FLAVOR，逗號分隔不同口味 |
| PX25 | 菜單代碼 | NVARCHAR | 20 | F | MENU，參照 HTFB |
| PX26 | 付款金額 | NUMERIC | 25,4 | - | Pay Amt., For上海or現金金額 |
| PX27 | VSNO/票號 | NVARCHAR | 16 | - | Ref No, For瓏山林or早餐扣抵(HP001+HP002) |
| PX28 | 業務員代碼 | NVARCHAR | 10 | - | Sales ID, For北投麗禧 |
| PX29 | SAP Flag | NVARCHAR | 1 | - | Sap Flag, For欣葉SAP |
| PX30 | 負項沖銷記號 | NVARCHAR | 1 | - | Net Flag, For欣葉SAP |
| PX31 | 調帳FLAG | NVARCHAR | 1 | - | Custom Flag, For世貿(*:調帳) |
| PX32 | Filler | NVARCHAR | 8 | - | - |
| PX33 | Filler | NVARCHAR | 8 | - | - |
| PX34 | Filler | NVARCHAR | 8 | - | - |
| PX35 | 票種 | NVARCHAR | 10 | - | CPType, For墨攻票券 |
| PX36 | Filler | NVARCHAR | 10 | - | - |
| PX37 | Filler | NVARCHAR | 10 | - | - |
| PX38 | 調帳後金額 | NUMERIC | 25,4 | - | Custom Amt., For世貿 |
| PX39 | 單價 | NUMERIC | 25,4 | - | UniPrice, For君達 |
| PX40 | 傭金 | NUMERIC | 25,4 | - | commission, S100303071 |
| PX41 | 內部備註 | NVARCHAR | 100 | - | Hidden Memo (S030917055) |
| PX42 | 信用卡載具 | NVARCHAR | 50 | - | Encrypt Card No，上海凌網Invoice(券)/termDate(x8)+termTime(x6)+RcpNo(積分)EDC信用卡載具隱碼(50碼加密卡號) |
| PX43 | OutTradeNo | NVARCHAR | 50 | - | 上海非碼OutTradeNo TranCode/Barcode For宜睿票券/大賀票券 |
| PX44 | Filler | NVARCHAR | 50 | - | - |

## 資料關聯說明

### 資料表間關聯

- 與 TBPOS 資料表的關聯:
  - HTPX.PX01 = TBPOS.POS代碼 (餐廳代碼)
  - HTPX.PX20 = TBPOS.POS代碼 (出菜餐廳)
  - 關聯類型: 多對一 (多筆交易明細對應一個餐廳)

- 與 HTSH 資料表的關聯:
  - HTPX.PX03 = HTSH.SH代碼
  - 關聯類型: 多對一 (多筆交易明細對應一個班別)

- 與 TBSC 資料表的關聯:
  - HTPX.PX031 = TBSC.SC代碼
  - 關聯類型: 多對一 (多筆交易明細對應一個區域)

- 與 HTCA 資料表的關聯:
  - HTPX.PX07 = HTCA.CA代碼
  - 關聯類型: 多對一 (多筆交易明細對應一個科目)

- 與 HTFB 資料表的關聯:
  - HTPX.PX09 = HTFB.FB代碼 (套餐碼)
  - HTPX.PX25 = HTFB.FB02 (菜單代碼)
  - 關聯類型: 多對一 (多筆交易明細對應一個菜單)

- 與 TBST 資料表的關聯:
  - HTPX.PX14 = TBST.ST代碼
  - 關聯類型: 多對一 (多筆交易明細對應一個用餐時段)

## 資料表特性說明

以下關鍵組合用於系統內部處理：

- PX：PX001、PX002、PX003
- PXA1：PX01+PX02+PX04+PX05+PX06
- PXA2：PX01+PX02+PX001+PX002
- PXA3：PX01+PX02+PX03+PX001+PX002
- PXA4：PX01+PX02+PX07+PX001+PX007
- PXA5：PX01+PX25
- PXA6：PX01+PX13
- PXA7：PX01+PX07
- PXA8：PX01+PX15

### 特殊欄位說明

- PX30: 整單沖銷時；原帳單負項沖抵的品項需上*號 (PSM68)
- PX38: 帳單內容調整，將調整後的金額寫入該欄位 (世貿)
- PX40: For RWD點餐，點菜當下直接由菜單主檔抓取抽傭率計算後填入，小數點不做四捨五入 (S100303071)
- PX41~PX44: (SRN: S030917054)

### 表格1：特殊欄位對應表

| 功能名稱 | PX13 | PX15 | PX16 | PX19 | PX24 | PX41 |
|----------|------|------|------|------|------|------|
| OpenCode | - | 菜名 | - | - | - | - |
| 訂金 | 訂金單號DP02 | - | 人工單號DP08 | - | - | - |
| 房客簽帳 | - | 旅客姓名 | 房號 | - | - | - |
| 會員簽帳 | - | 會員姓名 | 會員ID | - | - | - |
| 外客簽帳 | - | - | 合約公司代碼 | - | - | - |
| 餐廳轉帳 | - | 轉入/轉出之廳別 | 桌號+序號 | - | - | - |
| 信用卡 | - | - | 信用卡號 | - | - | 授權碼 |
| 禮券(未建主檔的票券) | - | 抵用張數 | 禮券號碼 | - | - | - |
| 票券 | - | 抵用張數 | 票券單價*張數 | 銷售與抵用之差額 | - | 功能說明(空白/溢收/刪除) |
| 儲值卡加值/銷售 | - | 餘額+紅利 | - | - | - | - |
| 折扣 | - | 折扣名稱(自訂折扣為空白) | - | - | 折扣序號 | - |
| 簽帳(LINEPay、街口) | - | - | 交易編號 | - | - | - |

## 變更歷史記錄

| 版本 | 日期 | 修改人 | 變更描述 |
| ---- | ---- | ------ | -------- |
| 1.0  | 2024-06-05 | System | 初始版本建立，格式標準化 |
