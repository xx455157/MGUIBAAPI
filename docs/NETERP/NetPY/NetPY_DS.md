NetPY 資料表欄位說明-DS

| # | # | # | # | # |
|---|---|---|---|---|
|Key|# FieldName|# DataType|# Length|# Description|
||DS001|Nvarchar|8|建檔日期|
||DS002|Nvarchar|9|建檔時間|
||DS003|Nvarchar|30|建檔工作站|
||DS004|Nvarchar|30|建檔人|
||DS005|Nvarchar|8|更改日期|
||DS006|Nvarchar|9|更改時間|
||DS007|Nvarchar|30|更改工作站|
||DS008|Nvarchar|30|更改人|
|＊|DS01|Nvarchar|2|類別(01:請假原因/02:加班原因/03:銷假原因)|
|＊|DS02|Nvarchar|2|代碼|
||DS03|Nvarchar|50|中文說明|
||DS04|Nvarchar|100|英文說明|
||DS05|Nvarchar|1|開放自由輸入(0:否/1:是)|
||DS06|Nvarchar|1|Filler|
||DS07|Nvarchar|1|Filler|
||DS08|Nvarchar|8|Filler|
||DS09|Nvarchar|8|Filler|
||DS10|Nvarchar|10|Filler|
||DS11|Nvarchar|10|Filler|
||DS12|Nvarchar|50|Filler|
||DS13|Nvarchar|50|Filler|
||DS14|Numeric||Filler|
||DS15|Numeric||Filler|
||DS16|Nvarchar|Max|Filler|
||DS17|Nvarchar|Max|Filler|

P-Key: DS01+DS02