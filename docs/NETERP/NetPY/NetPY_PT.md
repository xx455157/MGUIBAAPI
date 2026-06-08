Net.PY 資料表欄位說明-PT

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PT001|Nvarchar|8|8|建檔日期|Add Date||
||PT002|Nvarchar|9|8|建檔時間|Add Time||
||PT003|Nvarchar|30|12|建檔工作站|Add WS||
||PT004|Nvarchar|30|12|建檔人|Add User||
||PT005|Nvarchar|8|8|更改日期|Save Date||
||PT006|Nvarchar|9|8|更改時間|Save Time||
||PT007|Nvarchar|30|12|更改工作站|Save WS||
||PT008|Nvarchar|30|12|更改人|Save User||
||PT01|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
||PT011|Nvarchar|2|2|公司別|Company ID|A01.A0101|
||PT02|Nvarchar|8|8|生效日期|TXDate||
||PT03|Nvarchar|9|9|生效時間|TXTime||
||PT04|Nvarchar|6|6|記錄代碼|Record ID||
||PT05|Nvarchar|Max|30|異動說明|RemarkForChange||
||PT06|Nvarchar|Max|30|異動前記錄|RecordBeforeChange||
||PT07|Nvarchar|Max|30|異動後記錄|RecordAfterChange||
||PT08|Muneric||10|異動前金額|AmountBeforeChange||
||PT09|Numeric||10|異動後金額|AmountAfterChange||
|*|PT10|Numeric|13.4||流水號<br>(自動取號)|||
|||||||||

P-Key：PT10