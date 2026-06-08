Net.PY 資料表欄位說明-DV

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# DDVa<br># Type|# DDVa<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DV001|Nvarchar|8|8|建檔日期|Add DDVe||
||DV002|Nvarchar|9|8|建檔時間|Add Time||
||DV003|Nvarchar|30|12|建檔工作站|Add WS||
||DV004|Nvarchar|30|12|建檔人|Add User||
||DV005|Nvarchar|8|8|更改日期|SDVe DDVe||
||DV006|Nvarchar|9|8|更改時間|SDVe Time||
||DV007|Nvarchar|30|12|更改工作站|SDVe WS||
||DV008|Nvarchar|30|12|更改人|SDVe User||
|＊|DV01|Nvarchar|2|6|公司別|Company||
|＊|DV02|Nvarchar|10|10|原主辦人編號|Agent for|Ref PA(PA01=DV61 and PA06=DV02)|
|＊|DV03|Nvarchar|8|8|代理起日|Start Date||
|＊|DV031|Nvarchar|9|8|代理起時間|Start Time|Input ,填滿後|
|＊|DV04|Nvarchar|8|8|代理迄日|End Date||
|＊|DV041|Nvarchar|9|8|代理迄時間|End Time|Input ,填滿後|
|＊|DV05|Nvarchar|6|8|代理流程編號|Flow Agent|Ref DU(DU01=DV01 and DU02=DV05)<br>若此處空白，表示套用全部流程|
||DV06|Nvarchar|10|10|代理人編號|Agent ID|Ref PA(PA01=DV611 and PA06=DV06)|
||DV60|Nvarchar|12||來源請假單號|||
||DV601|Nvarchar|10||Filler|||
||DV602|Nvarchar|10||Filler|||
||DV61|Nvarchar|8|6|公司別(原主辦人)|||
||DV611|Nvarchar|8|6|公司別(代理人)|||
||DV612|Nvarchar|8||Filler|||
||DV62|Nvarchar|1||Filler|||
||DV621|Nvarchar|1||Flag|||
||DV622|Nvarchar|1||Flag|||
||DV63|Numeric|13.4||Filler|||
||DV631|Numeric|13.4||Filler|||
||DV632|Numeric|13.4||Filler|||
||DV64|Nvarchar|50||Flag|||
||DV641|Nvarchar|50||Flag|||
||DV642|Nvarchar|50||Flag|||

P-Key：DV01 + DV02 + DV03 + DV031 + DV04 + DV041 + DV05

IX_DV1：DV01+DV60