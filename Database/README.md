# Database Scripts 资料库

这个目录用于存放数据库相关的 SQL 脚本，方便 AI 分析和版本控制。

## 目录结构

```
Database/
├── StoredProcedures/   存储过程
│   ├── HTL/           酒店模块
│   ├── PY/            其他模块
│   └── GUI/           通用模块
├── Triggers/          触发器
├── Views/             视图
└── Functions/         函数
```

## 如何导出 SQL 脚本

### 方法 1：使用 SSMS（推荐）

1. 打开 SQL Server Management Studio
2. 连接到数据库服务器：`HT2000_2022`
3. 右键点击数据库 `ht2000_水美`
4. 选择 **Tasks → Generate Scripts**
5. 选择要导出的对象类型：
   - ✅ Stored Procedures
   - ✅ Triggers
   - ✅ Views
   - ✅ Functions
6. 设置导出选项：
   - **Script for Server Version**: 选择你的 SQL Server 版本
   - **Script CREATE**: Yes
7. 选择输出位置，指向这个目录
8. 点击 **Next** 完成导出

### 方法 2：使用 T-SQL 脚本

```sql
-- 导出单个存储过程
EXEC sp_helptext 'YourStoredProcedureName'

-- 查看所有存储过程
SELECT name FROM sys.procedures WHERE type = 'P' ORDER BY name

-- 查看所有触发器
SELECT name FROM sys.triggers ORDER BY name
```

## 命名规范建议

- **存储过程**: `sp_ModuleName_ActionDescription.sql`
  - 例如: `sp_HTL_GetRoomInfo.sql`
- **触发器**: `tr_TableName_Action.sql`
  - 例如: `tr_Rooms_AfterUpdate.sql`
- **视图**: `vw_Description.sql`
  - 例如: `vw_RoomOccupancy.sql`
- **函数**: `fn_Description.sql`
  - 例如: `fn_CalculatePrice.sql`

## 使用说明

导出的脚本将被 AI 自动分析，帮助你：
- ✅ 理解数据库逻辑
- ✅ 优化性能
- ✅ 检查依赖关系
- ✅ 发现潜在问题

## 注意事项

⚠️ **安全提示**：
- 不要在脚本中包含敏感数据（密码、密钥等）
- 如果提交到 Git，确保添加 .gitignore
- 定期更新脚本，保持与数据库同步

---

最后更新: 2025-10-08