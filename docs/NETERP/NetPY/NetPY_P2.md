Net.PY 資料表欄位說明-P2

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|P2001|Nvarchar|8|8|建檔日期|Add Date||
||P2002|Nvarchar|9|8|建檔時間|Add Time||
||P2003|Nvarchar|30|12|建檔工作站|Add WS||
||P2004|Nvarchar|30|12|建檔人|Add User||
||P2005|Nvarchar|8|8|更改日期|Save Date||
||P2006|Nvarchar|9|8|更改時間|Save Time||
||P2007|Nvarchar|30|12|更改工作站|Save WS||
||P2008|Nvarchar|30|12|更改人|Save User||
|*|P201|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|*|P202|Nvarchar|2|2|公司別|Company ID|A01.A0101|
|*|P203|Nvarchar|6|6|提撥年月|Salary Month||
||P204|Numeric||6|提撥金額|Retirement Base||
||P205|Numeric||3.2|公司提撥率|Company Pay %||
||P206|Numeric||3.2|個人提撥率|Self Pay %||
||P207|Numeric||6|公司提撥金額|Company Pay||
||P208|Numeric||6|個人提撥金額|Self Pay||
||P209|Numeric||6|實際薪資|Actual Salary||
||P210|Nvarchar|6|6|繳款月份|Pay Month|保留不處理|
||P211|Nvarchar|6|6|部門別|Department ID|A02.A0201|
||P212|Nvarchar|1|1|工作別|Work Type|1:有勞退 2:無勞退|
||P213|Nvarchar|8|8|扣薪日期|Deduct Date||
||P260|Nvarchar|10|10|Filler|Filler||
||P2601|Nvarchar|10|10|Filler|Filler||
||P2602|Nvarchar|10|10|Filler|Filler||
||P261|Nvarchar|8|8|Date Filler|Date Filler||
||P2611|Nvarchar|8|8|Date Filler|Date Filler||
||P2612|Nvarchar|8|8|Date Filler|Date Filler||
||P262|Nvarchar|1|1|Filler|Filler||
||P2621|Nvarchar|1|1|Filler|Filler||
||P2622|Nvarchar|1|1|Filler|Filler||
||P263|Numeric|||Filler|Filler||
||P2631|Numeric|||Filler|Filler||
||P2632|Numeric|||Filler|Filler||
||P264|Nvarchar|50|50|Filler|Filler||
||P2641|Nvarchar|50|50|Filler|Filler||
||P2642|Nvarchar|50|50|Filler|Filler||

P-Key：P201 + P202 + P203