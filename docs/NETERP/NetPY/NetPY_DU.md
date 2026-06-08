Net.PY 資料表欄位說明-DU

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DU001|Nvarchar|8|8|建檔日期|Add Date||
||DU002|Nvarchar|9|8|建檔時間|Add Time||
||DU003|Nvarchar|30|12|建檔工作站|Add WS||
||DU004|Nvarchar|30|12|建檔人|Add User||
||DU005|Nvarchar|8|8|更改日期|Save Date||
||DU006|Nvarchar|9|8|更改時間|Save Time||
||DU007|Nvarchar|30|12|更改工作站|Save WS||
||DU008|Nvarchar|30|12|更改人|Save User||
|＊|DU01|Nvarchar|8|6|公司別(流程)|Company||
|＊|DU02|Nvarchar|6|10|流程編號|Flow No.||
|＊|DU03|Numeric|4|6|序號|Sequence||
||DU04|Nvarchar|4|10|簽核階段|Step||
||DU05|Nvarchar|10|10|核准人編號|Manager|Ref PA(PA01=DU61 and PA06=DU04)|
||DU06|Numeric|4|4|核准數量|Authorized QTY||
||DU07|Nvarchar|1|4|會簽|CC.|Y/N|
||DU60|Nvarchar|10||待變更之原核准人|||
||DU601|Nvarchar|10||Filler|||
||DU602|Nvarchar|10||Filler|||
||DU61|Nvarchar|8|6|公司別(核准人)|||
||DU611|Nvarchar|8||照會部門||Ref ARTHGUI.A0201|
||DU612|Nvarchar|8||核准主管類別||Ref RP.SINI Section=’<br>ApprovalMGRType’<br>1:申请部门主管<br>2:上層部門主管<br>3:照會部門主管<br>4:會簽部門主管|
||DU62|Nvarchar|1||必審者<br>照會||空白/Y|
||DU621|Nvarchar|1||一般性通知||(空白:Mail/M:手機/A:全部/N:不通知)|
||DU622|Nvarchar|1||急件通知||(空白:Mail/M:手機/ A:全部/N:不通知)|
||DU63|Numeric|13.4||Filler|||
||DU631|Numeric|13.4||Filler|||
||DU632|Numeric|13.4||Filler|||
||DU64|Nvarchar|50||Flag|||
||DU641|Nvarchar|50||Flag|||
||DU642|Nvarchar|50||Flag|||

P-Key：DU01 + DU02 + DU03