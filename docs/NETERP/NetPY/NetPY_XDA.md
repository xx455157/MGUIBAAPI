Net.PY 資料表欄位說明-XDA

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# XDAta<br># Type|# XDAta<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|DA001|Nvarchar|8|8|建檔日期|Add Datee||
||DA002|Nvarchar|9|8|建檔時間|Add Time||
||DA003|Nvarchar|30|12|建檔工作站|Add WS||
||DA004|Nvarchar|30|12|建檔人|Add User||
||DA005|Nvarchar|8|8|更改日期|Save Date||
||DA006|Nvarchar|9|8|更改時間|Save Time||
||DA007|Nvarchar|30|12|更改工作站|Save WS||
||DA008|Nvarchar|30|12|更改人|Save User||
||DA009|Nvarchar|6|0|Filler|Filler||
||DA010|Nvarchar|8|0|Date Filler|Date Filler||
||DA011|Nvarchar|8|0|Date Filler|Date Filler||
||DA012|Numeric||0|Number Filler|Number Filler||
||DA013|Numeric|0|0|Number Filler|Number Filler||
||DA014|Nvarchar|1|0|Flag|Flag||
||DA015|Nvarchar|1|0|Flag|Flag||
||DA016|Nvarchar|10|0|Filler|Filler||
||DA017|Nvarchar|10|0|Filler|Filler||
|＊|DA01|Nvarchar|2|6|公司別|Company ID||
|＊|DA02|Nvarchar|4|0|Filler|Filler||
|＊|DA03|Nvarchar|20|10|身分證字號|Social ID||
|＊|DA04|Nvarchar|8|8|生效日期|Date||
|＊|DA05|Nvarchar|4|8|異動代號|Attendance Code||
||DA06|Numeric||4|序號|Seq. No.||
||DA07|Numeric||2|日|Day||
||DA08|Numeric||2|時|Hours||
||DA09|Numeric||2|分|Mins.||
||DA10|Numeric||4|計算量|QTY||
||DA11|Numeric||8|獎懲金額|Amount||
||DA12|Nvarchar|2|8|扣抵代號|Deduct Type|(1:加項,2:減項,3:無)|
||DA13|Nvarchar|2|8|考勤類別|Attend Type||
||DA14|Nvarchar|6|6|部門別|Dept. ID|Ref A02.A0201|
||DA15|Nvarchar|10|8|員工代號|Employee ID|Ref XPA.PA06 and XPA.PA01=XDA.DA01|
|＊|DA16|Nvarchar|4|4|班別|Shift No.||
||DA17|Nvarchar|8|8|發佈日期|Announce Date||
||DA18|Nvarchar|8|8|起始日期|Start Date||
||DA19|Nvarchar|9|8|起始時間|Start Time|Input ,填滿後|
||DA20|Nvarchar|8|8|截止日期|End Date||
||DA21|Nvarchar|9|8|截止時間|End Time|Input ,填滿後|
||DA22|Nvarchar|80|40|備註|Remark||
||DA23|Nvarchar|2|0|資料來源|Filler|1.線上請假|
||DA24|Nvarchar|4|8|扣抵異動代號|Deduct Code||
||DA25|Numeric|||扣抵量|Deduct QTY||

P-Key：DA01 + DA02 + DA03 + DA04 + DA05 + DA16