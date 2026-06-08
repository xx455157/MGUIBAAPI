Net.PY 資料表欄位說明-PU

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PU001|Nvarchar|8|8|建檔日期|Add Date||
||PU002|Nvarchar|9|8|建檔時間|Add Time||
||PU003|Nvarchar|30|12|建檔工作站|Add WS||
||PU004|Nvarchar|30|12|建檔人|Add User||
||PU005|Nvarchar|8|8|更改日期|Save Date||
||PU006|Nvarchar|9|8|更改時間|Save Time||
||PU007|Nvarchar|30|12|更改工作站|Save WS||
||PU008|Nvarchar|30|12|更改人|Save User||
|＊|PU01|Nvarchar|20|10|眷屬身份證字號|RelativeSocial ID|PA.PA05|
||PU02|Nvarchar|40|10|中文姓名|Name(C)||
||PU03|Nvarchar|40|20|英文姓名|Name(E)||
||PU04|Nvarchar|8|8|出生日期|Birthday||
|＊|PU05|Nvarchar|20|10|員工身份證字號|EmployeeSocial ID|PA.PA05|
||PU06|Nvarchar|1|4|關係|Relationship||
||PU07|Nvarchar|8|8|健保加保日|LH. In Date||
||PU08|Nvarchar|8|8|健保退保日|LH. Out Date||
||PU09|Nvarchar|1|1|投保類別|Healthy Ins.Type|非空白表示有投保|
||PU10|Nvarchar|Max|12|眷屬工作公司|RelativeWorkCompany||
||PU11|Nvarchar|Max|12|眷屬工作職稱|RelativeJobTitle||
||PU12|Nvarchar|Max|12|眷屬職業|RelativeJobType||
||PU13|Nvarchar|20|12|眷屬公司聯絡電話|RelativeCompany Phone||
||PU14|Nvarchar|1|4|性別|Sex|M:男性  F:女性|
||PU15|Nvarchar|1||繳費者|Contributors|Y/N|
||PU16|Nvarchar|Max|8|國籍|Natioanlity||
||PU17|Nvarchar|1||健保加保原因|(New)||
||PU18|Nvarchar|1||健保退保原因|(New)||
||PU19|Nvarchar|8||Filler-Date|||
||PU20|Nvarchar|8||Filler-Date|||
||PU21|Nvarchar|8||Filler-Date|||
||PU22|Nvarchar|10||Filler-Text|||
||PU23|Nvarchar|10||Filler-Text|||
||PU24|Nvarchar|10||Filler-Text|||
||PU25|Numeric|13.4||Filler-Numeric|||
||PU26|Numeric|13.4||Filler-Numeric|||
||PU27|Numeric|13.4||Filler-Numeric|||
||PU28|Nvarchar|Max||Filler-Text|||
||PU29|Nvarchar|Max||Filler-Text|||
||PU30|Nvarchar|Max||Filler-Text|||
|||||||||

P-KEY: PU01 + PU05

重要說明:

本人一定會有一筆PU, PU05=PU01

PA中的健保加退日期全部以PU中為主