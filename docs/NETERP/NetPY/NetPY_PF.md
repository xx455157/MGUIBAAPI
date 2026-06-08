Net.PY 資料表欄位說明-PF

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PF001|Nvarchar|8|8|建檔日期|Add Date||
||PF002|Nvarchar|9|8|建檔時間|Add Time||
||PF003|Nvarchar|30|12|建檔工作站|Add WS||
||PF004|Nvarchar|30|12|建檔人|Add User||
||PF005|Nvarchar|8|8|更改日期|Save Date||
||PF006|Nvarchar|9|8|更改時間|Save Time||
||PF007|Nvarchar|30|12|更改工作站|Save WS||
||PF008|Nvarchar|30|12|更改人|Save User||
|*|PF01|Nvarchar|2|8|公司別|Company ID|Ref A01.A0101|
|*|PF04|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|*|PF03|Nvarchar|2|2|發薪期別|Pay Day||
|*|PF02|Nvarchar|6|5|部門別|Department ID|Ref A02.A0201|
|*|PF05|Nvarchar|4|4|支薪代碼|Pay Code|Ref PC.PC01|
||PF06|Numeric||6|支薪金額|Pay Amount|正項為給款，負項為扣款|
||PF07|Nvarchar|Max|30|備註|Remark||
||PF08|Numeric||6|調薪前金額|Amount B.Adjust||
||PF09|Nvarchar|8|8|調薪日期|Adjustment Date||
||PF60|Nvarchar|10|10|Filler|Filler||
||PF601|Nvarchar|10|10|Filler|Filler||
||PF602|Nvarchar|10|10|Filler|Filler||
||PF61|Nvarchar|8|8|Date Filler|Date Filler||
||PF611|Nvarchar|8|8|Date Filler|Date Filler||
||PF612|Nvarchar|8|8|Date Filler|Date Filler||
||PF62|Nvarchar|1|1|Filler|Filler||
||PF621|Nvarchar|1|1|Filler|Filler||
||PF622|Nvarchar|1|1|Filler|Filler||
||PF63|Numeric|||Filler|Filler||
||PF631|Numeric|||Filler|Filler||
||PF632|Numeric|||Filler|Filler||
||PF64|Nvarchar|50|50|Filler|Filler||
||PF641|Nvarchar|50|50|Filler|Filler||
||PF642|Nvarchar|50|50|Filler|Filler||

P-Key：PF01 + PF04 + PF03 + PF02 + PF05