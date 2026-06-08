Net.PY 資料表欄位說明-P4

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|P4001|Nvarchar|8|8|建檔日期|Add Date||
||P4002|Nvarchar|9|8|建檔時間|Add Time||
||P4003|Nvarchar|30|12|建檔工作站|Add WS||
||P4004|Nvarchar|30|12|建檔人|Add User||
||P4005|Nvarchar|8|8|更改日期|Save Date||
||P4006|Nvarchar|9|8|更改時間|Save Time||
||P4007|Nvarchar|30|12|更改工作站|Save WS||
||P4008|Nvarchar|30|12|更改人|Save User||
|＊|P401|Nvarchar|20|10|身份證字號|Social ID||
|＊|P402|Nvarchar|2|6|資料類別|DataType|00：學歷<br>01：工作經歷<br>02：證照|
|＊|P403|Numeric|3|4|顯示序號|Sequence||
||P404|Nvarchar|20|8|項目|Item|學歷:學籍(小學、初中..)<br>經歷:空白<br>證照:空白|
||P405|Nvarchar|Max|12|項目名稱一|Description1|學歷:學校名稱<br>經歷:公司名稱<br>證照:證照名稱|
||P406|Nvarchar|Max|12|項目名稱二|Description2|學歷:科系名稱<br>經歷:職務名稱<br>證照:發照單位|
||P407|Nvarchar|8|8|有效起日|StartDate|學歷:就讀日期<br>經歷:到職日期<br>證照:有效起日|
||P408|Nvarchar|8|8|有效迄日|EndDate|學歷:畢業日期<br>經歷:離職日期<br>證照:有效迄日|
||P409|Nvarchar|Max|12|成績|Result|學歷:畢業/肄業<br>經歷:離職原因<br>證照:成績|
||P460|Nvarchar|10|10|Filler|Filler||
||P4601|Nvarchar|10|10|Filler|Filler||
||P4602|Nvarchar|10|10|Filler|Filler||
||P461|Nvarchar|8|8|Date Filler|Date Filler||
||P4611|Nvarchar|8|8|Date Filler|Date Filler||
||P4612|Nvarchar|8|8|Date Filler|Date Filler||
||P462|Nvarchar|1|1|Filler|Filler||
||P4621|Nvarchar|1|1|Filler|Filler||
||P4622|Nvarchar|1|1|Filler|Filler||
||P463|Numeric|||Filler|Filler||
||P4631|Numeric|||Filler|Filler||
||P4632|Numeric|||Filler|Filler||
||P464|Nvarchar|50|50|Filler|Filler||
||P4641|Nvarchar|50|50|Filler|Filler||
||P4642|Nvarchar|50|50|Filler|Filler||

P-KEY: P401 + P402 + P403

說明：

學歷的有效值，請取SINI中之資料，Section=EducationLevel，Topic=存檔序號，TopicValue=學歷(P404)