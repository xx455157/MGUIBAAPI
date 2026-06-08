Net.PY 資料表欄位說明-P3

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|P3001|Nvarchar|8|8|建檔日期|Add Date||
||P3002|Nvarchar|9|8|建檔時間|Add Time||
||P3003|Nvarchar|30|12|建檔工作站|Add WS||
||P3004|Nvarchar|30|12|建檔人|Add User||
||P3005|Nvarchar|8|8|更改日期|Save Date||
||P3006|Nvarchar|9|8|更改時間|Save Time||
||P3007|Nvarchar|30|12|更改工作站|Save WS||
||P3008|Nvarchar|30|12|更改人|Save User||
|*|P301|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|*|P302|Nvarchar|2|2|公司別|Company ID|A01.A0101|
||P303|Nvarchar|1|1|適用勞基法|Retirement Law|Y/N|
||P304|Nvarchar|1|1|制度別|Retirement Type|1:新制 2:舊制 3:暫不選擇|
||P305|Nvarchar|8|8|轉換日期|Change Date||
||P306|Numeric||3.1|轉換前年資|Work Years before Change||
||P307|Numeric||8|提繳工資|Retirement Salary||
||P308|Numeric||3.2|公司提撥率|Company Pay %||
||P309|Numeric||3.2|個人提撥率|Self Pay %||
||P310|Nvarchar|8|8|申報日期|Declare Date||
||P311|Nvarchar|8|8|停止日期|End Date||
||P312|Nvarchar|20|10|卡號|Card No||
||P3131|Nvarchar|12|12|有效起時|Validate Time S.||
||P3132|Nvarchar|12|12|有效迄時|Validate Time E.||
||P314|Nvarchar|1|1|所得稅扣抵方式|Tax Deduct Type||
||P315|Nvarchar|8|8|試用截止日|Probation Date||
||P360|Nvarchar|10|10|Filler|Filler||
||P3601|Nvarchar|10|10|Filler|Filler||
||P3602|Nvarchar|10|10|Filler|Filler||
||P361|Nvarchar|8|8|Date Filler|Date Filler||
||P3611|Nvarchar|8|8|Date Filler|Date Filler||
||P3612|Nvarchar|8|8|Date Filler|Date Filler||
||P362|Nvarchar|1|1|勞保特殊身分別|||
||P3621|Nvarchar|1|1|勞基法特殊身分別|||
||P3622|Nvarchar|1|1|Filler|Filler||
||P363|Numeric|||Filler|Filler||
||P3631|Numeric|||Filler|Filler||
||P3632|Numeric|||Filler|Filler||
||P364|Nvarchar|50|50|Filler|Filler||
||P3641|Nvarchar|50|50|Filler|Filler||
||P3642|Nvarchar|50|50|Filler|Filler||
||PA016|Nvarchar|10|0|固定班別|Fix Shift||
||PA017|Nvarchar|10|0|時薪|Hour Rate||
||PA02|Nvarchar|6|6|部門別|Dept. ID||
||PA06|Nvarchar|10|8|員工代號|Employee ID||
||PA07|Nvarchar|10|10|歸屬員工代號|Employee Master||
||PA151|Nvarchar|40|10|轉帳銀行(A)|Bank ID(A)||
||PA152|Nvarchar|40|10|轉帳銀行(B)|Bank ID(B)||
||PA161|Nvarchar|20|15|銀行帳號(A)|Bank Account(A)||
||PA162|Nvarchar|20|15|銀行帳號(B)|Bank Account(B)||
||PA17|Nvarchar|8|8|勞保加保日|LI. In Date||
||PA18|Numeric||8|勞保投保金額|LI. Amount||
||PA19|Nvarchar|8|8|勞保退保日|LI. Out Date||
||PA20|Nvarchar|8|8|到職日期|On Board Date||
||PA21|Nvarchar|8|15|離職日期|Leave Date||
||PA25|Nvarchar|1|8|職災類別|Occupation Calamity|1及空白:勞保含職災<br>2:只保職災|
||PA26|Nvarchar|1|6|發薪別|Pay Type|1：月薪2：日薪3：計件|
||PA27|Nvarchar|1|4|兼職|Part-Time|Y/N|
||PA30|Numeric||8|差異年資|Differential Years||
||PA31|Nvarchar|3|3|職等代號|Level ID||
||PA32|Nvarchar|3|3|職務代號|Job ID||
||PA33|Nvarchar|2|4|薪號|Salary Code||
||PA34|Nvarchar|2|4|級距|Salary Level||
||PA37|Nvarchar|1|8|留職停薪|Suspend|(Y/N)|
||PA40|Nvarchar|1|8|員工類別|Employee Type|1:正職,<br>2:兼職<br>3:工讀生,<br>4:實習生|
||PA41|Nvarchar|10|8|考勤種類|Attend Type|1:正常  2:免考勤|

P-Key：P301 + P302 

A-Key：P302 + PA06