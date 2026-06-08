Net.PY 資料表欄位說明-DB

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DB001|Nvarchar|8|8|建檔日期|Add DBte||
||DB002|Nvarchar|9|8|建檔時間|Add Time||
||DB003|Nvarchar|30|12|建檔工作站|Add WS||
||DB004|Nvarchar|30|12|建檔人|Add User||
||DB005|Nvarchar|8|8|更改日期|Save DBte||
||DB006|Nvarchar|9|8|更改時間|Save Time||
||DB007|Nvarchar|30|12|更改工作站|Save WS||
||DB008|Nvarchar|30|12|更改人|Save User||
||DB01|Nvarchar|2|2|類別代碼|Attend Type||
||DB02|Nvarchar|2|2|扣抵代碼|Deduct Type|1:加項 2:減項|
|＊|DB03|Nvarchar|4|4|考勤代碼|Attend Code||
||DB05|Nvarchar|30|10|代碼名稱(C)|Description(C)||
||DB051|Nvarchar|30|10|代碼名稱(E)|Description(E)||
||DB06|Nvarchar|10|6|單位名稱|Unit|設定檔在SINI. Section=Common_NetPY_AttendUnit|
||DB09|Numeric||3|計算倍數|Cal. Times||
||DB10|Nvarchar|1|1|計算公式|Formula||
||DB11|Nvarchar|4|4|薪資代碼|Pay Code|PC.PC01|
||DB60|Nvarchar|10|10|Filler|Filler||
||DB601|Nvarchar|10|10|Filler|Filler||
||DB602|Nvarchar|10|10|Filler|Filler||
||DB61|Nvarchar|8|8|Date Filler|Date Filler||
||DB611|Nvarchar|8|8|Date Filler|Date Filler||
||DB612|Nvarchar|8|8|Date Filler|Date Filler||
||DB62|Nvarchar|1|1|為加班費|Filler|空白/Y|
||DB621|Nvarchar|1|1|為期初核定|Filler|空白/Y|
||DB622|Nvarchar|1|1|Filler|Filler||
||DB63|Numeric|||Filler|Filler||
||DB631|Numeric|||Filler|Filler||
||DB632|Numeric|||Filler|Filler||
||DB64|Nvarchar|50|50|Filler|Filler||
||DB641|Nvarchar|50|50|Filler|Filler||
||DB642|Nvarchar|50|50|Filler|Filler||

P-Key：DB03