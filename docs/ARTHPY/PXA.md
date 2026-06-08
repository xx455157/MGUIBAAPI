PXA  二代健保補充保費明細檔

**Table** **Field** **DataType** **Length** **Description**

P XA PXA0 01 Text 8 建檔日期

P XA 002 Text 8 建檔時間

P XA 003 Text 10 建檔工作站

P XA 004 Text 10 建檔人

P XA 005 Text 8 更改日期

P XA 006 Text 8 更改時間

P XA 007 Text 10 更改工作站

P XA 008 Text 10 更改人

P XA 01 Text 8 公司別

P XA 02 Text 10    員工身份證字號

P XA 03 Text 8   發薪日期  (50 日只 keep 單期健保投保金額 )

P XA 0 4 Double 序號

PXA05 Text 1       計 算類別 ( 1. 超額獎金 /2. 兼職所得 /3. 執行業

務收入 /4. 股利所得 /5. 租金收入 /6. 利息所得 )

P XA 0 6 Text 6 部門別

P XA 0 7 Text 10 員工編號

P XA 0 8 Text 4     支薪代碼

P XA 0 9 Double 所得金額

P XA10 Double 上限金額

P XA11 Double 下限金額

P XA12 Double 最低 工 資( 兼職收入才填入 )

P XA13 Double 可扣抵投保金額 ( 若發薪日期為的日為 50,

用來 Keep 健保投保金額 , 不會有計算類別 )

P XA1 4 Double 本期保費費率

P XA1 5 Double 計算金額 ( 計算費基 )

P XA1 6 Double 補充保費金額

P XA1 7 Text 1 免扣補充保費類別

P XA18 Text 10 Free

P XA19 Text 10 Free

P XA20 Text 10 Free

P XA21 Text 8 Free

P XA22 Text 8 Free

P XA23 Text 8 Free

P XA24 Text 1 Free

P XA25 Text 1 Free

P XA26 Text 1 Free

P XA 27 Double 原計算金額

P XA28 Double 原補充保費金額

P XA29 Double 原可扣抵投保金額

P XA30 Text 40 備註

P XA31 Text 40 Free

P XA32 Double Free

P XA33 Double 前期累計獎金

P XA34 Double 超額獎金

P X A :  P X A 0 1 + P X A 0 2  + PX A 03 + PX A 04   (P & U)
