Net.PY 資料表欄位說明-PG

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PG001|Nvarchar|8|8|建檔日期|Add Date||
||PG002|Nvarchar|9|8|建檔時間|Add Time||
||PG003|Nvarchar|30|12|建檔工作站|Add WS||
||PG004|Nvarchar|30|12|建檔人|Add User||
||PG005|Nvarchar|8|8|更改日期|Save Date||
||PG006|Nvarchar|9|8|更改時間|Save Time||
||PG007|Nvarchar|30|12|更改工作站|Save WS||
||PG008|Nvarchar|30|12|更改人|Save User||
|*|PG01|Nvarchar|2|2|公司別|Company ID|Ref A01.A0101|
|*|PG02|Nvarchar|8|8|發薪日期|Paid Date||
||PG05|Nvarchar|8|8|計薪起日|Calculate From||
||PG06|Nvarchar|8|8|計薪迄日|Calculate To||
||PG07|Numeric||4|工作天數|Working Days||
||PG08|Numeric||4|休假天數|Off Days||
||PG09|Nvarchar|1|1|關帳代號|Closed|1:開帳 2:關帳|
||PG10|Nvarchar|4|4|所得年度|Income Year||
||PG11|Nvarchar|Max|30|備註|Remark||
||PG60|Nvarchar|10|10|PYtoGL製票號碼|Filler||
||PG601|Nvarchar|10|10|Filler|Filler||
||PG602|Nvarchar|10|10|Filler|Filler||
||PG61|Nvarchar|8|8|PYtoGL公司別|Date Filler||
||PG611|Nvarchar|8|8|Date Filler|Date Filler||
||PG612|Nvarchar|8|8|Date Filler|Date Filler||
||PG62|Nvarchar|1|1|Filler|Filler||
||PG621|Nvarchar|1|1|Filler|Filler||
||PG622|Nvarchar|1|1|Filler|Filler||
||PG63|Numeric|||Filler|Filler||
||PG631|Numeric|||Filler|Filler||
||PG632|Numeric|||Filler|Filler||
||PG64|Nvarchar|50|50|Filler|Filler||
||PG641|Nvarchar|50|50|Filler|Filler||
||PG642|Nvarchar|50|50|Filler|Filler||

P-Key：PG01 + PG02