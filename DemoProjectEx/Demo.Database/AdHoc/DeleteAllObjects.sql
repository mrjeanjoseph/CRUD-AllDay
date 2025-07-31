-- Drop all foreign key constraints
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += N'ALTER TABLE [' + s.name + '].[' + t.name + '] DROP CONSTRAINT [' + f.name + '];'
FROM sys.foreign_keys AS f
INNER JOIN sys.tables AS t ON f.parent_object_id = t.object_id
INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id;
EXEC sp_executesql @sql;

-- Drop all views
SET @sql = N'';
SELECT @sql += N'DROP VIEW [' + s.name + '].[' + v.name + '];'
FROM sys.views AS v
INNER JOIN sys.schemas AS s ON v.schema_id = s.schema_id;
EXEC sp_executesql @sql;

-- Drop all stored procedures
SET @sql = N'';
SELECT @sql += N'DROP PROCEDURE [' + s.name + '].[' + p.name + '];'
FROM sys.procedures AS p
INNER JOIN sys.schemas AS s ON p.schema_id = s.schema_id;
EXEC sp_executesql @sql;

-- Drop all functions
SET @sql = N'';
SELECT @sql += N'DROP FUNCTION [' + s.name + '].[' + o.name + '];'
FROM sys.objects AS o
INNER JOIN sys.schemas AS s ON o.schema_id = s.schema_id
WHERE o.type IN ('FN', 'IF', 'TF');
EXEC sp_executesql @sql;

-- Drop all tables
SET @sql = N'';
SELECT @sql += N'DROP TABLE [' + s.name + '].[' + t.name + '];'
FROM sys.tables AS t
INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id;
EXEC sp_executesql @sql;
