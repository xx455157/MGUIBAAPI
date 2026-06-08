Net.PY 資料表欄位說明-PZ

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PZ001|Nvarchar|8|8|建檔日期|Add Date||
||PZ002|Nvarchar|9|8|建檔時間|Add Time||
||PZ003|Nvarchar|30|12|建檔工作站|Add WS||
||PZ004|Nvarchar|30|12|建檔人|Add User||
||PZ005|Nvarchar|8|8|更改日期|Save Date||
||PZ006|Nvarchar|9|8|更改時間|Save Time||
||PZ007|Nvarchar|30|12|更改工作站|Save WS||
||PZ008|Nvarchar|30|12|更改人|Save User||
|*|PZ01|Nvarchar|2|2|公司別|Company ID|A01.A0101|
|*|PZ02|Nvarchar|8|8|日期|Date||
|*|PZ03|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|*|PZ04|Nvarchar|4|4|班別|Shift ID||
|*|PZ05|Nvarchar|6|6|部門別|Department ID|A02.A0201|
||PZ06|Nvarchar|10|10|員工編號|Employee ID|P3.P301=PZ01 <br>P3.PA06=PZ06|
||PZ07|Numeric||3.1|實際工時|Act.Work HRs||
||PZ08|Numeric||3.1|給薪工時|Paid Work HRs||
||PZ09|Numeric||3.1|超時工時|Overtime HRs||
||PZ10|Nvarchar|12|12|起時|Start Time||
||PZ11|Nvarchar|12|12|迄時|End Time||
||PZ12|Nvarchar|1|1|假日|||
||PZ13|Nvarchar|Max|30|備註|Remark||
||PZ60|Nvarchar|10|10|Filler|Filler||
||PZ601|Nvarchar|10|10|Filler|Filler||
||PZ602|Nvarchar|10|10|Filler|Filler||
||PZ61|Nvarchar|8|8|Date Filler|Date Filler||
||PZ611|Nvarchar|8|8|Date Filler|Date Filler||
||PZ612|Nvarchar|8|8|Date Filler|Date Filler||
||PZ62|Nvarchar|1|1|Filler|Filler||
||PZ621|Nvarchar|1|1|Filler|Filler||
||PZ622|Nvarchar|1|1|Filler|Filler||
||PZ63|Numeric|||Filler|Filler||
||PZ631|Numeric|||Filler|Filler||
||PZ632|Numeric|||Filler|Filler||
||PZ64|Nvarchar|50|50|Filler|Filler||
||PZ641|Nvarchar|50|50|Filler|Filler||
||PZ642|Nvarchar|50|50|Filler|Filler||

P-Key：PZ01 + PZ02 + PZ03 + PZ04 + PZ05