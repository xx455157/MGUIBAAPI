# DX_請假單簽核流程明細檔

## 資料表資訊

**系統：** NETERP/NETPY (NetERP薪資管理系統)
**資料表代碼：** DX
**中文名稱：** 請假單簽核流程明細檔
**主鍵：** DX01 + DX02 + DX03 (公司別 + 請假單號 + 序號)
**最後更新日期：** 2024-07-17
**版本：** 1.0

## 資料表說明

本資料表用於記錄每一張請假單的簽核流程明細，包括各階段的簽核人、代理人、簽核結果、簽核狀態、通知方式等，支援多階段、會簽、通知等複雜簽核流程。

## 欄位定義

> 標記說明：(P) 主鍵欄位、(M) 必填欄位、(F) 外鍵欄位

| 欄位代號 | 欄位名稱       | 資料型態 | 長度 | 屬性   | 備註說明 |
|----------|----------------|----------|------|--------|----------|
| DX001    | 建檔日期       | NVARCHAR | 8    | M      | 系統自動產生(YYYYMMDD) |
| DX002    | 建檔時間       | NVARCHAR | 9    | M      | 系統自動產生(HHMMSSFF) |
| DX003    | 建檔工作站     | NVARCHAR | 30   | M      | |
| DX004    | 建檔人         | NVARCHAR | 30   | M      | |
| DX005    | 更改日期       | NVARCHAR | 8    | M      | |
| DX006    | 更改時間       | NVARCHAR | 9    | M      | |
| DX007    | 更改工作站     | NVARCHAR | 30   | M      | |
| DX008    | 更改人         | NVARCHAR | 30   | M      | |
| DX01     | 公司別         | NVARCHAR | 2    | P,F    | 公司代碼，參照[ARTHGUI-A01公司基本資料檔](../../../ARTHGUI/A01.md) |
| DX02     | 請假單號       | NVARCHAR | 12   | P      | YYYYMMDDnnnnnn，系統自動給號 |
| DX03     | 序號           | NUMERIC  | 4    | P      | |
| DX04     | 簽核階段       | NVARCHAR | 4    |        | |
| DX05     | 應核准人       | NVARCHAR | 10   | F      | 員工代號，參照[ARTHPY-AC員工主檔](../../../ARTHPY/AC.md)；Ref PA(PA01=DX61 and PA06=DX05) |
| DX06     | 核准人編號     | NVARCHAR | 10   | F      | 員工代號，參照[ARTHPY-AC員工主檔](../../../ARTHPY/AC.md)；Ref PA(PA01=DX611 and PA06=DX06) |
| DX07     | 簽核內容       | NVARCHAR | MAX  |        | |
| DX08     | 簽核結果       | NVARCHAR | 1    |        | 參照[NETGUI-SINI系統參數設定檔](../../../NETERP/NETGUI/SINI.md) Section=Common_NetPY_LeaveApproveResult，Y:同意 N:不同意 空白:核准中 |
| DX09     | 簽核日期       | NVARCHAR | 8    |        | |
| DX10     | 簽核時間       | NVARCHAR | 9    |        | 電腦自動取 |
| DX11     | 最後階段       | NVARCHAR | 1    |        | Y/空白 |
| DX12     | 簽核狀態       | NVARCHAR | 1    |        | 參照[NETGUI-SINI系統參數設定檔](../../../NETERP/NETGUI/SINI.md) Section=Common_NetPY_LeaveApproveStatus，0:未簽核 1:簽核中 2:完成 3:取消 |
| DX60     | 待變更的應核准人 | NVARCHAR | 10   |        | |
| DX601    | 保留欄位       | NVARCHAR | 10   |        | |
| DX602    | 保留欄位       | NVARCHAR | 10   |        | |
| DX61     | 公司別(應核准人) | NVARCHAR | 8    | F      | 公司代碼，參照[ARTHGUI-A01公司基本資料檔](../../../ARTHGUI/A01.md) |
| DX611    | 公司別(核准人) | NVARCHAR | 8    | F      | 公司代碼，參照[ARTHGUI-A01公司基本資料檔](../../../ARTHGUI/A01.md) |
| DX612    | 保留欄位       | NVARCHAR | 8    |        | |
| DX62     | 照會           | NVARCHAR | 1    |        | 空白/Y |
| DX621    | 一般性通知     | NVARCHAR | 1    |        | (空白:Mail/M:手機/ A:全部/N:不通知) |
| DX622    | 急件通知       | NVARCHAR | 1    |        | |
| DX63     | 核准數量       | NUMERIC  | 13,4 |        | 參照DU06 (會簽數量=0) |
| DX631    | 保留欄位       | NUMERIC  | 13,4 |        | |
| DX632    | 保留欄位       | NUMERIC  | 13,4 |        | |
| DX64     | 手機簽核訊息TxMessage Id | NVARCHAR | 50 |        | For 手機訊息回收使用 |
| DX641    | 手機訊息公告TxMessage Id | NVARCHAR | 50 |        | For 手機訊息回收使用 |
| DX642    | Flag           | NVARCHAR | 50   |        | |

## 索引定義說明

| 索引名稱 | 欄位組成 | 索引類型 | 說明 |
|----------|----------|----------|------|
| PK_DX | DX01+DX02+DX03 | 主鍵索引 | 確保每筆簽核明細唯一 |
| IX_DX_APPNO | DX02 | 一般索引 | 加速請假單號查詢 |
| IX_DX_MANAGER | DX05 | 一般索引 | 加速應核准人查詢 |

## 資料關聯說明

- DX01、DX61、DX611 (公司別) 參照[ARTHGUI-A01公司基本資料檔](../../../ARTHGUI/A01.md)
- DX05、DX06 (員工代號) 參照[ARTHPY-AC員工主檔](../../../ARTHPY/AC.md)
- DX08 (簽核結果)、DX12 (簽核狀態) 參照[NETGUI-SINI系統參數設定檔](../../../NETERP/NETGUI/SINI.md)

## 資料表管理流程

DX資料表為請假單簽核流程的明細檔，記錄每一階段的簽核資訊，支援多階段、會簽、通知等複雜簽核流程，供薪資系統審批、查詢與追蹤。

## 變更歷史記錄

| 版本 | 日期       | 修改人     | 變更描述                      |
| ---- | ---------- | ---------- | ----------------------------- |
| 1.0  | 2024-07-17 | 系統管理員 | 依資料表文件規範修復初始版本 |