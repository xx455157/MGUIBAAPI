Net.PY 資料表欄位說明-PC

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PC001|Nvarchar|8|8|建檔日期|Add Date||
||PC002|Nvarchar|9|8|建檔時間|Add Time||
||PC003|Nvarchar|30|12|建檔工作站|Add WS||
||PC004|Nvarchar|30|12|建檔人|Add User||
||PC005|Nvarchar|8|8|更改日期|Save Date||
||PC006|Nvarchar|9|8|更改時間|Save Time||
||PC007|Nvarchar|30|12|更改工作站|Save WS||
||PC008|Nvarchar|30|12|更改人|Save User||
|*|PC01|Nvarchar|4|4|支薪代碼|Pay Code||
||PC02|Nvarchar|20|20|名稱(C)|Description(C)||
||PC03|Nvarchar|1|1|加減項區分|Plus/Minus|1:加項 2:減項|
||PC04|Nvarchar|1|1|所得區分|Income Code|1:列入所得 <br>2:不列入所得 <br>3:所得稅|
||PC05|Nvarchar|1|1|所得稅扣繳方式|Tax Type|1:級距表 2:所得別稅率|
||PC06|Nvarchar|1|1|固定區分|Regular Income|1:固定 2:非固定|
||PC07|Nvarchar|2|2|所得類別|Income Type||
||PC08|Nvarchar|1|1|納入勞退計算|Retirement Included||
||PC09|Nvarchar|1|1|勞退自願提撥|Self-Paid Retirement|Y or N|
||PC10|Nvarchar|1|1|月結所得稅|Monthly Tax|僅用在PC04=”時|
||PC11|Nvarchar|20|20|名稱(E)|Description(E)||
||PC12|Nvarchar|4|4|扣抵支薪代碼|||
||PC13|Nvarchar|1|1|不破月計算(全月)||Y or 空白|
||PC60|Nvarchar|10|10|Filler|Filler||
||PC601|Nvarchar|10|10|Filler|Filler||
||PC602|Nvarchar|10|10|Filler|Filler||
||PC61|Nvarchar|8|8|Date Filler|Date Filler||
||PC611|Nvarchar|8|8|Date Filler|Date Filler||
||PC612|Nvarchar|8|8|Date Filler|Date Filler||
||PC62|Nvarchar|1|1|納入補充保費||空白:否/Y:是|
||PC621|Nvarchar|1|1|Filler|Filler||
||PC622|Nvarchar|1|1|Filler|Filler||
||PC63|Numeric|||Filler|Filler||
||PC631|Numeric|||Filler|Filler||
||PC632|Numeric|||Filler|Filler||
||PC64|Nvarchar|50|50|Filler|Filler||
||PC641|Nvarchar|50|50|Filler|Filler||
||PC642|Nvarchar|50|50|Filler|Filler||

P-Key：PC01