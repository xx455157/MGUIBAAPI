Net.PY 資料表欄位說明-PV

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PV001|Nvarchar|8|8|建檔日期|Add Date||
||PV002|Nvarchar|9|8|建檔時間|Add Time||
||PV003|Nvarchar|30|12|建檔工作站|Add WS||
||PV004|Nvarchar|30|12|建檔人|Add User||
||PV005|Nvarchar|8|8|更改日期|Save Date||
||PV006|Nvarchar|9|8|更改時間|Save Time||
||PV007|Nvarchar|30|12|更改工作站|Save WS||
||PV008|Nvarchar|30|12|更改人|Save User||
|*|PV01|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|*|PV011|Nvarchar|2|2|記錄區分|Record Type|SINI.Section=Common_NetPY_RecordType，Topic=PS00<br>1:學歷      2:工作經驗<br>3:證照      9:其他記錄|
|*|PV02|Numeric||3|序號|Serial No|Auto assigned by DB|
||PV03|Nvarchar|6|6|記錄代碼|Record ID|PS.PS01|
||PV04|Nvarchar|6|6|有效值代碼|Valid Code|PV03=PSA.PSA01 PV04=PSA.PSA02|
||PV05|Nvarchar|Max|30|說明(C)|Description(C)||
||PV06|Nvarchar|Max|30|說明(E)|Description(E)||
||PV07|Nvarchar|8|8|有效期(起)|Validation S.||
||PV08|Nvarchar|8|8|有效期(迄)|Validation E.||
||PV09|Nvarchar|40|10|級數|Degree||
||PV60|Nvarchar|10|10|Filler|Filler||
||PV601|Nvarchar|10|10|Filler|Filler||
||PV602|Nvarchar|10|10|Filler|Filler||
||PV61|Nvarchar|8|8|Date Filler|Date Filler||
||PV611|Nvarchar|8|8|Date Filler|Date Filler||
||PV612|Nvarchar|8|8|Date Filler|Date Filler||
||PV62|Nvarchar|1|1|Filler|Filler||
||PV621|Nvarchar|1|1|Filler|Filler||
||PV622|Nvarchar|1|1|Filler|Filler||
||PV63|Numeric|||Filler|Filler||
||PV631|Numeric|||Filler|Filler||
||PV632|Numeric|||Filler|Filler||
||PV64|Nvarchar|50|50|Filler|Filler||
||PV641|Nvarchar|50|50|Filler|Filler||
||PV642|Nvarchar|50|50|Filler|Filler||

P-Key：PV01 + PV011+PV02