Net.PY 資料表欄位說明-WK01

| # | # | # | # | # | # | # | # |
|---|---|---|---|---|---|---|---|
|# Key|# Field<br># Name|# Data<br># Type|# Data<br># Len|# Display<br>Len|Chinese<br>Name|English<br>Name|# Description|
|##|WK01001|Nvarchar|8||建檔日期|Add Date||
||WK01002|Nvarchar|9||建檔時間|Add Time||
||WK01003|Nvarchar|30||建檔工作站|Add WS||
||WK01004|Nvarchar|30||建檔人|Add User||
||WK01005|Nvarchar|8||更改日期|Save Date||
||WK01006|Nvarchar|9||更改時間|Save Time||
||WK01007|Nvarchar|30||更改工作站|Save WS||
||WK01008|Nvarchar|30||更改人|Save User||
||WK0101|Nvarchar|3||稽徵機關代號|||
||WK0102|Nvarchar|4||媒體單位代號(未使用)|||
|＊|WK0103|Nvarchar|8||流水號|||
||WK0104|Nvarchar|1||所得註記|||
||WK0105|Nvarchar|2||所得類別代碼|||
||WK0106|Nvarchar|20||所得人證號|||
||WK0107|Nvarchar|1||證號別|||
||WK0108|Nvarchar|8||扣繳單位統一編號|||
||WK0109|Numeric|||給付總額|||
||WK0110|Numeric|||扣繳稅額|||
||WK0111|Numeric|||給付淨額|||
||WK0112|Nvarchar|12||房屋稅籍編號、業別、所得人帳號)或外僑護照號碼||所得類別<=50時此欄位為員工編號;其餘情形,此欄位等於PB19|
||WK0113|Nvarchar|1||所得人證號錯誤註記|||
|＊|WK0114|Nvarchar|4||所得歸屬年度|||
||WK0115|Nvarchar|12||所得人中文姓名|||
||WK0116|Nvarchar|60||所得人中文地址|||
||WK0117|Nvarchar|1||國內有無住所||1:有,2:無|
||WK0118|Nvarchar|2||Filler|||
||WK0119|Nvarchar|2||Filler|||
||WK0120|Nvarchar|12||房屋稅籍編號||扣繳憑單用<br>所得類別等於51時此欄位等於PB19，否則為空白|
|＊|WK0121|Nvarchar|2||公司別|||
||WK0122|Numeric|13.4||股利稅率(扣抵率)|||
||WK0123|Numeric|13.4||分配次數|||
||WK0124|Nvarchar|6||給付起始年月|||
||WK0125|Nvarchar|6||給付結束年月|||
||WK0126|Numeric|13.4||自願提撥金|||
||WK0127|Numeric|13.4||現金股利|||
||WK0128|Numeric|13.4||股票股利|||
||WK0129|Numeric|13.4||股票數|||
||WK0130|Nvarchar|6||國家代碼|||
||WK0131|Nvarchar|6||租稅協定代碼|||
||WK0132|Nvarchar|1||Filler|||
||WK0133|Nvarchar|1||Filler|||
||WK0134|Nvarchar|1||Filler|||
||WK0135|Nvarchar|8||除權日期|||
||WK0136|Nvarchar|8||Filler|||
||WK0137|Nvarchar|8||Filler|||
||WK0138|Nvarchar|10||Filler|||
||WK0139|Nvarchar|10||Filler|||
||WK0140|Nvarchar|10||Filler|||
||WK0141|Numeric|13.4||Filler|||
||WK0142|Numeric|13.4||Filler|||
||WK0143|Numeric|13.4||Filler|||
||WK0144|Nvarchar|Max||Filler|||
||WK0145|Nvarchar|Max||Filler|||
||WK0146|Nvarchar|Max||Filler|||
|||||||||

P-Key：WK0121 + WK0114 + WK0103

註：股利稅率／分配次數／除權日期，只有WK0105=54且WK0104=C時，才寫入