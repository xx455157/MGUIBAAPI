-- =====================================================
-- vOAM15 合約管理系統 - 完整重置 SQL
-- 適用於 MSSQL 資料庫
-- 日期：2026-08-06
-- 規範：依 HT2000 系列表文件規範
--       - 建檔 8 個共用欄位：[表碼]0001~[表碼]0008，NOT NULL
--       - 業務欄位：[表碼][業務序號]，NOT NULL
--       - Type=N 使用 NUMERIC(25,4)
--       - 日期格式 NVARCHAR(10) YYYY/MM/DD，預設 ''
--       - 時間格式 NVARCHAR(10) HH:MM:SS，預設 ''
--       - 所有欄位 NOT NULL + DEFAULT
--       - 不可逆：DROP TABLE 直接刪除
-- =====================================================

USE [YourDatabaseName];
GO

-- ===========================================
-- 1. 刪除舊表（不可逆）
-- 順序：子表先刪，再刪主表（參照完整性）
-- ===========================================
IF OBJECT_ID('dbo.OA25', 'U') IS NOT NULL DROP TABLE dbo.OA25;
IF OBJECT_ID('dbo.OA24', 'U') IS NOT NULL DROP TABLE dbo.OA24;
IF OBJECT_ID('dbo.OA23', 'U') IS NOT NULL DROP TABLE dbo.OA23;
IF OBJECT_ID('dbo.OA22', 'U') IS NOT NULL DROP TABLE dbo.OA22;
IF OBJECT_ID('dbo.OA21', 'U') IS NOT NULL DROP TABLE dbo.OA21;
IF OBJECT_ID('dbo.OA20', 'U') IS NOT NULL DROP TABLE dbo.OA20;
GO

-- ===========================================
-- 2. OA20 合約主檔
-- PK: OA2001 + OA2002
-- ===========================================
CREATE TABLE dbo.OA20 (
    -- 建檔 8 個共用稽核欄位 (NOT NULL + DEFAULT)
    OA20001    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔日期 YYYY/MM/DD
    OA20002    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔時間 HH:MM:SS
    OA20003    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔工作站
    OA20004    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔人
    OA20005    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改日期 YYYY/MM/DD
    OA20006    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改時間 HH:MM:SS
    OA20007    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改工作站
    OA20008    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改人

    -- 業務欄位 (NOT NULL)
    OA2001     NVARCHAR(8)   NOT NULL DEFAULT '',   -- 公司代號
    OA2002     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 合約ID
    OA2003     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 客戶ID
    OA2004     NVARCHAR(1)   NOT NULL DEFAULT '',  -- 新/舊客戶 (N=新/O=舊)
    OA2005     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 合約終了日期 YYYY/MM/DD
    OA2006     NVARCHAR(1)   NOT NULL DEFAULT '', -- 合約類型 (M=維護/S=買賣/R=租用)
    OA2007     NUMERIC(25,4) NOT NULL DEFAULT 0,    -- 合約總價(未稅)
    OA2008     NUMERIC(25,4) NOT NULL DEFAULT 0,    -- 合約總價(含稅)
    OA2009     NUMERIC(25,4) NOT NULL DEFAULT 0,    -- 外包成本預算(含稅)
    OA2010     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 合約狀態
    OA2011     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 備註
    OA2012     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 展期控制日期 YYYY/MM/DD
    OA2013     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 合約成立日期 YYYY/MM/DD
    OA2014     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 目前業務
    OA2015     NVARCHAR(500) NOT NULL DEFAULT '',  -- 合約文件URL

    CONSTRAINT PK_OA20 PRIMARY KEY (OA2001, OA2002)
);
CREATE INDEX OA20A1 ON dbo.OA20(OA2001);
CREATE INDEX OA20A2 ON dbo.OA20(OA2001, OA2003);
CREATE INDEX OA20A3 ON dbo.OA20(OA2001, OA2010);
GO

