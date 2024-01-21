-- SQL Server Backup Scripts
--
-- Scripts provided by MSSQLTips.com are from various contributors. Use links below to learn more about the scripts.
-- 
-- Be careful using any of these scripts. Test all scripts in Test/Dev prior to using in Production environments.
-- Please refer to the disclaimer policy: https://www.mssqltips.com/disclaimer/
-- Please refer to the copyright policy: https://www.mssqltips.com/copyright/
--
-- Note, these scripts are meant to be run individually.
--
-- Have a script to contribute or an update?  Send an email to: tips@mssqltips.com

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: This shows the default SQL Server backup directory.
-- More information: https://www.mssqltips.com/sqlservertip/1966/function-to-return-default-sql-server-backup-folder/
--
USE master
GO
EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'BackupDirectory'

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: Delete all backup and restore history data stored in the msdb database prior to provided date.
-- More information: https://www.mssqltips.com/sqlservertip/1727/purging-msdb-backup-and-restore-history-from-sql-server/
--
USE msdb
GO
EXEC sp_delete_backuphistory '2019-04-02' -- change date as needed in YYYYMMDD format. This example will remove all history data prior to April 2, 2019
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: Delete all backup and restore history data stored in the msdb database for a specific database.
-- More information: https://www.mssqltips.com/sqlservertip/1727/purging-msdb-backup-and-restore-history-from-sql-server/
--
USE msdb
GO
EXEC sp_delete_database_backuphistory 'test1' -- change to your database name.  This example will remove all history for database test1
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: Show the location of backup files for the latest backups.
-- More information: https://www.mssqltips.com/sqlservertip/2960/sql-server-backup-paths-and-file-management/
--
USE msdb
GO
SELECT TOP 20 -- change to get more than 20 backups
  a.server_name,
  a.database_name,
  backup_finish_date,
  CONVERT(int, (a.backup_size / 1024 / 1024)) AS BackupSizeMB,
  CASE a.[type] -- Let's decode the three main types of backup here
    WHEN 'D' THEN 'Full'
    WHEN 'I' THEN 'Differential'
    WHEN 'L' THEN 'Transaction Log'
    ELSE a.[type]
  END AS BackupType,
  -- Build a path to the backup
  '\\' +
  -- lets extract the server name out of the recorded server and instance name
  CASE
    WHEN PATINDEX('%\%', a.server_name) = 0 THEN a.server_name
    ELSE SUBSTRING(a.server_name, 1, PATINDEX('%\%', a.server_name) - 1)
  END
  -- then get the drive and path and file information
  + '\' + REPLACE(b.physical_device_name, ':', '$') AS '\\Server\Drive\backup_path\backup_file'
FROM msdb.dbo.backupset a
JOIN msdb.dbo.backupmediafamily b
  ON a.media_set_id = b.media_set_id
--WHERE a.database_name Like 'master%' -- you can use this to filter for a specific database
ORDER BY a.backup_finish_date DESC
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: Show the last time databases had a full backup and the duration of the backup.
-- More information: https://www.mssqltips.com/sqlservertip/1747/how-to-find-out-how-long-a-sql-server-backup-took/
--
USE msdb
GO
DECLARE @dbname sysname
SET @dbname = NULL --set this to be whatever dbname you want or leave as is for all databases
SELECT
  bup.user_name AS [User],
  bup.database_name AS [Database],
  bup.server_name AS [Server],
  bup.backup_start_date AS [Backup Started],
  bup.backup_finish_date AS [Backup Finished],
  CAST((CAST(DATEDIFF(s, bup.backup_start_date, bup.backup_finish_date) AS int)) / 3600 AS varchar) + ' hours, '
  + CAST((CAST(DATEDIFF(s, bup.backup_start_date, bup.backup_finish_date) AS int)) / 60 AS varchar) + ' minutes, '
  + CAST((CAST(DATEDIFF(s, bup.backup_start_date, bup.backup_finish_date) AS int)) % 60 AS varchar) + ' seconds'
  AS [Total Time]
FROM msdb.dbo.backupset bup
WHERE bup.backup_set_id IN ( SELECT MAX(backup_set_id)
                             FROM msdb.dbo.backupset
                             WHERE database_name = ISNULL(@dbname, database_name ) --if no dbname, then return all
                               AND type = 'D' --only interested in the time of last full backup
                             GROUP BY database_name
                           )
