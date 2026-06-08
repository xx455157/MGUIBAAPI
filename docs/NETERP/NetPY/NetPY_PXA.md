Net.PY 資料表欄位說明-PXA

| # | # | # | # | # | # |
|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|Chinese<br>Name|# Description|
|##|PXA001|Nvarchar|8|建檔日期||
||PXA002|Nvarchar|9|建檔時間||
||PXA003|Nvarchar|30|建檔工作站||
||PXA004|Nvarchar|30|建檔人||
||PXA005|Nvarchar|8|更改日期||
||PXA006|Nvarchar|9|更改時間||
||PXA007|Nvarchar|30|更改工作站||
||PXA008|Nvarchar|30|更改人||
|*|PXA01|Nvarchar|2|公司別||
|*|PXA02|Nvarchar|20|身分證字號||
|*|PXA03|Nvarchar|8|發薪日期||
|*|PXA04|Numeric||序號||
||PXA05|Nvarchar|1|所得類別|1.超額獎金/2.兼職所得/3.執行業務收入/4.股利所得/5.租金收入/6.利息所得|
||PXA06|Nvarchar|6|部門別||
||PXA07|Nvarchar|10|員工編號||
||PXA08|Nvarchar|4|支薪代碼||
||PXA09|Numeric||所得金額||
||PXA10|Numeric||上限金額||
||PXA11|Numeric||下限金額||
||PXA12|Numeric||最低工薪|兼職收入才填入|
||PXA13|Numeric||可扣抵投保金額|為超額獎金時,此欄位存入投保級距*4<br>為股利所得,且為雇主者,此欄位存入去年總投保金額|
||PXA14|Numeric||補充保費費率||
||PXA15|Numeric||計算金額(計算費基)||
||PXA16|Numeric||補充保費金額||
||PXA17|Nvarchar|1|是否免扣 Filler|Y/N|
||PXA18|Nvarchar|10|Filler||
||PXA19|Nvarchar|10|Filler||
||PXA20|Nvarchar|10|Filler||
||PXA21|Nvarchar|8|Date Filler||
||PXA22|Nvarchar|8|Date Filler||
||PXA23|Nvarchar|8|Date Filler||
||PXA24|Nvarchar|1|Filler||
||PXA25|Nvarchar|1|Filler||
||PXA26|Nvarchar|1|Filler||
||PXA27|Numeric||Filler||
||PXA28|Numeric||Filler||
||PXA29|Numeric||Filler||
||PXA30|Nvarchar|Max|Filler||
||PXA31|Nvarchar|Max|Filler||
||PXA32|Nvarchar|Max|Filler||
||PXA33|Numeric||前期累計獎金||
||PXA34|Numeric||超額獎金||

P-Key：PXA01 + PXA02 + PXA03 + PXA04