Net.PY 資料表欄位說明-DL

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DL001|Nvarchar|8|8|建檔日期|Add Date||
||DL002|Nvarchar|9|8|建檔時間|Add Time||
||DL003|Nvarchar|30|12|建檔工作站|Add WS||
||DL004|Nvarchar|30|12|建檔人|Add User||
||DL005|Nvarchar|8|8|更改日期|Save Date||
||DL006|Nvarchar|9|8|更改時間|Save Time||
||DL007|Nvarchar|30|12|更改工作站|Save WS||
||DL008|Nvarchar|30|12|更改人|Save User||
|＊|DL01|Nvarchar|2 8|2 8|公司別|Company ID|A01.A0101|
|＊|DL02|Nvarchar|4|4|班別|Shift ID||
||DL03|Numeric||2|應計工時|Working HRs||
||DL04|Nvarchar|1|1|假日區分<br>Filler||1:國定假日休<br>2:雙週休二日<br>3:全年無休|
||DL05|Nvarchar|1|1|打卡方式|Clock Type|1:準點打卡<br>2:彈性上下班|
||DL06|Nvarchar|4|4|上班時間(1)|Duty Time(1)||
||DL061|Nvarchar|4|4|上班容許(1)<br>時間(起)|Duty Time buffer(1)||
||DL062|Nvarchar|4|4|上班容許(1)<br>時間(迄)|Duty Time buffer(1)||
||DL07|Nvarchar|4|4|下班時間(1)|Off Time(1)||
||DL071|Nvarchar|4|4|下班容許(1)<br>時間(起)|Off Time buffer(1)||
||DL072|Nvarchar|4|4|下班容許(1)<br>時間(迄)|Off Time buffer(1)||
||DL08|Nvarchar|4|4|上班時間(2)|Duty Time(2)||
||DL081|Nvarchar|4|4|上班容許(2)<br>時間(起)|Duty Time buffer(2)||
||DL082|Nvarchar|4|4|上班容許(2)<br>時間(迄)|Duty Time buffer(2)||
||DL09|Nvarchar|4|4|下班時間(2)|Off Time(2)||
||DL091|Nvarchar|4|4|下班容許(2)<br>時間(起)|Off Time buffer(2)||
||DL092|Nvarchar|4|4|下班容許(2)<br>時間(迄)|Off Time buffer||
||DL60|Nvarchar|10|10|Filler|Filler||
||DL601|Nvarchar|10|10|Filler|Filler||
||DL602|Nvarchar|10|10|Filler|Filler||
||DL61|Nvarchar|8|8|Date Filler|Date Filler||
||DL611|Nvarchar|8|8|Date Filler|Date Filler||
||DL612|Nvarchar|8|8|Date Filler|Date Filler||
||DL62|Nvarchar|1|1|Filler|Filler||
||DL621|Nvarchar|1|1|Filler|Filler||
||DL622|Nvarchar|1|1|Filler|Filler||
||DL63|Numeric|||Filler|Filler||
||DL631|Numeric|||Filler|Filler||
||DL632|Numeric|||Filler|Filler||
||DL64|Nvarchar|50|50|Filler|Filler||
||DL641|Nvarchar|50|50|Filler|Filler||
||DL642|Nvarchar|50|50|Filler|Filler||

P-Key：DL01 + DL02