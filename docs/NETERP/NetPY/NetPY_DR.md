Net.PY 資料表欄位說明-DR

| # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|Chinese<br>Name|English<br>Name|# Description|
|##|DR001|Nvarchar|8|建檔日期|Add Date||
||DR002|Nvarchar|9|建檔時間|Add Time||
||DR003|Nvarchar|30|建檔工作站|Add WS||
||DR004|Nvarchar|30|建檔人|Add User||
||DR005|Nvarchar|8|更改日期|Save Date||
||DR006|Nvarchar|9|更改時間|Save Time||
||DR007|Nvarchar|30|更改工作站|Save WS||
||DR008|Nvarchar|30|更改人|Save User||
|＊|DR01|Nvarchar|10|卡號|Card ID||
||DR02|Nvarchar|2 8|公司別|Company ID|A01.A0101|
||DR03|Nvarchar|20|身分證字號|||
||DR04|Nvarchar|10|員工編號|Employee ID|P3.P301=DR02 P3.PA06=DR03|
||DR05|Nvarchar|12|有效期(起)|Validity Period S.||
||DR06|Nvarchar|12|有效期(迄)|Validity Period E.||
||DR07|Nvarchar|Max|備註|Remark||
||DR60|Nvarchar|10|Filler|Filler||
||DR601|Nvarchar|10|Filler|Filler||
||DR602|Nvarchar|10|Filler|Filler||
||DR61|Nvarchar|8|Date Filler|Date Filler||
||DR611|Nvarchar|8|Date Filler|Date Filler||
||DR612|Nvarchar|8|Date Filler|Date Filler||
||DR62|Nvarchar|1|Filler|Filler||
||DR621|Nvarchar|1|Filler|Filler||
||DR622|Nvarchar|1|Filler|Filler||
||DR63|Numeric||Filler|Filler||
||DR631|Numeric||Filler|Filler||
||DR632|Numeric||Filler|Filler||
||DR64|Nvarchar|50|Filler|Filler||
||DR641|Nvarchar|50|Filler|Filler||
||DR642|Nvarchar|50|Filler|Filler||

P-Key：DR01

A-Key：DR02 + DR03