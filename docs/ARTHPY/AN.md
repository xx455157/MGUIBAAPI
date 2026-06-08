AN  上下班時間控制檔

**Table** **Field** **DataType** **Length** **Description**

AN AN001 Text 8 建檔日期

AN002 Text 8 建檔時間

AN003 Text 10 建檔工作站

AN004 Text 10 建檔人

AN005 Text 8 更改日期

AN006 Text 8 更改時間

AN007 Text 10 更改工作站

AN008 Text 10 更改人

AN01 Text 4 班別代號

AN02 Text 1 1: 上班  2: 下班

AN03 Text 1 容許時間（起 : 0: 當日 ,1: 隔日 ,2: 前日）

AN04 Text 4 容許時間（起 :HHMM ）

AN05 Text 1 容許時間（迄 :0: 當日 ,1: 隔日 ,2: 前日）

AN06 Text 4 容許時間（迄 :HHMM ）

AN07 Text 4 標準時間

AN08 Text 1 0: 當日  1: 隔日   2: 前日

AN09 Text 10 Filler

AN10 Text 8 上班標準時間

AN11 Text 8 Date Filler

AN12 Text 8 Date Filler

AN13 Double Filler

AN14 Double Filler

AN15 Double Filler

AN16 Text 1 Filler

AN17 Text 1 Filler

AN18 Text 1 Filler

AN: AN01+AN02+AN07+AN08 (P & U)

* 在容許時間內之刷卡算正常
* 在犯規時間內之刷卡可算正常 ,  或算異常
* 在以上時間外之刷卡算異常
