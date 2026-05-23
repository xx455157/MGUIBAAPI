-- =====================================================
-- vOAM15 合約管理系統 - 資料表 SQL 建議檔
-- 適用于 MSSQL 資料庫
-- 日期：2026-04-29
-- 說明：OA22-OA25 為本版新增加的資料表
--       OA20/OA21 為既有資料表，如有需要可一併執行建立
-- =====================================================

-- =====================================================
-- 合約主檔 (OA20) - 既有資料表，如不存在請執行
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA20')
BEGIN
    CREATE TABLE OA20 (
        OA2001 NVARCHAR(8) NOT NULL,       -- 公司代號
        OA2002 NVARCHAR(20) NOT NULL,      -- 合約ID
        OA2003 NVARCHAR(20) NOT NULL,       -- 客戶ID
        OA2004 NVARCHAR(1) DEFAULT 'N',    -- 新/舊客戶 (N=新, O=舊)
        OA2005 NVARCHAR(10),                -- 合約終了日期
        OA2006 NVARCHAR(1) DEFAULT 'M',    -- 合約類型 (M=維護, S=買賣, R=租用)
        OA2007 DECIMAL(18,2) DEFAULT 0,   -- 合約總價(未稅)
        OA2008 DECIMAL(18,2) DEFAULT 0,   -- 合約總價(含稅)
        OA2009 DECIMAL(18,2) DEFAULT 0,   -- 外包成本預算(含稅)
        OA2010 NVARCHAR(20) DEFAULT 'Active', -- 合約狀態
        OA2011 NVARCHAR(MAX),              -- 備註
        OA2012 NVARCHAR(10),                -- 展期控制日期
        OA2013 NVARCHAR(10),                -- 合約成立日期
        OA2014 NVARCHAR(20),                -- 目前業務
        OA2015 NVARCHAR(500),               -- 合約文件URL
        AddDate DATETIME DEFAULT GETDATE(),
        AddUser NVARCHAR(20),
        UpdateDate DATETIME,
        UpdateUser NVARCHAR(20),
        PRIMARY KEY (OA2001, OA2002)
    );
    CREATE INDEX IX_OA20_CompId ON OA20(OA2001);
    CREATE INDEX IX_OA20_CustomerId ON OA20(OA2001, OA2003);
    CREATE INDEX IX_OA20_Status ON OA20(OA2001, OA2010);
END
GO

-- =====================================================
-- 產品/服務資料 (OA21) - 既有資料表，如不存在請執行
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA21')
BEGIN
    CREATE TABLE OA21 (
        OA2101 NVARCHAR(8) NOT NULL,       -- 公司代號
        OA2102 NVARCHAR(20) NOT NULL,       -- 合約ID
        OA2103 NVARCHAR(20) NOT NULL,       -- 產品/服務ID
        OA2104 DECIMAL(18,2) DEFAULT 0,   -- 銷售單價(含稅)
        OA2105 DECIMAL(18,2) DEFAULT 0,   -- 外包成本單價
        OA2106 NVARCHAR(10),                -- 保固開始日期
        OA2107 NVARCHAR(10),                -- 保固終了日期
        OA2108 DECIMAL(18,2) DEFAULT 0,   -- 預計維護金額
        OA2109 NVARCHAR(20),                -- 目前PM
        OA2110 NVARCHAR(50),               -- 產品類別
        OA2112 NVARCHAR(10),                -- 維護合約起始日
        OA2113 NVARCHAR(10),                -- 維護合約終了日
        OA2114 NVARCHAR(10),                -- 租用合約起始日
        OA2115 NVARCHAR(10),                -- 租用合約終了日
        AddDate DATETIME DEFAULT GETDATE(),
        AddUser NVARCHAR(20),
        UpdateDate DATETIME,
        UpdateUser NVARCHAR(20),
        PRIMARY KEY (OA2101, OA2102, OA2103)
    );
    CREATE INDEX IX_OA21_ContractId ON OA21(OA2101, OA2102);
END
GO

-- =====================================================
-- 收支預算 (OA22) - 新增
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA22')
BEGIN
    CREATE TABLE OA22 (
        OA2201 NVARCHAR(8) NOT NULL,       -- 公司代號
        OA2202 NVARCHAR(20) NOT NULL,       -- 合約ID
        OA2203 INT NOT NULL,                -- 序號
        OA2204 NVARCHAR(20),                -- 會計科目代碼
        OA2205 NVARCHAR(100),               -- 會計科目名稱
        OA2206 NVARCHAR(20),                -- 期別名稱 (如 2025-Q1)
        OA2207 NVARCHAR(1) DEFAULT 'I',    -- 收支別 (I=收入, E=支出)
        OA2208 NVARCHAR(50),               -- 收支類別
        OA2209 DECIMAL(18,2) DEFAULT 0,   -- 預算金額
        OA2210 NVARCHAR(10),                -- 預計日期
        OA2211 DECIMAL(18,2) DEFAULT 0,   -- 實際金額
        OA2212 NVARCHAR(10),                -- 實際日期
        OA2213 NVARCHAR(20),                -- 業務員ID
        OA2214 NVARCHAR(50),               -- 業務員姓名
        OA2215 NVARCHAR(50),               -- 傳票號碼
        OA2216 NVARCHAR(MAX),              -- 備註
        AddDate DATETIME DEFAULT GETDATE(),
        AddUser NVARCHAR(20),
        UpdateDate DATETIME,
        UpdateUser NVARCHAR(20),
        PRIMARY KEY (OA2201, OA2202, OA2203)
    );
    CREATE INDEX IX_OA22_ContractId ON OA22(OA2201, OA2202);