/* COMMENT THE NEXT LINE IF YOU WANT ALL BACKUP HISTORY */
AND bup.database_name IN (SELECT name FROM master.dbo.sysdatabases)
ORDER BY bup.database_name
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: Show databases missing a full backup in the last 24 hours.
-- More information: https://www.mssqltips.com/sqlservertip/1601/script-to-retrieve-sql-server-database-backup-history-and-no-backups/
--
-- databases with a full backup older than 24 hours
USE msdb
GO
DECLARE @hour int = 24 -- set to a different value if needed
SELECT
  CONVERT(char(100), SERVERPROPERTY('Servername')) AS Server,
  b.database_name,
  MAX(b.backup_finish_date) AS last_db_backup_date,
  DATEDIFF(hh, MAX(b.backup_finish_date), GETDATE()) AS [Backup Age (Hours)]
FROM msdb.dbo.backupset b
INNER JOIN master.sys.sysdatabases s
  ON s.name = b.database_name
WHERE b.type = 'D'
GROUP BY b.database_name
HAVING (MAX(b.backup_finish_date) < DATEADD(hh, -@hour, GETDATE()))
UNION
--databases without any backup history 
SELECT
  CONVERT(char(100), SERVERPROPERTY('Servername')) AS Server,
  s.NAME AS database_name,
  NULL AS [Last Data Backup Date],
  99999 AS [Backup Age (Hours)]
FROM master.sys.sysdatabases s
LEFT JOIN msdb.dbo.backupset b
  ON s.name = b.database_name
WHERE b.database_name IS NULL
  AND s.name <> 'tempdb'
ORDER BY b.database_name
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: This script will create a full backup for all databases on the SQL Server instance.
-- More information: https://www.mssqltips.com/sqlservertip/1070/simple-script-to-backup-all-sql-server-databases/
--
USE master
GO
DECLARE @name varchar(50)      -- database name  
DECLARE @path varchar(256)     -- path for backup files  
DECLARE @fileName varchar(256) -- filename for backup  
DECLARE @fileDate varchar(20)  -- used for file name

-- specify database backup directory
SET @path = 'C:\Backup\'

-- filename format will be DBname_YYYYDDMM_HHMMSS.BAK
SELECT @fileDate = CONVERT(varchar(20), GETDATE(), 112) + '_' + REPLACE(CONVERT(varchar(20), GETDATE(), 108), ':', '')

DECLARE db_cursor CURSOR READ_ONLY FOR
SELECT name
FROM master.sys.databases
WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb')  -- exclude these databases
AND state = 0 -- database is online
AND is_in_standby = 0 -- database is not read only for log shipping

OPEN db_cursor
FETCH NEXT FROM db_cursor INTO @name

WHILE @@FETCH_STATUS = 0
BEGIN
  SET @fileName = @path + @name + '_' + @fileDate + '.BAK'
  BACKUP DATABASE @name TO DISK = @fileName

  FETCH NEXT FROM db_cursor INTO @name
END

CLOSE db_cursor
DEALLOCATE db_cursor
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: This script will generate a script to create multiple smaller backup files instead of one large backup file for a database.
-- More information: https://www.mssqltips.com/sqlservertip/5668/sql-server-script-to-automatically-split-database-backups-into-multiple-backup-files/
--
USE master
GO
DECLARE @dbName sysname = 'mssqltips'        --enter database name
DECLARE @primaryDrive nchar(3) = 'C:\'       --drive location
DECLARE @backupDir nvarchar(200) = 'Backup\' --backup path 
DECLARE @nParts tinyint = 5                  --number of backup files to create

DECLARE @backupTSQLCmd nvarchar(max)
DECLARE @idx tinyint = 0
SET @idx += 1
SET @backupTSQLCmd = CONCAT('BACKUP DATABASE ', '[', @dbName, '] TO ')

WHILE @idx <= @nParts
BEGIN
  SET @backupTSQLCmd += CONCAT(
  'DISK = ',
  '''',
  @primaryDrive,
  @backupDir,
  @dbName,
  '_',
  RTRIM(LTRIM(STR(@idx))),
  '.BAK',
  '''',
  ', '
  )
  SET @idx += 1
