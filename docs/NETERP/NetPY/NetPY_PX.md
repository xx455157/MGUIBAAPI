Net.PY 資料表欄位說明-PX

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PX001|Nvarchar|8|8|建檔日期|Add Date||
||PX002|Nvarchar|9|8|建檔時間|Add Time||
||PX003|Nvarchar|30|12|建檔工作站|Add WS||
||PX004|Nvarchar|30|12|建檔人|Add User||
||PX005|Nvarchar|8|8|更改日期|Save Date||
||PX006|Nvarchar|9|8|更改時間|Save Time||
||PX007|Nvarchar|30|12|更改工作站|Save WS||
||PX008|Nvarchar|30|12|更改人|Save User||
|*|PX01|Nvarchar|2|6|公司別|CompanyCode|A01.A0101|
|*|PX02|Numeric|20|10|身分證字號|Social ID|PA.PA05|
|*|PX03|Nvarchar|8|8|發薪日期|Pay Date||
|*|PX04|Nvarchar|20|10|眷屬身分證字號|RelativeSocialID||
||PX05|Numeric||6|保費金額|InsuranceAmount||
||PX06|Nvarchar|1|4|投保類別|InsuranceType||
||PX07|Nvarchar|4|4|歸屬年度|BelongYear||
||PX08|Nvarchar|4|4|支薪代碼|Pay Code|此支薪代碼為PB中的健保費支薪代碼(PB12)|
||PX60|Nvarchar|10|10|Filler|Filler||
||PX601|Nvarchar|10|10|Filler|Filler||
||PX602|Nvarchar|10|10|Filler|Filler||
||PX61|Nvarchar|8|8|Date Filler|Date Filler||
||PX611|Nvarchar|8|8|Date Filler|Date Filler||
||PX612|Nvarchar|8|8|Date Filler|Date Filler||
||PX62|Nvarchar|1|1|Filler|Filler||
||PX621|Nvarchar|1|1|Filler|Filler||
||PX622|Nvarchar|1|1|Filler|Filler||
||PX63|Numeric|||Filler|Filler||
||PX631|Numeric|||Filler|Filler||
||PX632|Numeric|||Filler|Filler||
||PX64|Nvarchar|50|50|Filler|Filler||
||PX641|Nvarchar|50|50|Filler|Filler||
||PX642|Nvarchar|50|50|Filler|Filler||

P-Key：PX01 + PX02 + PX03 + PX04