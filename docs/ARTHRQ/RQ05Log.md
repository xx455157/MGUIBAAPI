# page_43

Table FieldName DataType Length Description

=============================================================

RQ05Log TransState Text   1   異動狀態  (D: Delete / U: Update)

TransDate Text   8  異動日期

TransTime Text   9   異動時間

TransWSID Text   10   異動機器名稱

TransUserID Text   10   異動人登錄帳號

P TransUIID GUID GUID

RQ 0 5 001 Text   8  建檔日期

RQ 0 5 00 2 Text 8 建檔時間

RQ 0 5 00 3 Text    10 建檔工作站

RQ 0 5 004 Text    10 建檔人

RQ 0 5 005 Text   8  更改日期

RQ 0 5 006 Text 8 更改時間

RQ 0 5 007  Text    10 更改工作站

RQ 0 5 008  Text    10 更改人

RQ 0 5 0 1   Text      2 公司別

RQ 0 5 0 2 Text     10 請購單號

RQ 0 5 0 3 Double     15 簽核順序序號

RQ 0 5 0 4 Text     50 目前核准人員工編號 ( 聯合請購 : 應核准人 )

RQ 0 5 0 5 Text     2 0 目前核准人員工姓名

RQ 0 506 Text      1 簽核結果

(1: 未處理  2: 簽核中  3: 退回  4: 已核准  5: 會簽 )

RQ 0 5 0 7 Text      8 簽核日期

RQ 0 5 0 8 Text    255 備註

RQ 0 5 0 9  Text     4 0 退件備註

RQ 0 510  Text     4 0 職稱 ( 取 20 碼使用 )

RQ 0 511  Text     4 0 Filler

RQ 0 512  Text     1 0 原核准人員工編號 ( 聯合請購 : 代簽人 )

RQ 0 513  Text     1 0 簽核編號

RQ 0 514  Text     1 0 **待變更的應簽核人**

RQ 0 515 Text      8 是否簽核(空白:是,N:不簽核 ,C:取消不需簽核 )

RQ 0 516   Text      8 收單日期(聯合請購使用)

RQ 0 517 Text      8 收單時間(聯合請購使用)

RQ 0 518 Text      1 會簽單位 (Y / N( 空白 ))

RQ 0 519 Text      1 發送 Mail(Y/N)

RQ 0 520 Text      1 鎖單 (*/ 空白 )

RQ 0 521 Double    15 授權金額(聯合請購使用)

RQ 0 522 Double    15 Double

RQ 0 523 Double    15 Double

RQ0 5Log : TransUIID

備註：

- .NET 聯合採購使用
- 資料來源有二： Update RQ05 的異動是透過 RQ05 Trigger 寫入；刪除 RQ05 則透過程式呼叫 Storeprocedure 寫入，因為在 Trigger 中取不到 Client 機器名稱與 UserID

