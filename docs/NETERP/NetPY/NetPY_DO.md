Net.PY 資料表欄位說明-DO

| # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|Chinese<br>Name|English<br>Name|# Description|
|##|DO001|Nvarchar|8|建檔日期|Add Date||
||DO002|Nvarchar|9|建檔時間|Add Time||
||DO003|Nvarchar|30|建檔工作站|Add WS||
||DO004|Nvarchar|30|建檔人|Add User||
||DO005|Nvarchar|8|更改日期|Save Date||
||DO006|Nvarchar|9|更改時間|Save Time||
||DO007|Nvarchar|30|更改工作站|Save WS||
||DO008|Nvarchar|30|更改人|Save User||
|＊|DO01|Nvarchar|2 8|公司別|Company ID|A01.A0101|
|＊|DO02|Nvarchar|8|日期|Date||
|＊|DO03|Nvarchar|4|班別/考勤代碼|Shift ID||
|＊|DO04|Nvarchar|20|身分證字號|||
||DO05|Nvarchar|10|員工編號|Employee ID|P3.P301=DO01<br>P3.PA06=DO04|
||DO06|Nvarchar|6 8|部門別|Department ID|A02.A0201|
||DO07|Nvarchar|12|預定上班時間(1)|Duty Time|YYYYMMDDHHNN|
||DO071|Nvarchar|12|預定上班(1)<br>容許時間(起)|Duty Time buffer|YYYYMMDDHHNN|
||DO072|Nvarchar|12|預定上班(1)<br>容許時間(迄)|Duty Time buffer|YYYYMMDDHHNN|
||DO073|Nvarchar|12|實際上班時間(1)|Duty Time|YYYYMMDDHHNN|
||DO08|Nvarchar|12|預定下班時間(1)|Off Time|YYYYMMDDHHNN|
||DO081|Nvarchar|12|預定下班(1)<br>容許時間(起)|Off Time buffer|YYYYMMDDHHNN|
||DO082|Nvarchar|12|預定下班(1)<br>容許時間(迄)|Off Time buffer|YYYYMMDDHHNN|
||DO083|Nvarchar|12|實際下班時間(1)|Duty Time|YYYYMMDDHHNN|
||DO09|Nvarchar|12|預定上班時間(2)|Duty Time|YYYYMMDDHHNN|
||DO091|Nvarchar|12|預定上班(2)<br>容許時間(起)|Duty Time buffer|YYYYMMDDHHNN|
||DO092|Nvarchar|12|預定上班(2)<br>容許時間(迄)|Duty Time buffer|YYYYMMDDHHNN|
||DO093|Nvarchar|12|實際上班時間(2)|Duty Time|YYYYMMDDHHNN|
||DO10|Nvarchar|12|預定下班時間(2)|Off Time|YYYYMMDDHHNN|
||DO101|Nvarchar|12|預定下班(2)<br>容許時間(起)|Off Time buffer|YYYYMMDDHHNN|
||DO102|Nvarchar|12|預定下班(2)<br>容許時間(迄)|Off Time buffer|YYYYMMDDHHNN|
||DO103|Nvarchar|12|實際下班時間(2)|Duty Time|YYYYMMDDHHNN|
||DO11|Nvarchar|1|實際刷卡狀態||N:正常/ A:異常|
||DO12|Nvarchar|10|卡號|Card No.||
||DO13|Nvarchar|1|假別註記||空白:班別/1:考勤|
||DO14|Numeric|13.4|預計工時|||
||DO15|Numeric|13.4|實際工時|||
||DO16|Numeric|13.4|核薪工時||臨時幫工專用|
||DO17|Nvarchar|1|資料別||1:正職/2:幫工|
||DO18|Numeric|25|時薪||臨時幫工專用|
||DO19|Numeric|25|核薪金額||臨時幫工專用|
||DO20|Nvarchar|1|核定狀態||空白.排班/1.未關帳/<br>2.已關帳/3.已轉薪(臨時幫工專用)|
||DO21|Nvarchar|8|發薪日期||臨時幫工專用|
||DO22|Nvarchar|4|支薪代號||臨時幫工專用|
||DO23|Nvarchar|10|Filler|Filler||
||DO24|Nvarchar|10|Filler|Filler||
||DO25|Nvarchar|10|Filler|Filler||
||DO26|Nvarchar|8|Date Filler|Date Filler||
||DO27|Nvarchar|8|Date Filler|Date Filler||
||DO28|Nvarchar|8|Date Filler|Date Filler||
||DO29|Nvarchar|1|Filler|Filler||
||DO30|Nvarchar|1|Filler|Filler||
||DO31|Nvarchar|1|Filler|Filler||
||DO32|Numeric|13.4|Filler|Filler||
||DO33|Numeric|13.4|Filler|Filler||
||DO34|Numeric|13.4|Filler|Filler||
||DO35|Nvarchar|Max|角色|Filler|畫面開16碼|
||DO36|Nvarchar|Max|Filler|Filler||
||DO37|Nvarchar|Max|Filler|Filler||

P-Key：DO01 + DO02 + DO03 + DO04

註：1. DQ比對DO時，即使時間沒有完全符合，也須將刷卡資料填入

    2.若為彈性上下班(DL05=’2’)，只要刷卡資料有一筆以上，狀態就為正常；只有一筆時，為異常