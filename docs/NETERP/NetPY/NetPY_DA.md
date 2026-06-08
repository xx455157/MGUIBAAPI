Net.PY 資料表欄位說明-DA

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DA001|Nvarchar|8|8|建檔日期|Add DDAe||
||DA002|Nvarchar|9|8|建檔時間|Add Time||
||DA003|Nvarchar|30|12|建檔工作站|Add WS||
||DA004|Nvarchar|30|12|建檔人|Add User||
||DA005|Nvarchar|8|8|更改日期|Save Date||
||DA006|Nvarchar|9|8|更改時間|Save Time||
||DA007|Nvarchar|30|12|更改工作站|Save WS||
||DA008|Nvarchar|30|12|更改人|Save User||
|＊|DA01|Nvarchar|2 8|2 8|公司別|Company ID||
|＊|DA03|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
|＊|DA04|Nvarchar|8|8|生效日期|Date||
||DA05|Nvarchar|4|4|異動代號|Attendance Code|DB.DB03|
|＊|DA06|Numeric||4|序號|Seq. No.||
||DA10|Numeric||4|計算量|QTY||
||DA11|Numeric||8|獎懲金額|Amount||
||DA14|Nvarchar|6 8|6 8|部門別|Dept. ID|Ref A02.A0201|
||DA15|Nvarchar|10|10|員工代號|Employee ID|P3.P301=DA01<br>P3.PA06=DA15|
||DA16|Nvarchar|4|4|班別|Shift No.||
||DA17|Nvarchar|8|8|發佈日期|Announce Date||
||DA18|Nvarchar|8|8|起始日期|Start Date||
||DA19|Nvarchar|9|8|起始時間|Start Time|Input ,填滿後|
||DA20|Nvarchar|8|8|截止日期|End Date||
||DA21|Nvarchar|9|8|截止時間|End Time|Input ,填滿後|
||DA22|Nvarchar|80|40|備註|Remark||
||DA24|Nvarchar|4|4|扣抵異動代號|Deduct Code|DB.DB03|
||DA25|Numeric|||扣抵量|Deduct QTY||
||DA60|Nvarchar|10|10|Filler|Filler||
||DA601|Nvarchar|10|10|Filler|Filler||
||DA602|Nvarchar|10|10|Filler|Filler||
||DA61|Nvarchar|8|8|Date Filler|Date Filler||
||DA611|Nvarchar|8|8|Date Filler|Date Filler||
||DA612|Nvarchar|8|8|Date Filler|Date Filler||
||DA62|Nvarchar|1|1|資料來源||空白:考勤異動輸入<br>1:線上請假<br>2:個人請假登錄<br>3:大批請假登錄|
||DA621|Nvarchar|1|1|Filler|Filler||
||DA622|Nvarchar|1|1|Filler|Filler||
||DA63|Numeric|||Filler|Filler||
||DA631|Numeric|||Filler|Filler||
||DA632|Numeric|||Filler|Filler||
||DA64|Nvarchar|50|50|Filler|Filler||
||DA641|Nvarchar|50|50|Filler|Filler||
||DA642|Nvarchar|50|50|Filler|Filler||

P-Key：DA01 + DA03 + DA04 + DA06

A-Key：DA01 + DA15 + DA04 + DA16