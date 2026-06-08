Net.PY 資料表欄位說明-XPA

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|PA001|Nvarchar|8|8|建檔日期|Add Date||
||PA002|Nvarchar|9|8|建檔時間|Add Time||
||PA003|Nvarchar|30|12|建檔工作站|Add WS||
||PA004|Nvarchar|30|12|建檔人|Add User||
||PA005|Nvarchar|8|8|更改日期|Save Date||
||PA006|Nvarchar|9|8|更改時間|Save Time||
||PA007|Nvarchar|30|12|更改工作站|Save WS||
||PA008|Nvarchar|30|12|更改人|Save User||
||PA009|Nvarchar|6|0|Filler|Filler||
||PA010|Nvarchar|8|0|健保加保日|LH. In Date||
||PA011|Nvarchar|8|0|健保退保日|LH. Out Date||
||PA012|Numeric||0|健保投保金額|LH. Amount||
||PA013|Numeric|0|0|健保眷屬人數|LH. Persons||
||PA014|Nvarchar|1|0|健\勞保身份|LH. Type||
||PA015|Nvarchar|1|0|證號別|Data Type||
||PA016|Nvarchar|10|0|固定班別|Fix Shift||
||PA017|Nvarchar|10|0|時薪|Hour Rate||
||PA01|Nvarchar|2|6|公司別|ComXPAny ID||
||PA02|Nvarchar|6|6|部門別|Dept. ID||
||PA03|Nvarchar|40|10|中文姓名|Name(C)||
||PA04|Nvarchar|30|20|英文姓名|Name(E)||
|＊|PA05|Nvarchar|20|10|身份證字號|Social ID||
||PA06|Nvarchar|10|8|員工代號|Employee ID||
||PA07|Nvarchar|10|10|歸屬員工代號|Employee Master||
||PA08|Nvarchar|1|4|性別|Sex||
||PA09|Nvarchar|8|8|出生日期|Birthday||
||PA10|Nvarchar|20|10|籍貫|Birth Place||
||PA11|Nvarchar|120|40|戶籍地址|Home Address||
||PA12|Nvarchar|15|10|戶籍電話|Tel.(H)||
||PA13|Nvarchar|120|40|目前地址|Contact Address||
||PA14|Nvarchar|15|10|目前電話|Contact Tel.||
||PA15|Nvarchar|40|10|轉帳銀行|Bank ID||
||PA16|Nvarchar|20|15|銀行帳號|Bank Account||
||PA17|Nvarchar|8|8|勞保加保日|LI. In Date||
||PA18|Numeric||8|投保金額|LI. Amount||
||PA19|Nvarchar|8|8|勞保退保日|LI. Out Date||
||PA20|Nvarchar|8|8|到職日期|On Board Date||
||PA21|Nvarchar|8|15|離職日期|Leave Date||
||PA22|Nvarchar|20|8|國籍|Nationality||
||PA23|Nvarchar|1|8|本國人或外國人|Local/Foreign||
||PA24|Nvarchar|8|8|外國人最近入境時間|Last Immigration Date||
||PA25|Nvarchar|1|8|職災類別|OccuXPAtion Calamity|1及空白:勞保含職災<br>2:只保職災|
||PA26|Nvarchar|1|6|發薪別|XPAy Type|1：月薪2：日薪3：計件|
||PA27|Nvarchar|1|4|兼職|XPArt-Time|Y/N|
||PA28|Nvarchar|62|40|備註|Remark||
||PA29|Numeric||8|撫養人數|Home Members|算所得稅用|
||PA30|Numeric||8|差異年資|Differential Years||
||PA31|Nvarchar|2|8|職等代號|Level ID||
||PA32|Nvarchar|2|8|職務代號|Job ID||
||PA33|Nvarchar|2|4|薪號|Salary Code||
||PA34|Nvarchar|2|4|級距|Salary Level||
||PA35|Nvarchar|1|8|教育程度代號|Education||
||PA36|Nvarchar|30|10|畢業學校|||
||PA37|Nvarchar|1|8|留職停薪|Suspend|(Y/N)|
||PA38|Numeric|||Filler|Filler||
||PA39|Numeric|||Filler|Filler||
||PA40|Nvarchar|1|8|員工類別|Employee Type|1:正職,2:兼職,3:工讀生,<br>4:實習生|
||PA41|Nvarchar|10|8|考勤種類|Attend Type|正常,免考勤|
||PA42|Nvarchar|10|0|特休假剩餘結算方式||(1:轉薪,2:轉補休)|
||PA43|Numeric|||Filler|Filler|Filler|
||PA44|Numeric|||Filler|Filler|Filler|
||PA45|Nvarchar|8|8|試用截止日|Probation Date||
||PA46|Nvarchar|8||免扣補充保費類別(二代健保產生)||1.6項所得(或收入)皆免扣取/2.薪資所得/3.執行業務收入/4.薪資所得及執行業務收入/5.未達基本工資之兼職薪資所得|
||PA47|VarBinary|Max||員工照片|Photo||
||PA48|Nvarchar|1||Filler|Filler||
||PA49|Nvarchar|1||Filler|Filler||
||PA50|Nvarchar|1||Filler|Filler||
||PA51|Nvarchar|8||實際到職日|Filler|(若無差異年資等同PA20)|
||PA52|Nvarchar|8||Date Filler|Date Filler||
||PA53|Nvarchar|8||Date Filler|Date Filler||
||PA54|Nvarchar|10||Text Filler|Text Filler||
||PA55|Nvarchar|10||Text Filler|Text Filler||
||PA56|Nvarchar|10||Text Filler|Text Filler||
||PA57|Nvarchar|50||Text Filler|Text Filler||
||PA58|Nvarchar|50||Text Filler|Text Filler||
||PA59|Nvarchar|50||Text Filler|Text Filler||
||PA60|Numeric|||Filler|Filler|Filler|
||PA61|Numeric|||Filler|Filler|Filler|
||PA62|Numeric|||Filler|Filler|Filler|
|||||||||

P-Key：PA05

IX_XPA1：PA01 + PA06