-- ===========================================
-- 3. OA21 產品/服務資料
-- PK: OA2101 + OA2102 + OA2103
-- ===========================================
CREATE TABLE dbo.OA21 (
    -- 建檔 8 個共用稽核欄位 (NOT NULL + DEFAULT)
    OA21001    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21002    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21003    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21004    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21005    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21006    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21007    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA21008    NVARCHAR(10)  NOT NULL DEFAULT '',

    -- 業務欄位 (NOT NULL)
    OA2101     NVARCHAR(8)   NOT NULL DEFAULT '',   -- 公司代號
    OA2102     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 合約ID
    OA2103     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 產品/服務ID
    OA2104     NUMERIC(25,4) NOT NULL DEFAULT 0,    -- 銷售單價(含稅)
    OA2105     NUMERIC(25,4) NOT NULL DEFAULT 0,    -- 外包成本單價
    OA2106     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 保固開始日期 YYYY/MM/DD
    OA2107     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 保固終了日期 YYYY/MM/DD
    OA2108     NUMERIC(25,4) NOT NULL DEFAULT 0,    -- 預計維護金額
    OA2109     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 目前PM
    OA2110     NVARCHAR(50)  NOT NULL DEFAULT '',   -- 產品類別
    OA2111     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 備註
    OA2112     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 維護合約起始日 YYYY/MM/DD
    OA2113     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 維護合約終了日 YYYY/MM/DD
    OA2114     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 租用合約起始日 YYYY/MM/DD
    OA2115     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 租用合約終了日 YYYY/MM/DD

    CONSTRAINT PK_OA21 PRIMARY KEY (OA2101, OA2102, OA2103)
);
CREATE INDEX OA21A1 ON dbo.OA21(OA2101, OA2102);
GO

-- ===========================================
-- 4. OA22 收支明細（整併 原 OA23 發票 + OA24 收款 + OA25 收支款現況）
-- PK: OA2201 + OA2202 + OA2203
-- 業務欄位: OA2201~OA2231 (共 31 欄)
-- ===========================================
CREATE TABLE dbo.OA22 (
    -- 建檔 8 個共用稽核欄位 (NOT NULL + DEFAULT)
    OA22001    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔日期 YYYY/MM/DD
    OA22002    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔時間 HH:MM:SS
    OA22003    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔工作站
    OA22004    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 建檔人
    OA22005    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改日期 YYYY/MM/DD
    OA22006    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改時間 HH:MM:SS
    OA22007    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改工作站
    OA22008    NVARCHAR(10)  NOT NULL DEFAULT '',   -- 更改人

    -- 業務欄位 (NOT NULL) -- 共 31 個業務欄位
    -- 收支類別
    OA2201     NVARCHAR(8)   NOT NULL DEFAULT '',   -- 公司代號
    OA2202     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 合約ID
    OA2203     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 序號
    OA2204     NVARCHAR(1)   NOT NULL DEFAULT 'I', -- 收支別 I=收入/E=支出
    OA2205     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 收支類別
    OA2206     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 收支科目
    OA2207     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 收支項目名稱
    OA2208     NUMERIC(25,4) NOT NULL DEFAULT 0,   -- 預算金額
    OA2209     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 預算日期 YYYY/MM/DD
    OA2210     NUMERIC(25,4) NOT NULL DEFAULT 0,   -- 實際金額
    OA2211     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 實際日期 YYYY/MM/DD
    OA2212     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 目前業務
    OA2213     NVARCHAR(30)  NOT NULL DEFAULT '',   -- GL傳票號碼(收支傳票)
    OA2214     NVARCHAR(100) NOT NULL DEFAULT '',  -- 備註
    -- 發票欄位 (原 OA23)
    OA2215     NVARCHAR(1)   NOT NULL DEFAULT 'N', -- 是否已開發票 Y/N
    OA2216     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 發票號碼
    OA2217     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 發票日期 YYYY/MM/DD
    OA2218     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 發票類別
    OA2219     NUMERIC(25,4) NOT NULL DEFAULT 0,   -- 發票金額
    OA2220     NVARCHAR(100) NOT NULL DEFAULT '',  -- 發票明細
    OA2221     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 發票GL傳票號碼
    OA2222     NVARCHAR(10)  NOT NULL DEFAULT '',   -- FILLER
    -- 收款欄位 (原 OA24)
    OA2223     NVARCHAR(1)   NOT NULL DEFAULT 'N', -- 是否已收款 Y/N
    OA2224     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 匯款編號
    OA2225     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 支票號碼
    OA2226     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 收款日期 YYYY/MM/DD
    OA2227     NUMERIC(25,4) NOT NULL DEFAULT 0,   -- 收款金額
    OA2228     NVARCHAR(30)  NOT NULL DEFAULT '',   -- 收款GL傳票號碼
    OA2229     NVARCHAR(10)  NOT NULL DEFAULT '',   -- FILLER
    -- 保留欄位
    OA2230     NUMERIC(25,4) NOT NULL DEFAULT 0,   -- FILLER (NUMERIC)
    OA2231     NUMERIC(25,4) NOT NULL DEFAULT 0,   -- FILLER (NUMERIC)

    CONSTRAINT PK_OA22 PRIMARY KEY (OA2201, OA2202, OA2203)
);
CREATE INDEX OA22A1 ON dbo.OA22(OA2201, OA2202);
CREATE INDEX OA22A2 ON dbo.OA22(OA2201, OA2202, OA2204);
CREATE INDEX OA22A3 ON dbo.OA22(OA2201, OA2202, OA2215);
CREATE INDEX OA22A4 ON dbo.OA22(OA2201, OA2202, OA2223);
GO

