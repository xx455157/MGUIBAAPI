Net.PY 資料表欄位說明-DQ

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DQ001|Nvarchar|8|8|建檔日期|Add Date|轉檔日期|
||DQ002|Nvarchar|9|8|建檔時間|Add Time|轉檔時間|
||DQ003|Nvarchar|30|12|建檔工作站|Add WS||
||DQ004|Nvarchar|30|12|建檔人|Add User||
||DQ005|Nvarchar|8|8|更改日期|Save Date||
||DQ006|Nvarchar|9|8|更改時間|Save Time||
||DQ007|Nvarchar|30|12|更改工作站|Save WS||
||DQ008|Nvarchar|30|12|更改人|Save User||
|＊|DQ01|Nvarchar|30|10|機台號碼|Machine ID||
|＊|DQ02|Nmueric||6|序號|Serial No|Auto Assigned by DB|
||DQ03|Nvarchar|10|10|卡號|Card ID||
||DQ04|Nvarchar|2 8|2 8|公司別|Company ID|A01.A0101|
||DQ05|Nvarchar|10|10|員工編號|Employee ID|P3.P301=DQ02 P3.PA06=DQ03|
||DQ06|Nvarchar|1|1|上下班別|On/Off Type|轉入1:上班 2:下班<br>判定A:上班 B:下班|
||DQ07|Nvarchar|12|12|刷卡時間|Clock Time||
||DQ08|Nvarchar|1|1|狀態|Status|Blank: 未處理<br>D:註銷 N:正常 A:異常<br>M:符合|
||DQ09|Nvarchar|8|8|歸屬日期|Schedule Date||
||DQ10|Nvarchar|4|4|歸屬班別|Schedule Shift||
||DQ11|Nvarchar|1|1|刷卡別|Clock Type|Blank:正常<br>1:兼職刷卡<br>2:餐廳刷卡|
||DQ12|Nvarchar|60|20|轉檔檔名|File Name||
||DQ60|Nvarchar|10|10|部門別|Dept||
||DQ601|Nvarchar|10|10|Filler|Filler||
||DQ602|Nvarchar|10|10|Filler|Filler||
||DQ61|Nvarchar|8|8|Date Filler|Date Filler||
||DQ611|Nvarchar|8|8|Date Filler|Date Filler||
||DQ612|Nvarchar|8|8|Date Filler|Date Filler||
||DQ62|Nvarchar|1|1|Filler|Filler||
||DQ621|Nvarchar|1|1|Filler|Filler||
||DQ622|Nvarchar|1|1|Filler|Filler||
||DQ63|Numeric|||Filler|Filler||
||DQ631|Numeric|||Filler|Filler||
||DQ632|Numeric|||Filler|Filler||
||DQ64|Nvarchar|50|50|Filler|Filler||
||DQ641|Nvarchar|50|50|Filler|Filler||
||DQ642|Nvarchar|50|50|Filler|Filler||

P-Key：DQ01 + DQ02

A-Key：DQ02 + DQ03