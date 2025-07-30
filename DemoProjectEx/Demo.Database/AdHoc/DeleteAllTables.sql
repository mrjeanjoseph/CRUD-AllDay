-- Script to disable all foreign keys and delete all tables in the CCMS.Data database

USE [Demo.Database];
GO

-- Script to disable all foreign keys in the current database
DECLARE @sql NVARCHAR(MAX) = N'';

-- Generate ALTER TABLE statements to disable all foreign keys
SELECT @sql += 'ALTER TABLE [' + SCHEMA_NAME(schema_id) + '].[' + OBJECT_NAME(parent_object_id) + '] NOCHECK CONSTRAINT [' + name + '];' + CHAR(13)
FROM sys.foreign_keys;

-- Execute the generated SQL
EXEC sp_executesql @sql;

-- Script to delete all tables in the current database
SET @sql = N'';

-- Generate DROP TABLE statements for all user tables
SELECT @sql += 'DROP TABLE [' + SCHEMA_NAME(schema_id) + '].[' + name + '];' + CHAR(13)
FROM sys.tables;

-- Execute the generated SQL
EXEC sp_executesql @sql;

-- Script to retrieve server details
SELECT 
    SERVERPROPERTY('MachineName') AS MachineName,
    SERVERPROPERTY('ServerName') AS ServerName,
    SERVERPROPERTY('Edition') AS Edition,
    SERVERPROPERTY('ProductVersion') AS ProductVersion,
    SERVERPROPERTY('ProductLevel') AS ProductLevel,
    SERVERPROPERTY('EngineEdition') AS EngineEdition,
    SERVERPROPERTY('IsClustered') AS IsClustered,
    SERVERPROPERTY('Collation') AS Collation;
