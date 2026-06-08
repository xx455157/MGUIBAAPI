Net.PY 資料表欄位說明-D2

| # | # | # | # | # | # |
|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|Chinese<br>Name|# Description|
|##|D2001|Nvarchar|8|建檔日期||
||D2002|Nvarchar|9|建檔時間||
||D2003|Nvarchar|30|建檔工作站||
||D2004|Nvarchar|30|建檔人||
||D2005|Nvarchar|8|更改日期||
||D2006|Nvarchar|9|更改時間||
||D2007|Nvarchar|30|更改工作站||
||D2008|Nvarchar|30|更改人||
|＊|D201|Nvarchar|2 8|公司別||
|＊|D202|Nvarchar|4|考勤代碼||
||D203|Nvarchar|1|給薪／扣薪|1.給薪;2.扣薪|
||D204|Nvarchar|1|計算方式|1.發薪期別<br>2.固定單位金額<br>3.個人基本單位時薪<br>4.指定實際已發薪日期|
||D205|Numeric|13.4|固定金額|計算方式為2時才填入|
||D206|Nvarchar|2|期別|計算方式為1時才填入|
||D207|Nvarchar|2|Filler 計算週期-起日||
||D208|Nvarchar|2|Filler 計算週期-迄日||
||D209|Nvarchar|1|追加設定|1.依數量2.依次數3.無|
||D210|Numeric|13.4|不計薪數量或次數||
||D211|Numeric|13.4|加倍追加之累積量||
||D212|Numeric|13.4|追加倍數||
||D213|Nvarchar|1|前期累計納入計算|Y/N|
||D214|Nvarchar|1|數量來源|1:考勤別/2:扣抵別|
||D215|Nvarchar|1|Filler-Flag||
||D216|Nvarchar|1|Filler-Flag||
||D217|Nvarchar|1|Filler-Flag||
||D218|Nvarchar|8|Filler-Date||
||D219|Nvarchar|8|Filler-Date||
||D220|Nvarchar|8|Filler-Date||
||D221|Nvarchar|10|Filler-Text||
||D222|Nvarchar|10|Filler-Text||
||D223|Nvarchar|10|Filler-Text||
||D224|Numeric|13.4|Filler-Numeric||
||D225|Numeric|13.4|Filler-Numeric||
||D226|Numeric|13.4|Filler-Numeric||
||D227|Nvarchar|Max|Filler-Text||
||D228|Nvarchar|Max|Filler-Text||
||D229|Nvarchar|Max|Filler-Text||

P-Key：D201+D202