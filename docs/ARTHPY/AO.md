# AO_員工排班檔

## 資料表資訊

**系統：** ARTHPY (薪資系統)
**資料表代碼：** AO
**中文名稱：** 員工排班檔
**主鍵：** AO01, AO02, AO03, AO08, AO10
**最後更新日期：** 2024-06-01
**版本：** 1.0

## 資料表說明

員工排班檔(AO)用於記錄員工每日排班、班別、部門、公司、標準上下班時間、假別、特殊排班等資訊，支援薪資與考勤系統之班表管理。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(U) 唯一值欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱             | 資料型態 | 長度 | 屬性 | 備註說明 |
|----------|----------------------|----------|------|------|----------|
| AO001    | 建檔日期             | NVARCHAR | 8    |      |          |
| AO002    | 建檔時間             | NVARCHAR | 8    |      |          |
| AO003    | 建檔工作站           | NVARCHAR | 10   |      |          |
| AO004    | 建檔人               | NVARCHAR | 10   |      |          |
| AO005    | 更改日期             | NVARCHAR | 8    |      |          |
| AO006    | 更改時間             | NVARCHAR | 8    |      |          |
| AO007    | 更改工作站           | NVARCHAR | 10   |      |          |
| AO008    | 更改人               | NVARCHAR | 10   |      |          |
| AO01     | 員工代號             | NVARCHAR | 10   | P,F  | 員工代碼，參照[ARTHPY-A08員工基本資料檔](../ARTHPY/A08.md) |
| AO02     | 日期                 | NVARCHAR | 8    | P    |          |
| AO03     | 班別代號             | NVARCHAR | 4    | P    |          |
| AO04     | 部門代號             | NVARCHAR | 8    |      |          |
| AO05     | 上下班別             | NVARCHAR | 1    |      | 1:上班 2:下班 |
| AO06     | 容許時間:起          | NVARCHAR | 12   |      | YYYYMMDDHHMM |
| AO07     | 容許時間:迄          | NVARCHAR | 12   |      | YYYYMMDDHHMM |
| AO08     | 公司別               | NVARCHAR | 10   | P,F  |          |
| AO09     | 假別對應國定假日日期 | NVARCHAR | 8    |      |          |
| AO10     | 標準時間             | NVARCHAR | 12   | P    | YYYYMMDDHHMM |
| AO11     | 臨時班別流水號(前台) | NVARCHAR | 8    |      |          |
| AO12     | Filler(休息時數)     | NUMERIC  | 25,4 |      | 六福村訂製 |
| AO13     | Filler(實際工時)     | NUMERIC  | 25,4 |      | 六福村訂製 |
| AO14     | 上班標準時間         | NUMERIC  | 25,4 |      |          |
| AO15     | Filler(假別註記)     | NVARCHAR | 1    |      |          |
| AO16     | 原班別(銷假用)       | NVARCHAR | 4    |      | (.Net)    |
| AO17     | 居家上班記號         | NVARCHAR | 1    |      | Y=居家    |
| AO18     | Filler(分區)         | NVARCHAR | 50   |      | 台勤訂製  |
| AO19     | Filler(工作項目)     | NVARCHAR | 50   |      | 台勤訂製  |
| AO20     | 申請假單單號(忘卡)   | NVARCHAR | 12   |      |          |
| AO30     | Filler               | NVARCHAR | 1    |      |          |
| AO31     | Filler               | NVARCHAR | 1    |      |          |
| AO32     | Filler               | NVARCHAR | 1    |      |          |
| AO33     | Filler               | NVARCHAR | 10   |      |          |
| AO34     | Filler               | NVARCHAR | 10   |      |          |
| AO35     | Filler               | NVARCHAR | 10   |      |          |
| AO36     | Filler               | NVARCHAR | 50   |      |          |
| AO37     | Filler               | NVARCHAR | 50   |      |          |
| AO38     | Filler               | NVARCHAR | 50   |      |          |
| AO39     | Filler               | NVARCHAR | 8    |      |          |
| AO40     | Filler               | NVARCHAR | 8    |      |          |
| AO41     | Filler               | NVARCHAR | 8    |      |          |
| AO42     | Filler               | NUMERIC  | 25,4 |      |          |
| AO43     | Filler               | NUMERIC  | 25,4 |      |          |
| AO44     | Filler               | NUMERIC  | 25,4 |      |          |

## 資料型態規範

- 所有字元欄位統一使用 NVARCHAR 型態。
- 日期欄位格式為 YYYYMMDD。
- 數值欄位統一使用 NUMERIC(25,4)。

## 索引定義說明

| 索引名稱 | 欄位組成                       | 索引類型 | 說明         |
|----------|--------------------------------|----------|--------------|
| PK_AO_01 | AO01, AO02, AO03, AO08, AO10   | 主鍵     | 員工排班唯一識別 |
| IDX_AO_1 | AO08, AO01, AO06, AO07         | 輔助索引 | 排班查詢輔助   |

## 資料關聯說明

- 與公司基本資料檔(A01)的關聯：AO08 = A01公司代碼
- 與部門資料檔(A02)的關聯：AO04 = A02部門代碼
- 與員工基本資料檔(A08)的關聯：AO01 = A08.A0801員工代號

## 變更歷史記錄

| 版本 | 日期       | 修改人 | 變更描述         |
|------|------------|--------|------------------|
| 1.0  | 2024-06-01 | System | 初始標準化修復   |
