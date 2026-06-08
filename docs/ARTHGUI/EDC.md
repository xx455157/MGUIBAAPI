# page_173

金旭共用系統檔案關連

EDC 介接

Key Field Description Type   Length    Remarks

P EDC01 電腦名稱 X          30

EDC 0 2 建檔日期 X 8

EDC 0 3 建檔時間 X 8

EDC 0 4 廳別 X 10

EDC0 5 使用者帳號 X 30

EDC0 6 格式 X 4 EDC格式代碼

EDC0 7 目前狀態 X 1 **空白=未處理；1=處理中；2=處理完畢** **； **3=****處理失敗**

EDC0 8 傳入內容 X Max EDC傳入內容

EDC0 9 回傳內容 X Max EDC回傳內容

EDC 10 F iller X 1

**** EDC1 1 Filler X 1

EDC1 2 Filler X 1

EDC13 卡別 X 10 0    = 一般信用卡  1    = 簽帳金融卡2    = 美國運通卡(AE卡)    3    = 銀聯卡4    = 大來卡5    = 電子票證(悠遊卡)6    = 電子票證(一卡通)7    = 電子票證(iCash)8    = 電子票證(HappyCash)9    = Discover卡10   = Smart Pay11   = 掃碼支付(街口、LinePay...)12   = Taiwan Pay13   =Pi拍錢包14 =  街口15  = LinePay

EDC14 會計日期 X 10 會計日期(PF0 2)

EDC15 Filler X 10

EDC 16 卡號 X 50

EDC 17 帳單號碼 X 50     帳單號碼(PF05)

EDC 18 Filler X 50

EDC19  Filler X 8

EDC20 Fiiler X 8

EDC21 Filler X 8

EDC 22 金額 N

EDC23 Filler N

EDC24 Filler N

EDC25 處理結果 X Max     成功或失敗訊息

EDC26 Filler X Max

EDC27 Filler X Max
