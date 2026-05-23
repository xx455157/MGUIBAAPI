# vOAM15 合約管理系統 - 完整開發與測試指令

## 資料概覽

| 層級 | 資料表 | 用途 | 現況 |
|------|--------|------|------|
| 合約主檔 | OA20 | 合約基本資料（客戶、金額、日期、業務） | ✅ 已實作（DAL/BLL） |
| 產品/服務 | OA21 | 合約下的產品清單與單價 | ✅ 已實作（DAL/BLL） |
| 收支明細 | OA22 | 收支預算/發票/收款（整併三表） | ⚠️ 草案，待建 Controller |
| ~~發票~~ | ~~OA23~~ | — | ❌ 已廢止，整併至 OA22 |
| ~~收款~~ | ~~OA24~~ | — | ❌ 已廢止，整併至 OA22 |
| ~~收支款現況~~ | ~~OA25~~ | — | ❌ 已廢止，整併至 OA22 |

---

## 一、資料庫建表指令（由 DBA 執行）

### 1-1. OA20 合約主檔 — 新增欄位

```sql
-- ============================================================
-- OA20 合約主檔 - 新增欄位 SQL
-- 對應前端欄位：contractType / contractEndDate / remark / contractFileUrl
-- ============================================================
-- 請 DBA 確認後執行

-- 合約主檔 (OA20) 新增欄位
ALTER TABLE OA20 ADD OA2005 NVARCHAR(10) NULL;       -- 合約到期日
ALTER TABLE OA20 ADD OA2006 NVARCHAR(1) NULL;        -- 合約類別 (M=維護, S=買賣, R=租用)
ALTER TABLE OA20 ADD OA2011 NVARCHAR(MAX) NULL;       -- 備註
ALTER TABLE OA20 ADD OA2012 NVARCHAR(10) NULL;       -- 合約延展控制日期
ALTER TABLE OA20 ADD OA2013 NVARCHAR(10) NULL;       -- 合約成立日期
ALTER TABLE OA20 ADD OA2014 NVARCHAR(20) NULL;        -- 目前Sales
ALTER TABLE OA20 ADD OA2015 NVARCHAR(500) NULL;      -- 合約掃描檔連結

-- 建議建立索引
CREATE NONCLUSTERED INDEX IX_OA20_ContractType ON OA20 (OA2001, OA2006);
CREATE NONCLUSTERED INDEX IX_OA20_Customer ON OA20 (OA2001, OA2003);
CREATE NONCLUSTERED INDEX IX_OA20_Status ON OA20 (OA2001, OA2010);
```

### 1-2. OA21 產品/服務 — 新增欄位

```sql
-- ============================================================
-- OA21 產品/服務資料 - 新增欄位 SQL
-- 對應前端欄位：maintenanceStartDate / maintenanceEndDate / rentalStartDate / rentalEndDate
-- ============================================================
-- 請 DBA 確認後執行

-- 產品/服務 (OA21) 新增欄位
ALTER TABLE OA21 ADD OA2112 NVARCHAR(10) NULL;       -- 維護合約起始日
ALTER TABLE OA21 ADD OA2113 NVARCHAR(10) NULL;       -- 維護合約到期日
ALTER TABLE OA21 ADD OA2114 NVARCHAR(10) NULL;       -- 租用生效日期
ALTER TABLE OA21 ADD OA2115 NVARCHAR(10) NULL;       -- 租用到期日

-- 建議建立索引
CREATE NONCLUSTERED INDEX IX_OA21_Contract ON OA21 (OA2101, OA2102);
CREATE NONCLUSTERED INDEX IX_OA21_Maintenance ON OA21 (OA2101, OA2112, OA2113);
```

### 1-3. OA22 收支明細 — 建表（草案，需 DBA 確認）