END
GO

-- =====================================================
-- 發票 (OA23) - 新增
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA23')
BEGIN
    CREATE TABLE OA23 (
        OA2301 NVARCHAR(8) NOT NULL,       -- 公司代號
        OA2302 NVARCHAR(20) NOT NULL,       -- 合約ID
        OA2303 INT NOT NULL,                -- 序號
        OA2304 NVARCHAR(30),                -- 發票號碼
        OA2305 NVARCHAR(10),                -- 發票日期
        OA2306 NVARCHAR(1) DEFAULT 'S',   -- 發票類型 (S=銷項, P=進項)
        OA2307 DECIMAL(18,2) DEFAULT 0,   -- 發票金額(含稅)
        OA2308 NVARCHAR(MAX),              -- 發票說明
        OA2309 NVARCHAR(1) DEFAULT 'N',     -- 是否已收款 (Y/N)
        OA2310 DECIMAL(18,2) DEFAULT 0,   -- 已收款金額
        OA2311 NVARCHAR(50),               -- 傳票號碼
        OA2312 NVARCHAR(MAX),              -- 備註
        AddDate DATETIME DEFAULT GETDATE(),
        AddUser NVARCHAR(20),
        UpdateDate DATETIME,
        UpdateUser NVARCHAR(20),
        PRIMARY KEY (OA2301, OA2302, OA2303)
    );
    CREATE INDEX IX_OA23_ContractId ON OA23(OA2301, OA2302);
END
GO

-- =====================================================
-- 收款 (OA24) - 新增
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA24')
BEGIN
    CREATE TABLE OA24 (
        OA2401 NVARCHAR(8) NOT NULL,       -- 公司代號
        OA2402 NVARCHAR(20) NOT NULL,       -- 合約ID
        OA2403 INT NOT NULL,                -- 序號
        OA2404 NVARCHAR(30),                -- 匯款ID
        OA2405 NVARCHAR(30),                -- 支票號碼
        OA2406 NVARCHAR(10),                -- 收款日期
        OA2407 DECIMAL(18,2) DEFAULT 0,   -- 收款金額
        OA2408 NVARCHAR(30),                -- 發票號碼
        OA2409 NVARCHAR(50),               -- 傳票號碼
        OA2410 NVARCHAR(MAX),              -- 備註
        AddDate DATETIME DEFAULT GETDATE(),
        AddUser NVARCHAR(20),
        UpdateDate DATETIME,
        UpdateUser NVARCHAR(20),
        PRIMARY KEY (OA2401, OA2402, OA2403)
    );
    CREATE INDEX IX_OA24_ContractId ON OA24(OA2401, OA2402);
END
GO

-- =====================================================
-- 收支款現況 (OA25) - 新增
-- =====================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OA25')
BEGIN
    CREATE TABLE OA25 (
        OA2501 NVARCHAR(8) NOT NULL,       -- 公司代號
        OA2502 NVARCHAR(20) NOT NULL,       -- 合約ID
        OA2503 INT NOT NULL,                -- 序號
        OA2504 NVARCHAR(20),                -- 現況類型
                                          --   received = 已收款
                                          --   AR = 應收帳款
                                          --   BL = 銀行借款
                                          --   accrualExpense = 應計支出
        OA2505 NVARCHAR(100),               -- 現況說明
        OA2506 DECIMAL(18,2) DEFAULT 0,   -- 金額
        OA2507 NVARCHAR(10),                -- 日期
        OA2508 NVARCHAR(MAX),              -- 描述
        OA2509 NVARCHAR(50),               -- 傳票號碼
        AddDate DATETIME DEFAULT GETDATE(),
        AddUser NVARCHAR(20),
        UpdateDate DATETIME,
        UpdateUser NVARCHAR(20),
        PRIMARY KEY (OA2501, OA2502, OA2503)
    );
    CREATE INDEX IX_OA25_ContractId ON OA25(OA2501, OA2502);
END
GO

-- =====================================================
-- 收支款現況統計視圖 (建議性) - 方便前端快速取得結算數據
-- =====================================================
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vOA25_Summary')
    DROP VIEW vOA25_Summary;
GO

CREATE VIEW vOA25_Summary AS
SELECT
    OA2501 AS CompId,
    OA2502 AS ContractId,
    SUM(CASE WHEN OA2504 = 'received' THEN OA2506 ELSE 0 END) AS ReceivedAmount,
    SUM(CASE WHEN OA2504 = 'AR' THEN OA2506 ELSE 0 END) AS ArAmount,
    SUM(CASE WHEN OA2504 = 'BL' THEN OA2506 ELSE 0 END) AS BlAmount,
    SUM(CASE WHEN OA2504 = 'accrualExpense' THEN OA2506 ELSE 0 END) AS AccrualExpenseAmount
FROM OA25 WITH(NOLOCK)
GROUP BY OA2501, OA2502;
GO

PRINT 'vOAM15 SQL Schema Setup Complete.';
GO
