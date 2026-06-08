Net.PY 資料表欄位說明-DY

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DY001|Nvarchar|8|8|建檔日期|Add Date||
||DY002|Nvarchar|9|8|建檔時間|Add Time||
||DY003|Nvarchar|30|12|建檔工作站|Add WS||
||DY004|Nvarchar|30|12|建檔人|Add User||
||DY005|Nvarchar|8|8|更改日期|Save Date||
||DY006|Nvarchar|9|8|更改時間|Save Time||
||DY007|Nvarchar|30|12|更改工作站|Save WS||
||DY008|Nvarchar|30|12|更改人|Save User||
|＊|DY01|Nvarchar|2|6|公司別|Company||
|＊|DY02|Nvarchar|10|10|原主辦人|Manager ID|Ref PA(PA01=DY61 and PA06=DY02)|
|＊|DY03|Nvarchar|6|8|流程編號|Flow No.|Ref DU(DU01=DV01 and DU02=DV05)|
||DY04|Nvarchar|10|10|新主辦人編號|Agent ID|Ref PA(PA01=DY611 and PA06=DY04)|
||DY05|Nvarchar|8|8|生效日期|Start Date||
||DY06|Nvarchar|9|8|生效時間|Start Time|Input ,填滿後|
||DY60|Nvarchar|10||Filler|||
||DY601|Nvarchar|10||Filler|||
||DY602|Nvarchar|10||Filler|||
||DY61|Nvarchar|8|6|公司別(原主辦人)|||
||DY611|Nvarchar|8|6|公司別(新主辦人)|||
||DY612|Nvarchar|8||Filler|||
||DY62|Nvarchar|1||是否執行過||(Y:是)|
|y|DY621|Nvarchar|1||一般性通知|||
||DY622|Nvarchar|1||急件通知||(空白:Mail/M:手機/A:全部/N:不通知)|
||DY63|Numeric|13.4||Filler||(空白:Mail/M:手機/A:全部/N:不通知)|
||DY631|Numeric|13.4||Filler|||
||DY632|Numeric|13.4||Filler|||
||DY64|Nvarchar|50||Flag|||
||DY641|Nvarchar|50||Flag|||
||DY642|Nvarchar|50||Flag|||

P-Key：DY01 + DY02 + DY03