```sql
-- ============================================================
-- OA22 收支明細（新增草案表，待確認後建表）
-- 主鍵：(OA2201 公司別) + (OA2202 合約代號) + (OA2203 序號)
--
-- ※ 此表為草案，待正式確認後方可建表。
--   OA22 同時包含收支預算、發票、收款三種狀態的欄位。
--   OA23（發票）/ OA24（收款）/ OA25（收支款現況）已廢止，整併至本表。
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA22')
BEGIN
    CREATE TABLE OA22 (
        -- 收支明細主要欄位（主鍵構成）
        OA2201 NVARCHAR(8) NOT NULL,         -- 公司別（主鍵 Part 1）
        OA2202 NVARCHAR(20) NOT NULL,        -- 合約代號（主鍵 Part 2）
        OA2203 NVARCHAR(10) NOT NULL,        -- 序號（主鍵 Part 3）
        -- 收支類別欄位
        OA2204 NVARCHAR(1) NULL,              -- 收支別 (I=收入, E=支出)
        OA2205 NVARCHAR(20) NULL,            -- 收支類別
        OA2206 NVARCHAR(30) NULL,            -- 收支科目
        OA2207 NVARCHAR(30) NULL,             -- 收支項目名稱
        -- 收支預算欄位
        OA2208 NUMERIC(25,4) DEFAULT 0 NULL, -- 預算金額
        OA2209 NVARCHAR(10) NULL,            -- 預算日期
        OA2210 NUMERIC(25,4) DEFAULT 0 NULL, -- 實際金額
        OA2211 NVARCHAR(10) NULL,            -- 實際日期
        OA2212 NVARCHAR(20) NULL,            -- 目前業務
        OA2213 NVARCHAR(30) NULL,            -- GL傳票號碼（收支傳票）
        OA2214 NVARCHAR(100) NULL,           -- 備註
        -- 發票欄位
        OA2215 NVARCHAR(1) NULL,             -- 是否已開發票 (Y/N)
        OA2216 NVARCHAR(30) NULL,            -- 發票號碼
        OA2217 NVARCHAR(10) NULL,            -- 發票日期
        OA2218 NVARCHAR(10) NULL,            -- 發票類別
        OA2219 NUMERIC(25,4) DEFAULT 0 NULL, -- 發票金額
        OA2220 NVARCHAR(100) NULL,            -- 發票明細
        OA2221 NVARCHAR(30) NULL,            -- 發票GL傳票號碼
        OA2222 NVARCHAR(10) NULL,            -- FILLER
        -- 收款欄位
        OA2223 NVARCHAR(1) NULL,             -- 是否已收款 (Y/N)
        OA2224 NVARCHAR(30) NULL,            -- 匯款編號
        OA2225 NVARCHAR(30) NULL,            -- 支票號碼
        OA2226 NVARCHAR(10) NULL,            -- 收款日期
        OA2227 NUMERIC(25,4) DEFAULT 0 NULL, -- 收款金額
        OA2228 NVARCHAR(30) NULL,            -- 收款GL傳票號碼
        -- FILLER
        OA2229 NVARCHAR(10) NULL,            -- FILLER
        OA2230 NUMERIC(25,4) DEFAULT 0 NULL, -- FILLER
        OA2231 NUMERIC(25,4) DEFAULT 0 NULL, -- FILLER
        -- 建檔/異動標準欄位
        AddDate DATETIME DEFAULT GETDATE() NULL,
        AddUser NVARCHAR(20) NULL,
        UpdateDate DATETIME NULL,
        UpdateUser NVARCHAR(20) NULL,
        PRIMARY KEY (OA2201, OA2202, OA2203)
    );
    
    CREATE NONCLUSTERED INDEX IX_OA22_Contract ON OA22(OA2201, OA2202);
    CREATE NONCLUSTERED INDEX IX_OA22_IncomeExpense ON OA22(OA2201, OA2202, OA2204);
    CREATE NONCLUSTERED INDEX IX_OA22_Invoice ON OA22(OA2201, OA2202, OA2215);
    CREATE NONCLUSTERED INDEX IX_OA22_Payment ON OA22(OA2201, OA2202, OA2223);
    
    PRINT 'OA22 建立完成。';
END
```

---

## 二、測試資料寫入指令（完整版）

以下 SQL 包含：建表防呆 + 3 筆 OA20 + 6 筆 OA21 + 9 筆 OA22（收支明細）