END
SET @backupTSQLCmd = LEFT(@backupTSQLCmd, LEN(@backupTSQLCmd) - 1)
PRINT @backupTSQLCmd
--EXEC (@backupTSQLCmd) -- uncomment this line to execute script
GO

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: This script will show the percentage complete for a backup or restore that is in running.
-- More information: https://www.mssqltips.com/sqlservertip/2343/how-to-monitor-backup-and-restore-progress-in-sql-server/
--
SELECT 
   session_id as SPID, command, a.text AS Query, start_time, percent_complete,
   dateadd(second,estimated_completion_time/1000, getdate()) as estimated_completion_time
FROM sys.dm_exec_requests r 
   CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a 
WHERE r.command in ('BACKUP DATABASE','RESTORE DATABASE') 

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: This script will show when databases were restored.
-- More information: https://www.mssqltips.com/sqlservertip/1860/identify-when-a-sql-server-database-was-restored-the-source-and-backup-date/
--
SELECT 
   [rs].[destination_database_name], 
   [rs].[restore_date], 
   [bs].[backup_start_date], 
   [bs].[backup_finish_date], 
   [bs].[database_name] as [source_database_name], 
   [bmf].[physical_device_name] as [backup_file_used_for_restore]
FROM msdb..restorehistory rs
INNER JOIN msdb..backupset bs ON [rs].[backup_set_id] = [bs].[backup_set_id]
INNER JOIN msdb..backupmediafamily bmf ON [bs].[media_set_id] = [bmf].[media_set_id] 
ORDER BY [rs].[restore_date] DESC

---------------------------------------------------------------------------------------------------------------------------
-- Purpose: This script will show when full, differential and log backups last occured for each database.
-- More information: https://www.mssqltips.com/sqlservertip/5721/script-to-obtain-most-recent-sql-server-backup-information-for-all-databases/
--
WITH MostRecentBackups
   AS(
      SELECT 
         database_name AS [Database],
         MAX(bus.backup_finish_date) AS LastBackupTime,
         CASE bus.type
            WHEN 'D' THEN 'Full'
            WHEN 'I' THEN 'Differential'
            WHEN 'L' THEN 'Transaction Log'
         END AS Type
      FROM msdb.dbo.backupset bus
      WHERE bus.type <> 'F'
      GROUP BY bus.database_name,bus.type
   ),
   BackupsWithSize
   AS(
      SELECT mrb.*, (SELECT TOP 1 CONVERT(DECIMAL(10,4), b.backup_size/1024/1024/1024) AS backup_size FROM msdb.dbo.backupset b WHERE [Database] = b.database_name AND LastBackupTime = b.backup_finish_date) AS [Backup Size]
      FROM MostRecentBackups mrb
   )
   
   SELECT 
      SERVERPROPERTY('ServerName') AS Instance, 
      d.name AS [Database],
      d.state_desc AS State,
      d.recovery_model_desc AS [Recovery Model],
      bf.LastBackupTime AS [Last Full],
      DATEDIFF(DAY,bf.LastBackupTime,GETDATE()) AS [Time Since Last Full (in Days)],
      bf.[Backup Size] AS [Full Backup Size],
      bd.LastBackupTime AS [Last Differential],
      DATEDIFF(DAY,bd.LastBackupTime,GETDATE()) AS [Time Since Last Differential (in Days)],
      bd.[Backup Size] AS [Differential Backup Size],
      bt.LastBackupTime AS [Last Transaction Log],
      DATEDIFF(MINUTE,bt.LastBackupTime,GETDATE()) AS [Time Since Last Transaction Log (in Minutes)],
      bt.[Backup Size] AS [Transaction Log Backup Size]
   FROM sys.databases d
   LEFT JOIN BackupsWithSize bf ON (d.name = bf.[Database] AND (bf.Type = 'Full' OR bf.Type IS NULL))
   LEFT JOIN BackupsWithSize bd ON (d.name = bd.[Database] AND (bd.Type = 'Differential' OR bd.Type IS NULL))
   LEFT JOIN BackupsWithSize bt ON (d.name = bt.[Database] AND (bt.Type = 'Transaction Log' OR bt.Type IS NULL))
   WHERE d.name <> 'tempdb' AND d.source_database_id IS NULL