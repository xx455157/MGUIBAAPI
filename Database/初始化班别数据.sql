-- =============================================
-- 班别配置 - 数据库初始化脚本
-- 说明：使用系统代码表存储班别数据
-- HelpType: 'SH' (Shift)
-- 创建日期：2024-10-30
-- =============================================

USE [YourDatabaseName]
GO

-- =============================================
-- 步骤 1: 检查是否已存在班别数据
-- =============================================

IF NOT EXISTS (SELECT 1 FROM [dbo].[Codes] WHERE [HelpType] = 'SH')
BEGIN
    PRINT '开始插入默认班别数据...'

    -- =============================================
    -- 步骤 2: 插入默认班别数据
    -- =============================================

    INSERT INTO [dbo].[Codes] ([HelpType], [Id], [Name], [SortOrder], [IsActive], [Memo])
    VALUES
        -- 全部选项（用于查询）
        ('SH', '', '全部', 0, 1, '查询所有班别'),
        
        -- 标准班别
        ('SH', 'A', 'A班', 1, 1, '早班 06:00-14:00'),
        ('SH', 'B', 'B班', 2, 1, '中班 14:00-22:00'),
        ('SH', 'C', 'C班', 3, 1, '晚班 22:00-06:00'),
        ('SH', 'D', 'D班', 4, 1, '夜班 00:00-08:00');

    PRINT '✅ 默认班别数据插入成功！'
    
    -- 显示插入的数据
    SELECT 
        [HelpType] AS '代码类型',
        [Id] AS '班别代码',
        [Name] AS '班别名称',
        [SortOrder] AS '排序',
        [IsActive] AS '启用',
        [Memo] AS '说明'
    FROM [dbo].[Codes]
    WHERE [HelpType] = 'SH'
    ORDER BY [SortOrder];
END
ELSE
BEGIN
    PRINT '⚠️ 班别数据已存在，跳过插入。'
    
    -- 显示现有数据
    SELECT 
        [HelpType] AS '代码类型',
        [Id] AS '班别代码',
        [Name] AS '班别名称',
        [SortOrder] AS '排序',
        [IsActive] AS '启用',
        [Memo] AS '说明'
    FROM [dbo].[Codes]
    WHERE [HelpType] = 'SH'
    ORDER BY [SortOrder];
END
GO

-- =============================================
-- 步骤 3: 验证数据
-- =============================================

-- 检查班别数量
DECLARE @Count INT
SELECT @Count = COUNT(*) FROM [dbo].[Codes] WHERE [HelpType] = 'SH'

IF @Count >= 5
BEGIN
    PRINT '✅ 验证通过：共有 ' + CAST(@Count AS VARCHAR) + ' 个班别'
END
ELSE
BEGIN
    PRINT '⚠️ 警告：只有 ' + CAST(@Count AS VARCHAR) + ' 个班别，可能数据不完整'
END
GO

-- =============================================
-- 可选步骤：创建独立的班别表（更灵活的方案）
-- =============================================

/*
-- 如果需要更复杂的功能，可以创建独立的班别表
-- 取消以下注释来创建独立表

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shifts]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Shifts]
    (
        [Code] NVARCHAR(1) NOT NULL,            -- 班别代码（主键）
        [Name] NVARCHAR(20) NOT NULL,           -- 班别名称
        [SortOrder] INT NOT NULL DEFAULT 99,    -- 排序顺序
        [IsActive] BIT NOT NULL DEFAULT 1,      -- 是否启用
        [Description] NVARCHAR(100),            -- 说明
        [StartTime] TIME,                       -- 开始时间（可选）
        [EndTime] TIME,                         -- 结束时间（可选）
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [CreatedBy] NVARCHAR(50),
        [UpdatedBy] NVARCHAR(50),
        
        CONSTRAINT [PK_Shifts] PRIMARY KEY CLUSTERED ([Code] ASC)
    );

    PRINT '✅ Shifts 表创建成功'

    -- 插入默认数据
    INSERT INTO [dbo].[Shifts] 
        ([Code], [Name], [SortOrder], [IsActive], [Description], [StartTime], [EndTime])
    VALUES
        ('A', 'A班', 1, 1, '早班', '06:00:00', '14:00:00'),
        ('B', 'B班', 2, 1, '中班', '14:00:00', '22:00:00'),
        ('C', 'C班', 3, 1, '晚班', '22:00:00', '06:00:00'),
        ('D', 'D班', 4, 1, '夜班', '00:00:00', '08:00:00');

    PRINT '✅ 默认班别数据插入成功'
END
GO

-- 创建索引
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shifts_SortOrder')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Shifts_SortOrder] ON [dbo].[Shifts]
    (
        [SortOrder] ASC,
        [IsActive] ASC
    );
    PRINT '✅ 索引创建成功'
END
GO
*/

-- =============================================
-- 使用示例查询
-- =============================================

-- 查询所有启用的班别（按排序顺序）
SELECT 
    [Id] AS 'code',
    [Name] AS 'name',
    [SortOrder] AS 'sortOrder',
    [IsActive] AS 'isActive',
    [Memo] AS 'description'
FROM [dbo].[Codes]
WHERE [HelpType] = 'SH' 
    AND [IsActive] = 1
    AND [Id] <> ''  -- 排除「全部」选项
ORDER BY [SortOrder];
GO

-- 查询包含「全部」选项的班别列表
SELECT 
    [Id] AS 'code',
    [Name] AS 'name',
    [SortOrder] AS 'sortOrder',
    [IsActive] AS 'isActive',
    [Memo] AS 'description'
FROM [dbo].[Codes]
WHERE [HelpType] = 'SH' 
    AND [IsActive] = 1
ORDER BY [SortOrder];
GO

PRINT '============================='
PRINT '脚本执行完成！'
PRINT '============================='
GO



