```sql
-- ============================================================
-- vOAM15 合約模組 - 完整測試資料建表 SQL
--
-- 內容：
--   - OA20 合約主檔（3 筆）
--   - OA21 產品/服務（每合約 2 筆，共 6 筆）
--   - OA22 收支明細（每合約 3 筆，共 9 筆）
--
-- ⚠️ 執行前請確認：
--   1. DBA 已執行上方新增欄位 SQL（OA20/OA21 欄位 OA2005~OA2015 / OA2112~OA2115）
--   2. DBA 已確認是否已有正式 OA20/OA21 資料（若有，請先備份）
--   3. OA22 建表（草案）是否已確認
--
-- 建表日期：2026-05-05
-- ============================================================

SET NOCOUNT ON;
PRINT '============================================================';
PRINT 'vOAM15 合約模組 - 完整測試資料建表';
PRINT '建表日期：2026-05-05';
PRINT '============================================================';
PRINT '';

-- ============================================================
-- 全域變數設定
-- ============================================================
DECLARE @CompId NVARCHAR(8) = N'GUEST';
DECLARE @Today NVARCHAR(10) = CONVERT(NVARCHAR(10), GETDATE(), 111);
DECLARE @CurrentUser NVARCHAR(20) = N'TESTUSER';

PRINT '公司別：' + @CompId;
PRINT '建檔日期：' + @Today;
PRINT '建檔人員：' + @CurrentUser;
PRINT '';

-- ============================================================
-- 一、OA20 合約主檔（3 筆測試資料）
-- ============================================================

PRINT '============================================================';
PRINT '一、OA20 合約主檔（3 筆）';
PRINT '============================================================';

-- 1-1. 合約 TEST001：維護合約（Active，執行中）
PRINT '';
PRINT '--- 寫入 OA20 測試資料 #1：TEST001（維護合約，Active）---';
DELETE FROM OA20 WHERE OA2001 = @CompId AND OA2002 = N'TEST001';
INSERT INTO OA20 (
    OA2001, OA2002, OA2003, OA2004, OA2005, OA2006,
    OA2007, OA2008, OA2009, OA2010, OA2011, OA2012,
    OA2013, OA2014, OA2015,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST001', N'C001', N'N',
    N'2026/12/31', N'M',       -- 合約終了日、合約類別（M=維護）
    1000000.00, 1050000.00, 200000.00,  -- 合約總價未稅/含稅、外包成本
    N'Active', N'年度 ERP 系統維護合約，含軟體更新與技術支援。', N'2026/10/01',  -- 狀態、備註、展期控制日
    @Today, N'SALES01', N'',           -- 成立日期、業務、文件URL
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA20 #1 寫入完成（TEST001）。';

-- 1-2. 合約 TEST002：買賣合約（Completed，已完成）
PRINT '';
PRINT '--- 寫入 OA20 測試資料 #2：TEST002（買賣合約，Completed）---';
DELETE FROM OA20 WHERE OA2001 = @CompId AND OA2002 = N'TEST002';
INSERT INTO OA20 (
    OA2001, OA2002, OA2003, OA2004, OA2005, OA2006,
    OA2007, OA2008, OA2009, OA2010, OA2011, OA2012,
    OA2013, OA2014, OA2015,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST002', N'C002', N'O',
    N'2027/06/30', N'S',       -- 合約終了日、合約類別（S=買賣）
    600000.00, 660000.00, 350000.00,  -- 合約總價未稅/含稅、外包成本
    N'Completed', N'伺服器與網路設備採購合約，已完成交付驗收。', N'',  -- 狀態、備註
    @Today, N'SALES02', N'',           -- 成立日期、業務、文件URL
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA20 #2 寫入完成（TEST002）。';

-- 1-3. 合約 TEST003：租用合約（Active，執行中）
PRINT '';
PRINT '--- 寫入 OA20 測試資料 #3：TEST003（租用合約，Active）---';
DELETE FROM OA20 WHERE OA2001 = @CompId AND OA2002 = N'TEST003';
INSERT INTO OA20 (
    OA2001, OA2002, OA2003, OA2004, OA2005, OA2006,
    OA2007, OA2008, OA2009, OA2010, OA2011, OA2012,
    OA2013, OA2014, OA2015,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST003', N'C003', N'N',
    N'2028/03/31', N'R',       -- 合約終了日、合約類別（R=租用）
    800000.00, 840000.00, 100000.00,   -- 合約總價未稅/含稅、外包成本
    N'Active', N'影印機租用合約，含每月保養服務。', N'2028/01/01',  -- 狀態、備註、展期控制日
    @Today, N'SALES03', N'',           -- 成立日期、業務、文件URL
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA20 #3 寫入完成（TEST003）。';

-- ============================================================
-- 二、OA21 產品/服務（每合約 2 筆，共 6 筆）
-- ============================================================

PRINT '';
PRINT '============================================================';
PRINT '二、OA21 產品/服務（共 6 筆）';
PRINT '============================================================';

-- 2-1. TEST001 的產品 1：年度維護服務
PRINT '';
PRINT '--- 寫入 OA21 測試資料：TEST001 / PROD001（年度維護）---';
DELETE FROM OA21 WHERE OA2101 = @CompId AND OA2102 = N'TEST001' AND OA2103 = N'PROD001';
INSERT INTO OA21 (
    OA2101, OA2102, OA2103, OA2104, OA2105, OA2106, OA2107,
    OA2108, OA2109, OA2110, OA2112, OA2113, OA2114, OA2115,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST001', N'PROD001',
    500000.00, 100000.00, N'2025/01/01', N'2025/12/31',  -- 售價、成本、保固日
    50000.00, N'PM001', N'MA',           -- 預計維護金、PM、類別（MA=維護）
    N'2025/01/01', N'2025/12/31', N'', N'',  -- 維護合約日、租用合約日
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA21 #1 寫入完成（TEST001 / PROD001）。';

-- 2-2. TEST001 的產品 2：軟體授權
PRINT '';
PRINT '--- 寫入 OA21 測試資料：TEST001 / PROD002（軟體授權）---';
DELETE FROM OA21 WHERE OA2101 = @CompId AND OA2102 = N'TEST001' AND OA2103 = N'PROD002';
INSERT INTO OA21 (
    OA2101, OA2102, OA2103, OA2104, OA2105, OA2106, OA2107,
    OA2108, OA2109, OA2110, OA2112, OA2113, OA2114, OA2115,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST001', N'PROD002',
    300000.00, 50000.00, N'2025/01/01', N'2025/12/31',
    0.00, N'PM002', N'SW',              -- 預計維護金=0（SW 軟體無維護費）、類別（SW=軟體）
    N'', N'', N'', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA21 #2 寫入完成（TEST001 / PROD002）。';

-- 2-3. TEST002 的產品 1：伺服器主機
PRINT '';
PRINT '--- 寫入 OA21 測試資料：TEST002 / PROD101（伺服器主機）---';
DELETE FROM OA21 WHERE OA2101 = @CompId AND OA2102 = N'TEST002' AND OA2103 = N'PROD101';
INSERT INTO OA21 (
    OA2101, OA2102, OA2103, OA2104, OA2105, OA2106, OA2107,
    OA2108, OA2109, OA2110, OA2112, OA2113, OA2114, OA2115,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST002', N'PROD101',
    450000.00, 280000.00, N'2025/04/01', N'2026/03/31',  -- 保固 1 年
    0.00, N'PM101', N'HW',             -- 預計維護金=0（HW 設備）、類別（HW=硬體）
    N'', N'', N'', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA21 #3 寫入完成（TEST002 / PROD101）。';

-- 2-4. TEST002 的產品 2：網路交換器
PRINT '';
PRINT '--- 寫入 OA21 測試資料：TEST002 / PROD102（網路交換器）---';
DELETE FROM OA21 WHERE OA2101 = @CompId AND OA2102 = N'TEST002' AND OA2103 = N'PROD102';
INSERT INTO OA21 (
    OA2101, OA2102, OA2103, OA2104, OA2105, OA2106, OA2107,
    OA2108, OA2109, OA2110, OA2112, OA2113, OA2114, OA2115,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST002', N'PROD102',
    150000.00, 70000.00, N'2025/04/01', N'2026/03/31',
    0.00, N'PM101', N'HW',
    N'', N'', N'', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA21 #4 寫入完成（TEST002 / PROD102）。';

-- 2-5. TEST003 的產品 1：影印機租用
PRINT '';
PRINT '--- 寫入 OA21 測試資料：TEST003 / PROD201（影印機租用）---';
DELETE FROM OA21 WHERE OA2101 = @CompId AND OA2102 = N'TEST003' AND OA2103 = N'PROD201';
INSERT INTO OA21 (
    OA2101, OA2102, OA2103, OA2104, OA2105, OA2106, OA2107,
    OA2108, OA2109, OA2110, OA2112, OA2113, OA2114, OA2115,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST003', N'PROD201',
    200000.00, 50000.00, N'2025/04/01', N'2030/03/31',  -- 保固到租用期滿
    120000.00, N'PM201', N'RE',             -- 預計維護金（每年保養）、類別（RE=租用設備）
    N'', N'', N'2025/04/01', N'2028/03/31',  -- 租用合約日（起訖）
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA21 #5 寫入完成（TEST003 / PROD201）。';

-- 2-6. TEST003 的產品 2：碳粉匣耗材
PRINT '';
PRINT '--- 寫入 OA21 測試資料：TEST003 / PROD202（碳粉匣耗材）---';
DELETE FROM OA21 WHERE OA2101 = @CompId AND OA2102 = N'TEST003' AND OA2103 = N'PROD202';
INSERT INTO OA21 (
    OA2101, OA2102, OA2103, OA2104, OA2105, OA2106, OA2107,
    OA2108, OA2109, OA2110, OA2112, OA2113, OA2114, OA2115,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST003', N'PROD202',
    60000.00, 30000.00, N'', N'',
    0.00, N'PM201', N'CU',              -- 類別（CU=耗材）
    N'', N'', N'2025/04/01', N'2028/03/31',  -- 租用合約日（起訖）
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA21 #6 寫入完成（TEST003 / PROD202）。';

-- ============================================================
-- 三、OA22 收支明細（每合約 3 筆，共 9 筆）
-- ============================================================

PRINT '';
PRINT '============================================================';
PRINT '三、OA22 收支明細（共 9 筆）';
PRINT '============================================================';

-- 3-1. TEST001 收支 #001：收入 - Q1 維護費（已開發票、已收款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST001 / 收支001（Q1維護費-已收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2203 = N'001';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST001', N'001',
    N'I', N'維護費', N'4101', N'Q1維護費',  -- 收支別/類別/科目/項目名稱
    500000.0000, N'2025/01/15', 500000.0000, N'2025/01/15',  -- 預算金額/日、實際金額/日
    N'SALES01', N'GL-2025-001', N'',         -- 業務、GL傳票、備註
    N'Y', N'INV-2025-001', N'2025/01/15', N'S', 525000.0000, N'2025年度維護合約第一期款項（Q1）', N'GL-2025-002', N'',  -- 發票
    N'Y', N'REM-2025-001', N'', N'2025/01/20', 525000.0000, N'GL-2025-003', N'',  -- 收款
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #1 寫入完成（TEST001 / 收支001）。';

-- 3-2. TEST001 收支 #002：支出 - Q1 人事成本（無發票、無收款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST001 / 收支002（Q1人事成本-未收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2203 = N'002';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST001', N'002',
    N'E', N'人事成本', N'6101', N'Q1人事成本',
    200000.0000, N'2025/02/01', 180000.0000, N'2025/02/05',
    N'SALES01', N'GL-2025-004', N'',
    N'N', N'', N'', N'', 0.0000, N'', N'', N'',
    N'N', N'', N'', N'', 0.0000, N'', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #2 寫入完成（TEST001 / 收支002）。';

-- 3-3. TEST001 收支 #003：收入 - Q2 軟體授權（已開發票、未收款 → 應收帳款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST001 / 收支003（Q2授權費-應收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2203 = N'003';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST001', N'003',
    N'I', N'軟體授權', N'4102', N'Q2軟體授權費',
    300000.0000, N'2025/04/01', 0.0000, N'',
    N'SALES02', N'', N'',
    N'Y', N'INV-2025-002', N'2025/03/31', N'S', 315000.0000, N'2025年度軟體授權合約款項（Q2預估）', N'', N'',
    N'N', N'', N'', N'', 0.0000, N'', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #3 寫入完成（TEST001 / 收支003）。';

-- 3-4. TEST002 收支 #001：收入 - 設備銷售（已開發票、已收款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST002 / 收支001（設備銷售-已收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2203 = N'001';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST002', N'001',
    N'I', N'設備銷售', N'4103', N'伺服器主機與網路設備',
    660000.0000, N'2025/04/01', 660000.0000, N'2025/06/30',
    N'SALES02', N'GL-2025-010', N'已完成驗收',
    N'Y', N'INV-2025-010', N'2025/06/30', N'S', 660000.0000, N'伺服器及網路設備採購合約款項', N'GL-2025-012', N'',
    N'Y', N'REM-2025-010', N'', N'2025/06/30', 660000.0000, N'GL-2025-013', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #4 寫入完成（TEST002 / 收支001）。';

-- 3-5. TEST002 收支 #002：支出 - 設備成本（無發票、未付款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST002 / 收支002（設備成本-未付）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2203 = N'002';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST002', N'002',
    N'E', N'設備費用', N'6102', N'伺服器與交換器採購成本',
    350000.0000, N'2025/04/01', 350000.0000, N'2025/05/15',
    N'SALES02', N'GL-2025-011', N'供應商已出貨',
    N'N', N'', N'', N'', 0.0000, N'', N'', N'',
    N'N', N'', N'CHK-2025-001', N'2025/05/15', 350000.0000, N'GL-2025-014', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #5 寫入完成（TEST002 / 收支002）。';

-- 3-6. TEST002 收支 #003：收入 - 安裝服務費（已開發票、已收款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST002 / 收支003（安裝服務費-已收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2203 = N'003';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST002', N'003',
    N'I', N'技術服務', N'4104', N'設備安裝設定服務費',
    50000.0000, N'2025/04/01', 50000.0000, N'2025/06/30',
    N'SALES02', N'GL-2025-015', N'已完成安裝',
    N'Y', N'INV-2025-011', N'2025/06/30', N'S', 52500.0000, N'設備安裝設定服務費', N'GL-2025-016', N'',
    N'Y', N'REM-2025-011', N'', N'2025/06/30', 52500.0000, N'GL-2025-017', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #6 寫入完成（TEST002 / 收支003）。';

-- 3-7. TEST003 收支 #001：收入 - 租用月費（已開發票、已收款，持續性收入）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST003 / 收支001（租用月費-已收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2203 = N'001';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST003', N'001',
    N'I', N'租金收入', N'4105', N'影印機月租金',
    200000.0000, N'2025/04/01', 200000.0000, N'2025/04/30',
    N'SALES03', N'GL-2025-020', N'首年月租費',
    N'Y', N'INV-2025-020', N'2025/04/30', N'S', 210000.0000, N'影印機租用合約首年月租費', N'GL-2025-021', N'',
    N'Y', N'REM-2025-020', N'', N'2025/05/05', 210000.0000, N'GL-2025-022', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #7 寫入完成（TEST003 / 收支001）。';

-- 3-8. TEST003 收支 #002：收入 - 耗材費用（已開發票、已收款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST003 / 收支002（耗材費用-已收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2203 = N'002';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST003', N'002',
    N'E', N'耗材費用', N'6105', N'碳粉匣與零件更換',
    60000.0000, N'2025/05/01', 45000.0000, N'2025/05/15',
    N'SALES03', N'GL-2025-023', N'實際使用量低於預估',
    N'Y', N'INV-2025-023', N'2025/05/15', N'S', 47250.0000, N'碳粉匣更換費用（實際用量 75%）', N'GL-2025-024', N'',
    N'Y', N'REM-2025-023', N'', N'2025/05/20', 47250.0000, N'GL-2025-025', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #8 寫入完成（TEST003 / 收支002）。';

-- 3-9. TEST003 收支 #003：收入 - 保養服務費（已開發票、未收款 → 應收帳款）
PRINT '';
PRINT '--- 寫入 OA22 測試資料：TEST003 / 收支003（保養服務費-應收）---';
DELETE FROM OA22 WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2203 = N'003';
INSERT INTO OA22 (
    OA2201, OA2202, OA2203,
    OA2204, OA2205, OA2206, OA2207,
    OA2208, OA2209, OA2210, OA2211,
    OA2212, OA2213, OA2214,
    OA2215, OA2216, OA2217, OA2218, OA2219, OA2220, OA2221, OA2222,
    OA2223, OA2224, OA2225, OA2226, OA2227, OA2228, OA2229,
    AddDate, AddUser, UpdateDate, UpdateUser
)
VALUES (
    @CompId, N'TEST003', N'003',
    N'I', N'服務收入', N'4106', N'年度保養服務費',
    120000.0000, N'2026/01/01', 0.0000, N'',
    N'SALES03', N'', N'預計 Q1 收取',
    N'Y', N'INV-2025-030', N'2025/12/31', N'S', 126000.0000, N'影印機年度保養服務費（2026年度）', N'', N'',
    N'N', N'', N'', N'', 0.0000, N'', N'',
    GETDATE(), @CurrentUser, GETDATE(), @CurrentUser
);
PRINT 'OA22 #9 寫入完成（TEST003 / 收支003）。';

-- ============================================================
-- 四、驗證查詢
-- ============================================================

PRINT '';
PRINT '============================================================';
PRINT '四、驗證查詢';
PRINT '============================================================';

-- 4-1. OA20 驗證
PRINT '';
PRINT '--- OA20 合約主檔（共 ' + CAST((SELECT COUNT(*) FROM OA20 WHERE OA2001 = @CompId AND OA2002 IN (N'TEST001',N'TEST002',N'TEST003')) AS NVARCHAR(10)) + ' 筆）---';
SELECT
    OA2001 AS 公司別,
    OA2002 AS 合約代號,
    OA2003 AS 客戶編號,
    OA2004 AS 新舊客戶,
    OA2005 AS 合約終了日,
    OA2006 AS 合約類別,
    OA2007 AS 合約總價未稅,
    OA2008 AS 合約總價含稅,
    OA2009 AS 外包成本預算,
    OA2010 AS 合約狀態,
    OA2011 AS 備註,
    OA2012 AS 展期控制日,
    OA2013 AS 合約成立日,
    OA2014 AS 目前業務,
    AddUser AS 建檔人
FROM OA20 WITH(NOLOCK)
WHERE OA2001 = @CompId AND OA2002 IN (N'TEST001',N'TEST002',N'TEST003')
ORDER BY OA2002;

-- 4-2. OA21 驗證
PRINT '';
PRINT '--- OA21 產品/服務（共 ' + CAST((SELECT COUNT(*) FROM OA21 WHERE OA2101 = @CompId AND OA2102 IN (N'TEST001',N'TEST002',N'TEST003')) AS NVARCHAR(10)) + ' 筆）---';
SELECT
    OA2101 AS 公司別,
    OA2102 AS 合約代號,
    OA2103 AS 產品ID,
    OA2110 AS 產品類別,
    OA2104 AS 銷售單價含稅,
    OA2105 AS 外包成本單價,
    OA2106 AS 保固開始日,
    OA2107 AS 保固終了日,
    OA2112 AS 維護起始日,
    OA2113 AS 維護終了日,
    OA2114 AS 租用起始日,
    OA2115 AS 租用終了日,
    AddUser AS 建檔人
FROM OA21 WITH(NOLOCK)
WHERE OA2101 = @CompId AND OA2102 IN (N'TEST001',N'TEST002',N'TEST003')
ORDER BY OA2102, OA2103;

-- 4-3. OA22 收支明細驗證
PRINT '';
PRINT '--- OA22 收支明細（共 ' + CAST((SELECT COUNT(*) FROM OA22 WHERE OA2201 = @CompId AND OA2202 IN (N'TEST001',N'TEST002',N'TEST003')) AS NVARCHAR(10)) + ' 筆）---';
SELECT
    OA2201 AS 公司別,
    OA2202 AS 合約代號,
    OA2203 AS 序號,
    OA2204 AS 收支別,
    OA2205 AS 收支類別,
    OA2207 AS 收支項目名稱,
    OA2208 AS 預算金額,
    OA2210 AS 實際金額,
    OA2215 AS 是否開發票,
    OA2216 AS 發票號碼,
    OA2219 AS 發票金額,
    OA2223 AS 是否已收款,
    OA2227 AS 收款金額,
    AddUser AS 建檔人
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 IN (N'TEST001',N'TEST002',N'TEST003')
ORDER BY OA2202, OA2203;

-- 4-4. 各合約收支款現況統計
PRINT '';
PRINT '--- 各合約收支款現況統計 ---';

-- TEST001
PRINT '';
PRINT '=== TEST001（維護合約）收支統計 ===';
SELECT 
    N'已開發票合計' AS 統計項目,
    ISNULL(SUM(OA2219), 0) AS 金額
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2215 = N'Y'
UNION ALL
SELECT 
    N'已收款合計',
    ISNULL(SUM(OA2227), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2223 = N'Y'
UNION ALL
SELECT 
    N'應收帳款（已開未收）',
    ISNULL(SUM(OA2219), 0) - ISNULL(SUM(OA2227), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2215 = N'Y' AND OA2223 = N'N'
UNION ALL
SELECT 
    N'收入合計（預算）',
    ISNULL(SUM(OA2208), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2204 = N'I'
UNION ALL
SELECT 
    N'支出合計（預算）',
    ISNULL(SUM(OA2208), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST001' AND OA2204 = N'E';

-- TEST002
PRINT '';
PRINT '=== TEST002（買賣合約）收支統計 ===';
SELECT 
    N'已開發票合計' AS 統計項目,
    ISNULL(SUM(OA2219), 0) AS 金額
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2215 = N'Y'
UNION ALL
SELECT 
    N'已收款合計',
    ISNULL(SUM(OA2227), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2223 = N'Y'
UNION ALL
SELECT 
    N'應付帳款（已估未付）',
    ISNULL(SUM(OA2210), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2204 = N'E' AND OA2223 = N'N'
UNION ALL
SELECT 
    N'收入合計（實際）',
    ISNULL(SUM(OA2210), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2204 = N'I'
UNION ALL
SELECT 
    N'支出合計（實際）',
    ISNULL(SUM(OA2210), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST002' AND OA2204 = N'E';

-- TEST003
PRINT '';
PRINT '=== TEST003（租用合約）收支統計 ===';
SELECT 
    N'已開發票合計' AS 統計項目,
    ISNULL(SUM(OA2219), 0) AS 金額
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2215 = N'Y'
UNION ALL
SELECT 
    N'已收款合計',
    ISNULL(SUM(OA2227), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2223 = N'Y'
UNION ALL
SELECT 
    N'應收帳款（已開未收）',
    ISNULL(SUM(OA2219), 0) - ISNULL(SUM(OA2227), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2215 = N'Y' AND OA2223 = N'N'
UNION ALL
SELECT 
    N'收入合計（預算）',
    ISNULL(SUM(OA2208), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2204 = N'I'
UNION ALL
SELECT 
    N'支出合計（預算）',
    ISNULL(SUM(OA2208), 0)
FROM OA22 WITH(NOLOCK)
WHERE OA2201 = @CompId AND OA2202 = N'TEST003' AND OA2204 = N'E';

PRINT '';
PRINT '============================================================';
PRINT 'vOAM15 完整測試資料建表完成';
PRINT '============================================================';
GO
```

