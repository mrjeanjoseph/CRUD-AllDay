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



DROP DATABASE IF EXISTS [MySimpleDatabase];
CREATE DATABASE MySimpleDatabase;
GO

USE [master];
GO
ALTER DATABASE [MySimpleDatabase] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
DROP DATABASE [MySimpleDatabase];
GO