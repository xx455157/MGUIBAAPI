# page_167

**銷售分析統計主檔**

Table FieldName     DataType   Length   Description

=============================================================

SB  SB01            Text               1   類別

( O: 訂單 P: 採購 S: 銷貨 R: 進貨 )

SB02            Text               2   方式代碼 (spread Row)

SB031           Text              20   對象一 ID

SB032           Text              20   對象二 ID

SB033           Text              20   對象三 ID

SB034           Text              20   對象四 ID

SB035           Text              20   對象五 ID

SB036           Text              20   對象六 I D

SB037           Text              20   對象七 ID

SB04            Text               8   日期

(1, 年 yyyy0000

2, 季 yyyymm9903,06,09,12

3, 月 yyyymm00

4, 日 yyyymmdd)

SB051           Text              40   對象一名稱

SB052           Text              40   對象二名稱

SB053           Text              40   對象三名稱

SB054           Text              40   對象四名稱

SB055           Text              40   對象五名稱

SB056           Text              40   對象六名稱

SB057           Text              40   對象七名稱

SB06            Text               1   統計幣別

SB071           Currency    目標金額 (NT)

54089305845810過帳後產生00過帳後產生  SB072           Currency    目標金額 (US)

過帳後產生

過帳後產生

1447800579120000  SB08            Currency    目標數量

SB091           Currency    實績金額 (NT)

SB092           Currency    實績金額 (US)

SB10            Currency    實績數量

SB11            Currency    實績成本

1447800655320000  SB121           Currency    目標毛利金額 (NT)

SB122           Currency    目標毛利金額 (US)

SB13            Currency    目標毛利數量

SB141           Currency   FreeField

SB142           Currency   FreeField

SB15            Currency   FreeField

SB161           Currency   FreeField

SB162           Currency   FreeField

SB17            Currency   FreeField

SB:SB01+SB02+SB031+SB032+SB033+SB034+SB035+SB036+SB037+SB04

金旭共用系統檔案關連

**系統參數設定資料表** **(Trigger** **串** **.Net** **部份資料** **)**

Table FieldName     DataType   Length   Description

=============================================================

SINI    SECTION     Text     298 50 SECTION

TOPIC     Text 140 50 TOPIC

TOPICVALUE Text Max 50 TOPICVALUE

LANGUAGE     Text 1 0 TOPIC

INI:SECTION+TOPIC + LANGUAGE