---

## 三、測試資料一覽表

### 3-1. OA20 合約主檔（3 筆）

| 合約代號 | 客戶編號 | 合約類別 | 合約總價(含稅) | 狀態 | 備註 |
|---------|---------|---------|-------------|------|------|
| TEST001 | C001 | M（維護） | 1,050,000 | Active | 年度 ERP 系統維護合約 |
| TEST002 | C002 | S（買賣） | 660,000 | Completed | 伺服器與網路設備採購 |
| TEST003 | C003 | R（租用） | 840,000 | Active | 影印機租用合約 |

### 3-2. OA21 產品/服務（共 6 筆）

| 合約 | 產品ID | 產品類別 | 銷售單價(含稅) | 保固/維護日 | 租用日 |
|------|--------|---------|-------------|------------|--------|
| TEST001 | PROD001 | MA（維護） | 500,000 | 2025/01/01~2025/12/31（維護） | — |
| TEST001 | PROD002 | SW（軟體） | 300,000 | — | — |
| TEST002 | PROD101 | HW（硬體） | 450,000 | 2025/04/01~2026/03/31（保固） | — |
| TEST002 | PROD102 | HW（硬體） | 150,000 | 2025/04/01~2026/03/31（保固） | — |
| TEST003 | PROD201 | RE（租用） | 200,000 | — | 2025/04/01~2028/03/31 |
| TEST003 | PROD202 | CU（耗材） | 60,000 | — | 2025/04/01~2028/03/31 |

