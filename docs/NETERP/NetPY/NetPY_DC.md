Net.PY 資料表欄位說明-DC

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DC001|Nvarchar|8|8|建檔日期|Add Date||
||DC002|Nvarchar|9|8|建檔時間|Add Time||
||DC003|Nvarchar|30|12|建檔工作站|Add WS||
||DC004|Nvarchar|30|12|建檔人|Add User||
||DC005|Nvarchar|8|8|更改日期|Save Date||
||DC006|Nvarchar|9|8|更改時間|Save Time||
||DC007|Nvarchar|30|12|更改工作站|Save WS||
||DC008|Nvarchar|30|12|更改人|Save User||
|＊|DC01|Nvarchar|4|4|考勤代碼|Attend Code|DB.DB03|
|＊|DC02|Nvarchar|4|4|扣抵代碼|Deduct ID|DB.DB03|
||DC03|Numeric||9(3)v9(4)|扣抵換算比例|Deduct Rate||
||DC04|Numeric||2|優先順序|Display Sequence||
||DC60|Nvarchar|10|10|Filler|Filler||
||DC601|Nvarchar|10|10|Filler|Filler||
||DC602|Nvarchar|10|10|Filler|Filler||
||DC61|Nvarchar|8|8|Date Filler|Date Filler||
||DC611|Nvarchar|8|8|Date Filler|Date Filler||
||DC612|Nvarchar|8|8|Date Filler|Date Filler||
||DC62|Nvarchar|1|1|Filler|Filler||
||DC621|Nvarchar|1|1|Filler|Filler||
||DC622|Nvarchar|1|1|Filler|Filler||
||DC63|Numeric|||Filler|Filler||
||DC631|Numeric|||Filler|Filler||
||DC632|Numeric|||Filler|Filler||
||DC64|Nvarchar|50|50|Filler|Filler||
||DC641|Nvarchar|50|50|Filler|Filler||
||DC642|Nvarchar|50|50|Filler|Filler||

P-Key：DC01 + DC02

A-Key：DC01 + DC04