Net.PY 資料表欄位說明-PE

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PE001|Nvarchar|8|8|建檔日期|Add Date||
||PE002|Nvarchar|9|8|建檔時間|Add Time||
||PE003|Nvarchar|30|12|建檔工作站|Add WS||
||PE004|Nvarchar|30|12|建檔人|Add User||
||PE005|Nvarchar|8|8|更改日期|Save Date||
||PE006|Nvarchar|9|8|更改時間|Save Time||
||PE007|Nvarchar|30|12|更改工作站|Save WS||
||PE008|Nvarchar|30|12|更改人|Save User||
|*|PE01|Nvarchar|2|8|公司別|Company ID|Ref A01.A0101|
|*|PE02|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|*|PE0456|Nvarchar|8|8|發薪日期|Pay Date|PE01=PG.PG01 PE0456=PG.PG02|
||PE03|Nvarchar|20|10|支票號碼|Check No.|此欄位保留為Filler，不再使用|
||PE07|Nvarchar|1|1|領現方式|Pay Method|1:Cash 3:Others|
||PE08|Nvarchar|40|10|轉帳銀行(A)|Bank ID(A)||
||PE09|Nvarchar|20|15|銀行帳號(A)|Bank Account(A)||
||PE081|Nvarchar|40|10|轉帳銀行(B)|Bank ID(B)||
||PE091|Nvarchar|20|15|銀行帳號(B)|Bank Account(B)||
||PE10|Numeric|||領現金額|Cash Amount||
||PE101|Numeric|||轉帳金額(A)|T/T Amount(A)||
||PE102|Numeric|||轉帳金額(B)|T/T Amount(B)||
||PE60|Nvarchar|10|10|Filler|Filler||
||PE601|Nvarchar|10|10|Filler|Filler||
||PE602|Nvarchar|10|10|Filler|Filler||
||PE61|Nvarchar|8|8|Date Filler|Date Filler||
||PE611|Nvarchar|8|8|Date Filler|Date Filler||
||PE612|Nvarchar|8|8|Date Filler|Date Filler||
||PE62|Nvarchar|1|1|Filler|Filler||
||PE621|Nvarchar|1|1|Filler|Filler||
||PE622|Nvarchar|1|1|Filler|Filler||
||PE63|Numeric|||Filler|Filler||
||PE631|Numeric|||Filler|Filler||
||PE632|Numeric|||Filler|Filler||
||PE64|Nvarchar|50|50|Filler|Filler||
||PE641|Nvarchar|50|50|Filler|Filler||
||PE642|Nvarchar|50|50|Filler|Filler||

P-Key：PE01 + PE02+ PE0456