### 3-3. OA22 收支明細（共 9 筆）

| 合約 | 序號 | 收支別 | 收支項目 | 預算金額 | 實際金額 | 發票 | 收款 |
|------|------|--------|---------|---------|---------|------|------|
| TEST001 | 001 | I（收入） | Q1維護費 | 500,000 | 500,000 | ✅ INV-2025-001 | ✅ 525,000 |
| TEST001 | 002 | E（支出） | Q1人事成本 | 200,000 | 180,000 | ❌ | ❌ |
| TEST001 | 003 | I（收入） | Q2軟體授權費 | 300,000 | 0 | ✅ INV-2025-002（應收） | ❌ |
| TEST002 | 001 | I（收入） | 伺服器主機與網路設備 | 660,000 | 660,000 | ✅ INV-2025-010 | ✅ 660,000 |
| TEST002 | 002 | E（支出） | 設備採購成本 | 350,000 | 350,000 | ❌ | 支票支付 |
| TEST002 | 003 | I（收入） | 設備安裝設定服務費 | 50,000 | 50,000 | ✅ INV-2025-011 | ✅ 52,500 |
| TEST003 | 001 | I（收入） | 影印機月租金 | 200,000 | 200,000 | ✅ INV-2025-020 | ✅ 210,000 |
| TEST003 | 002 | E（支出） | 碳粉匣與零件更換 | 60,000 | 45,000 | ✅ INV-2025-023 | ✅ 47,250 |
| TEST003 | 003 | I（收入） | 年度保養服務費 | 120,000 | 0 | ✅ INV-2025-030（應收） | ❌ |

