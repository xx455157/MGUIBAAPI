-- =====================================================
-- vOAM15 程式碼異動後對應的資料庫 SQL 異動指令
-- 適用於 MSSQL
-- 日期：2026-08-09
-- 說明：對應前端 + 後端程式碼異動所需的資料表結構變更
--       執行前請先備份現有資料庫
-- =====================================================

PRINT '開始執行 vOAM15 程式碼異動對應的 SQL 異動...';
GO

-- =====================================================
-- 1. OA20 新增 OA2016 續約模式欄位（修正 BUG-020）
-- =====================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('OA20') AND name = 'OA2016'
)
BEGIN
    ALTER TABLE OA20 ADD OA2016 NVARCHAR(20) DEFAULT '待續簽約';
    PRINT '已新增 OA20.OA2016（續約模式）';
END
ELSE
BEGIN
    PRINT 'OA20.OA2016 已存在，略過';
END
GO

-- =====================================================
-- 2. OA21 新增 OA2111 產品/服務名稱欄位（修正 BUG-002）
-- =====================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('OA21') AND name = 'OA2111'
)
BEGIN
    ALTER TABLE OA21 ADD OA2111 NVARCHAR(100);
    PRINT '已新增 OA21.OA2111（產品/服務名稱）';
END
ELSE
BEGIN
    PRINT 'OA21.OA2111 已存在，略過';
END
GO

-- =====================================================
-- 3. OA20 索引補強（補強查詢效能）
-- =====================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('OA20') AND name = 'IX_OA20_CreateDate'
)
BEGIN
    CREATE INDEX IX_OA20_CreateDate ON OA20(OA2001, OA2013);
    PRINT '已新增 OA20 合約日期索引（IX_OA20_CreateDate）';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('OA20') AND name = 'IX_OA20_CustomerName_QueryText'
)
BEGIN
    -- 此索引需先 JOIN A08；如下游用 A08.A0802 做模糊查詢頻繁可考慮
    -- 因 A0802 為 A08 表欄位，索引建立在 A08 上更有效
    IF NOT EXISTS (
        SELECT 1 FROM sys.indexes
        WHERE object_id = OBJECT_ID('A08') AND name = 'IX_A08_A0802'
    )
    BEGIN
        CREATE INDEX IX_A08_A0802 ON A08(A0802);
        PRINT '已新增 A08 客戶名稱索引（IX_A08_A0802）';
    END
END
GO

-- =====================================================
-- 4. 補齊 OA20 預設值（確保新合約有合理的狀態/類型預設）
-- =====================================================
-- 註：此段僅在尚未補預設的環境執行；若 DB 已存在資料，請以 UPDATE 處理
IF OBJECT_ID('OA20', 'U') IS NOT NULL
BEGIN
    -- 將歷史資料的合約狀態預設為 'Active'（如果為 NULL）
    UPDATE OA20 SET OA2010 = 'Active' WHERE OA2010 IS NULL OR OA2010 = '';
    PRINT '已補齊 OA20.OA2010 預設值（NULL → Active）';

    -- 將歷史資料的合約類型預設為 'M'（維護）（如果為 NULL）
    UPDATE OA20 SET OA2006 = 'M' WHERE OA2006 IS NULL OR OA2006 = '';
    PRINT '已補齊 OA20.OA2006 預設值（NULL → M）';
END
GO

-- =====================================================
-- 5. 驗證 SQL 結果
-- =====================================================
SELECT
    'OA20 OA2016（續約模式）' AS [檢查項目],
    CASE WHEN EXISTS (
        SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('OA20') AND name = 'OA2016'
    ) THEN '✓ 已建立' ELSE '✗ 缺失' END AS [狀態]
UNION ALL
SELECT
    'OA21 OA2111（產品/服務名稱）',
    CASE WHEN EXISTS (
        SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('OA21') AND name = 'OA2111'
    ) THEN '✓ 已建立' ELSE '✗ 缺失' END
UNION ALL
SELECT
    'OA20 OA2013 合約日期索引',
    CASE WHEN EXISTS (
        SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('OA20') AND name = 'IX_OA20_CreateDate'
    ) THEN '✓ 已建立' ELSE '✗ 缺失' END;
GO

PRINT 'vOAM15 SQL 異動完成。請重新發布應用程式後再進行測試。';
GO