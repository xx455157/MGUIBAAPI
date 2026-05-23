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


