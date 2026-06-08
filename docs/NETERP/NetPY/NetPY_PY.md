Net.PY 資料表欄位說明-PY

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PY001|Nvarchar|8|8|建檔日期|Add Date||
||PY002|Nvarchar|9|8|建檔時間|Add Time||
||PY003|Nvarchar|30|12|建檔工作站|Add WS||
||PY004|Nvarchar|30|12|建檔人|Add User||
||PY005|Nvarchar|8|8|更改日期|Save Date||
||PY006|Nvarchar|9|8|更改時間|Save Time||
||PY007|Nvarchar|30|12|更改工作站|Save WS||
||PY008|Nvarchar|30|12|更改人|Save User||
|*|PY01|Nvarchar|2|2|公司別|Company ID|A01.A0101|
|*|PY02|Nvarchar|10|10|員工編號|Employee ID|P3.P301=PY01 <br>P3.PA06=PY02|
|*|PY03|Nvarchar|4|4|班別|Shift ID|空白表示基本設定|
||PY04|Numeric||4|時薪|HR Rate||
||PY05|Numeric||4|超時時薪|Overtime Rate||
||PY06|Numeric||6|津貼|Allowance||
||PY07|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
||PY60|Nvarchar|10|10|Filler|Filler||
||PY601|Nvarchar|10|10|Filler|Filler||
||PY602|Nvarchar|10|10|Filler|Filler||
||PY61|Nvarchar|8|8|Date Filler|Date Filler||
||PY611|Nvarchar|8|8|Date Filler|Date Filler||
||PY612|Nvarchar|8|8|Date Filler|Date Filler||
||PY62|Nvarchar|1|1|Filler|Filler||
||PY621|Nvarchar|1|1|Filler|Filler||
||PY622|Nvarchar|1|1|Filler|Filler||
||PY63|Numeric|||Filler|Filler||
||PY631|Numeric|||Filler|Filler||
||PY632|Numeric|||Filler|Filler||
||PY64|Nvarchar|50|50|Filler|Filler||
||PY641|Nvarchar|50|50|Filler|Filler||
||PY642|Nvarchar|50|50|Filler|Filler||

P-Key：PY01 + PY02 + PY03