-- ===========================================
-- 5. OA23 客戶聯絡人
-- PK: OA2301 + OA2302 + OA2303
-- ===========================================
CREATE TABLE dbo.OA23 (
    -- 建檔 8 個共用稽核欄位 (NOT NULL + DEFAULT)
    OA23001    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23002    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23003    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23004    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23005    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23006    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23007    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA23008    NVARCHAR(10)  NOT NULL DEFAULT '',

    -- 業務欄位 (NOT NULL)
    OA2301     NVARCHAR(8)   NOT NULL DEFAULT '',   -- 公司代號
    OA2302     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 客戶ID
    OA2303     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 聯絡人序號
    OA2304     NVARCHAR(40)  NOT NULL DEFAULT '',   -- 聯絡人姓名
    OA2305     NVARCHAR(40)  NOT NULL DEFAULT '',   -- 職稱
    OA2306     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 電話
    OA2307     NVARCHAR(100) NOT NULL DEFAULT '',  -- 電子郵件
    OA2308     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 備註

    CONSTRAINT PK_OA23 PRIMARY KEY (OA2301, OA2302, OA2303)
);
CREATE INDEX OA23A1 ON dbo.OA23(OA2301, OA2302);
GO

-- ===========================================
-- 6. OA24 業務變更歷史
-- PK: OA2401 + OA2402 + OA2403
-- ===========================================
CREATE TABLE dbo.OA24 (
    -- 建檔 8 個共用稽核欄位 (NOT NULL + DEFAULT)
    OA24001    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24002    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24003    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24004    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24005    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24006    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24007    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA24008    NVARCHAR(10)  NOT NULL DEFAULT '',

    -- 業務欄位 (NOT NULL)
    OA2401     NVARCHAR(8)   NOT NULL DEFAULT '',   -- 公司代號
    OA2402     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 合約ID
    OA2403     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 變更序號
    OA2404     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 變更日期 YYYY/MM/DD
    OA2405     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 變更類型
    OA2406     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 變更前內容
    OA2407     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 變更後內容
    OA2408     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 變更原因

    CONSTRAINT PK_OA24 PRIMARY KEY (OA2401, OA2402, OA2403)
);
CREATE INDEX OA24A1 ON dbo.OA24(OA2401, OA2402);
GO

-- ===========================================
-- 7. OA25 合約附件
-- PK: OA2501 + OA2502 + OA2503
-- ===========================================
CREATE TABLE dbo.OA25 (
    -- 建檔 8 個共用稽核欄位 (NOT NULL + DEFAULT)
    OA25001    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25002    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25003    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25004    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25005    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25006    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25007    NVARCHAR(10)  NOT NULL DEFAULT '',
    OA25008    NVARCHAR(10)  NOT NULL DEFAULT '',

    -- 業務欄位 (NOT NULL)
    OA2501     NVARCHAR(8)   NOT NULL DEFAULT '',   -- 公司代號
    OA2502     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 合約ID
    OA2503     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 附件序號
    OA2504     NVARCHAR(200) NOT NULL DEFAULT '',  -- 附件名稱
    OA2505     NVARCHAR(500) NOT NULL DEFAULT '',  -- 附件URL
    OA2506     NVARCHAR(20)  NOT NULL DEFAULT '',   -- 附件類型
    OA2507     NVARCHAR(10)  NOT NULL DEFAULT '',   -- 上傳日期 YYYY/MM/DD
    OA2508     NVARCHAR(MAX) NOT NULL DEFAULT '',  -- 備註

    CONSTRAINT PK_OA25 PRIMARY KEY (OA2501, OA2502, OA2503)
);
CREATE INDEX OA25A1 ON dbo.OA25(OA2501, OA2502);
GO

-- ===========================================
-- 8. 驗證：列出所有建立的表
-- ===========================================
SELECT name AS TableName,
       CAST(CASE WHEN OBJECTPROPERTY(OBJECT_ID, 'TableHasPrimaryKey') = 1 THEN 'Yes' ELSE 'No' END AS VARCHAR(10)) AS HasPK
FROM   sys.tables
WHERE  name LIKE 'OA2%'
ORDER BY name;
GO
