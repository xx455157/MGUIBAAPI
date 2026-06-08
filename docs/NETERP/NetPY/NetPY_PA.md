Net.PY 資料表欄位說明-PA

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
||PA010|Nvarchar|8|8|健保加保日|LH. In Date|保留不處理, 以PU中的PU07為主|
||PA011|Nvarchar|8|8|健保退保日|LH. Out Date|保留不處理, 以PU中的PU08為主|
||PA012|Numeric||6|健保投保金額|LH. Amount||
||PA013|Numeric|0|1|健保眷屬人數|LH. Persons||
||PA014|Nvarchar|1|0|投保人身份|LH.IDType|1.員工/2.滿65歲或外勞/3顧主|
||PA015|Nvarchar|1|0|證號別|Data Type||
||PA01|Nvarchar|2|6|公司別(健保)|Company ID||
||PA03|Nvarchar|40|10|中文姓名|Name(C)||
||PA04|Nvarchar|40|20|英文姓名|Name(E)||
|*|PA05|Nvarchar|20|10|身份證字號|Social ID|本人或眷屬，當為眷屬時可允許空白，但在眷保時必須有值|
||PA08|Nvarchar|1|4|性別|Sex|F:女/M:男/C:公司|
||PA09|Nvarchar|8|8|出生日期|Birthday||
||PA10|Nvarchar|Max|10|籍貫|Birth Place||
||PA11|Nvarchar|Max|40|戶籍地址|Home Address||
||PA12|Nvarchar|20|10|戶籍電話|Tel.(H)||
||PA13|Nvarchar|Max|40|目前地址|Contact Address||
||PA14|Nvarchar|15|10|目前電話|Contact Tel.||
||PA22|Nvarchar|Max|8|國籍|Nationality||
||PA23|Nvarchar|1|8|本國人或外國人|Local/Foreign|Y:外國人/N:本國人|
||PA24|Nvarchar|8|8|最近入境時間|Last Immigration Date||
||PA28|Nvarchar|Max|40|備註|Remark||
||PA29|Numeric||8|撫養人數|Home Members|算所得稅用|
||PA35|Nvarchar|1|8|教育程度代號|Education||
||PA36|Nvarchar|Max|10|畢業學校|||
||PA42|Nvarchar|20|12|聯絡手機(1)|Cell Phone(1)||
||PA421|Nvarchar|20|12|聯絡手機(2)|Cell Phone(2)||
||PA43|Nvarchar|120|30|e-Mail(1)|e-Mail(1)||
||PA431|Nvarchar|120|30|e-Mail(2)|e-Mail(2)||
||PA441|Nvarchar|120|30|MSN Account|MSN Account||
||PA442|Nvarchar|120|12|Skype Account|Skype Account||
||PA443|Nvarchar|120|12|Google Talk|Google Talk||
||PA444|Nvarchar|120|12|Other-1|Other-1||
||PA445|Nvarchar|120|12|Other-2|Other-2||
||PA45|Nvarchar|3|3|血型|Blood Type||
||PA461|Nvarchar|20|12|傳真電話(1)|Fax (1)|戶籍傳真|
||PA462|Nvarchar|20|12|傳真電話(2)|Fax (2)|聯絡傳真|
||PA48|Nvarchar|Max|10|薪資單密碼|SlipPassword||
||PA49|Numeric||3|身高(cm)|Height(cm)||
||PA50|Numeric||3|體重(kg)|Weight(kg)||
||PA51|Nvarchar|1|3|婚姻狀態|Marriage||
||PA52|Nvarchar|40|10|配偶姓名|||
||PA541|Nvarchar|20|12|緊急連絡人|EMContactor||
||PA542|Nvarchar|20|12|緊急聯絡電話|EMContact Phone||
||PA543|Nvarchar|Max|12|緊急聯絡地址|EMContactAdd||
||PA60|Nvarchar|10|10|居留證號碼|Filler||
||PA601|Nvarchar|10|10|Filler|Filler||
||PA602|Nvarchar|10|10|Filler|Filler||
||PA61|Nvarchar|8|8|租稅協定代碼|租稅協定代碼|只開放2碼長|
||PA611|Nvarchar|8|8|Date Filler|Date Filler||
||PA612|Nvarchar|8|8|Date Filler|Date Filler||
||PA62|Nvarchar|1|1|外籍|||
||PA621|Nvarchar|1|1|免扣補充保費項目|Filler|1.6項所得(或收入)皆免扣取/2.薪資所得/3.執行業務收入/4.薪資所得及執行業務收入/5.未達基本工資之兼職薪資所得|
||PA622|Nvarchar|1|1|Filler|Filler||
||PA63|Numeric|||Filler|Filler||
||PA631|Numeric|||Filler|Filler||
||PA632|Numeric|||Filler|Filler||
||PA64|Nvarchar|50|50|Filler|Filler||
||PA641|Nvarchar|50|50|Filler|Filler||
||PA642|Nvarchar|50|50|Filler|Filler||

P-Key：PA05