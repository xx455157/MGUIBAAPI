# page_175

金旭共用系統檔案關連

EDC Log

Key Field Description Type   Length    Remarks

Act i on 動作狀態 X 1 **A=** **新增；** **U=** **修改；** **D=** **刪除**

DateTime 紀錄日期時間 X 25

P EDC00 自動編號序號 N

EDC01 電腦名稱 X 30

EDC 0 2 建檔日期 X 8

EDC 0 3 建檔時間 X 8

EDC 0 4 廳別 X 10

EDC0 5 使用者帳號 X 30

EDC0 6 格式 X 4 EDC 格式代碼

EDC0 7 目前狀態 X 1 **空白** **=** **未處理；** **1=** **處理中；** **2=** **處理完畢**

EDC0 8 傳入內容 X Max EDC 傳入內容

EDC0 9 回傳內容 X Max EDC 回傳內容

EDC 10 F iller X 1

**** EDC1 1 Filler X 1

EDC1 2 Filler X 1

EDC1 3 卡別 X 10

EDC1 4 Filler X 10

EDC 15 Filler X 10

EDC 16 卡號 X 50

EDC 17 Filler X 50

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

PK_ EDC L ： EDC 00