---

## 四、執行流程檢查清單

```
[ ] 1. 由 DBA 執行 一-1「OA20 新增欄位」
[ ] 2. 由 DBA 執行 一-2「OA21 新增欄位」
[ ] 3. 由 DBA 確認並執行 一-3「OA22 建表」（草案）
[ ] 4. 由 DBA 或測試人員執行 第二節「完整測試資料 SQL」
[ ] 5. 執行 第四節「驗證查詢」，確認 3+6+9 = 18 筆資料寫入正確
[ ] 6. 開啟前端測試：
       a. 啟動 Live Server
       b. 開啟 settings.html 初始化 sessionStorage
       c. 開啟 http://localhost:23531/Security/SP/MobileOA/vOAM15.html
       d. 桌面端/平板端/手機端測試
[ ] 7. 前端查詢測試：搜尋 TEST001 / TEST002 / TEST003
[ ] 8. 前端明細測試：點擊各合約，檢查 產品/服務 Tab
[ ] 9. 前端新增測試：新增一筆產品（使用 Mock 或後端 API）
[ ] 10. Git commit（如有修改）
```

---

## 五、相關程式檔案路徑

| 層級 | 檔案 | 路徑 |
|------|------|------|
| DAL | DaOA20.cs | `D:\GUIMobile\Packages\SRC\GUIStd.DAL.OA\DAO\Private\DaOA20.cs` |
| DAL | DaOA21.cs | `D:\GUIMobile\Packages\SRC\GUIStd.DAL.OA\DAO\Private\DaOA21.cs` |
| BLL | BlOA20.cs | `D:\GUIMobile\Packages\SRC\GUIStd.BLL.OA\Private\BlOA20.cs` |
| 前端 | voam15.js | `D:\GUINet\WebRWD\GUIVueBA\statics\dev\js\private\mobileoa\voam15\voam15.js` |
| 前端 | voam15-filter.js | `D:\GUINet\WebRWD\GUIVueBA\statics\dev\js\private\mobileoa\voam15\voam15-filter.js` |
| 前端 | voam15-detail.js | `D:\GUINet\WebRWD\GUIVueBA\statics\dev\js\private\mobileoa\voam15\voam15-detail.js` |
| 建表 SQL | OA_vOAM15_CreateTestTables.sql | `D:\GUINet\WebRWD\GUIVueBA\docs\Test\OA_vOAM15_CreateTestTables.sql` |
| 欄位 SQL | SQL_OA20_OA21_新增欄位.sql | `D:\GUIMobile\WebCoreAPI\MGUIBAAPI\MGUIBAAPI\docs\SQL_OA20_OA21_新增欄位.sql` |
