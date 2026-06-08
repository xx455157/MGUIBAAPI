# NDS_資料不同步指示檔

## 資料表資訊

**系統：** [ARTHAS](../ARTHAS.md) (固定資產管理系統)
**資料表代碼：** NDS
**中文名稱：** 資料不同步指示檔
**主鍵：** NDS01 + NDS02 + NDS03 + NDS04 + NDS05 + NDS06 + NDS07 + NDS08 + NDS09
**最後更新日期：** 2024-07-11
**版本：** 1.0

## 資料表說明

資料不同步指示檔(NDS)用於標記系統中存在資料不同步的記錄，主要記錄表格名稱及相關主鍵值，方便系統進行後續同步處理。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱  | 資料型態 | 長度 | 屬性 | 備註說明      |
| -------- | --------- | -------- | ---- | ---- | ------------- |
| NDS01    | TableName | NVARCHAR | 50   | P,M  | 資料表名稱    |
| NDS02    | Data PK-1 | NVARCHAR | 50   | P    | 資料表主鍵值1 |
| NDS03    | Data PK-2 | NVARCHAR | 50   | P    | 資料表主鍵值2 |
| NDS04    | Data PK-3 | NVARCHAR | 50   | P    | 資料表主鍵值3 |
| NDS05    | Data PK-4 | NVARCHAR | 50   | P    | 資料表主鍵值4 |
| NDS06    | Data PK-5 | NVARCHAR | 50   | P    | 資料表主鍵值5 |
| NDS07    | Data PK-6 | NVARCHAR | 50   | P    | 資料表主鍵值6 |
| NDS08    | Data PK-7 | NVARCHAR | 50   | P    | 資料表主鍵值7 |
| NDS09    | Data PK-8 | NVARCHAR | 50   | P    | 資料表主鍵值8 |

## 索引定義

| 索引名稱  | 索引欄位                                              | 索引類型 | 說明                   |
| --------- | ----------------------------------------------------- | -------- | ---------------------- |
| PK_NDS    | NDS01+NDS02+NDS03+NDS04+NDS05+NDS06+NDS07+NDS08+NDS09 | 主鍵     | 唯一識別一筆不同步記錄 |
| IDX_NDS_A | NDS01                                                 | 一般     | 依資料表名稱查詢       |

## 資料表管理流程

資料不同步指示檔(NDS)是系統用於處理資料同步的輔助資料表。當系統偵測到資料不一致時，會在此表中記錄相關資訊，並透過同步機制自動或手動執行資料同步。此表不需要使用者直接維護，主要由系統自動處理。

## 變更歷史記錄

| 版本 | 日期       | 修改人     | 變更描述                                 |
| ---- | ---------- | ---------- | ---------------------------------------- |
| 1.0  | 2024-07-11 | 系統管理員 | 初始版本建立，規範化資料不同步指示檔文檔 |

**資料不同步指示檔**

**Table** **Field** **** **DataType** **Length** **Description**

**============================================================**

NDS NDS01 N varchar 50 TableName

NDS02 N varchar 50 Data PK-1

NDS03 N varchar 50 Data PK-2

NDS04 N varchar 50 Data PK-3

NDS05 N varchar 50 Data PK-4

NDS06 N varchar 50 Data PK-5

NDS07 N varchar 50 Data PK-6

NDS08 N varchar 50 Data PK-7

NDS09 N varchar 50 Data PK-8

NDS ： NDS 01 +   NDS 0 2+   NDS 0 3+   NDS 0 4+   NDS 0 5+   NDS 0 6+   NDS 0 7+   NDS 0 8+   NDS 0 9
