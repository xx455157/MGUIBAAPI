Net.PY 資料表欄位說明-PB

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PB001|Nvarchar|8|8|建檔日期|Add Date||
||PB002|Nvarchar|9|8|建檔時間|Add Time||
||PB003|Nvarchar|30|12|建檔工作站|Add WS||
||PB004|Nvarchar|30|12|建檔人|Add User||
||PB005|Nvarchar|8|8|更改日期|Save Date||
||PB006|Nvarchar|9|8|更改時間|Save Time||
||PB007|Nvarchar|30|12|更改工作站|Save WS||
||PB008|Nvarchar|30|12|更改人|Save User||
||PB01|Nvarchar|2|8|股利登錄區分|Dividend Type||
|*|PB02|Nvarchar|2|2|公司別|Company ID|Ref A01.A0101|
||PB03|Nvarchar|6|6|部門別|Department ID|Ref A02.A0201|
|*|PB04|Nvarchar|20|10|身分證字號|Social ID|PA.PA05|
||PB05|Nvarchar|10|10|員工編號|Employee ID|P3.P301=PB02<br>P3.PA06=PB05|
||PB06|Nvarchar|10|10|歸屬員工編號|Master Employee|P3.P301=PB02<br>P3.PA06=PB06|
|*|PB0789|Nvarchar|8|8|發薪日期|Pay Date||
||PB10|Nvarchar|4|4|歸屬年度|Income Year||
||PB11|Nvarchar|2|2|歸屬月份|Income Month||
||PB12|Nvarchar|4|4|支薪代碼|Pay Code|Ref PC.PC01|
|*|PB13|Numeric||2|序號|Sequence||
||PB14|Numeric||6|金額|Amount||
||PB15|Nvarchar|2|2|所得類別|Income Type||
||PB16|Nvarchar|1|2|證號別|ID Type||
||PB17|Nvarchar|Max|30|備註|Remark||
||PB18|Nvarchar|1|1|過帳記號|GL Mark||
||PB19|Nvarchar|20|12|房屋稅籍編號/所得人代號|House Tax Code||
||PB20|Nvarchar|1|1|類別區分|Type Code|空白:薪資資料／0: 非薪資資料／1:非薪資資料／2:股利資料|
||PB21|Nvarchar|10|10|(保留)|Filler||
||PB22|Nvarchar|30|20|資料產生來源|Data Source||
||PB23|Nvarchar|1|1|註記|Remark Code||
||PB24|Nvarchar|1|1|補充保費計算類別|||
||PB25|Numeric||6|股利稅率|Dividend Tax Rate||
||PB26|Numeric||6|股數|||
||PB27|Nvarchar|50|30|租賃房屋地址|||
||PB281|Numeric||8|健保投保金額|LH. Amount||
||PB282|Numeric||8|勞保投保金額|LI. Amount||
||PB283|Numeric||8|提繳工資|Retirement Salary||
||PB60|Nvarchar|10|10|Filler|Filler||
||PB601|Nvarchar|10|10|Filler|Filler||
||PB602|Nvarchar|10|10|Filler|Filler||
||PB61|Nvarchar|8|8|Date Filler|Date Filler||
||PB611|Nvarchar|8|8|Date Filler|Date Filler||
||PB612|Nvarchar|8|8|Date Filler|Date Filler||
||PB62|Nvarchar|1|1|Filler|Filler||
||PB621|Nvarchar|1|1|Filler|Filler||
||PB622|Nvarchar|1|1|Filler|Filler||
||PB63|Numeric|||Filler|Filler||
||PB631|Numeric|||Filler|Filler||
||PB632|Numeric|||Filler|Filler||
||PB64|Nvarchar|50|50|Filler|Filler||
||PB641|Nvarchar|50|50|Filler|Filler||
||PB642|Nvarchar|50|50|Filler|Filler||

P-Key：PB02+ PB04 + PB0789 + PB13