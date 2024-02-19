USE [InterstellarTransport];
GO

SELECT OBJECT_SCHEMA_NAME(i.object_id) AS SchemaName, OBJECT_NAME(i.object_id) TableName, 
	i.name,
	ips.partition_number, 
	ips.index_type_desc, 
	ips.index_level,
	ips.avg_fragmentation_in_percent,
	ips.page_count,
	ips.avg_page_space_used_in_percent
	FROM sys.indexes i 
		INNER JOIN sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'detailed') ips 
			ON ips.object_id = i.object_id AND ips.index_id = i.index_id
	WHERE i.name = 'idx_ShipmentDetails_ShipmentID';
GO

RETURN;

/*
	ALTER INDEX [ALL | <index name>]
	ON TableName
	REBUILD
	WITH
	PAD_INDEX = {ON | OFF}
	FILLFACTOR = fillfactor
	SORT_IN_TEMPDB = {ON | OFF}
	ONLINE = {ON [ ( <low_priority_lock_wait> ) ] | OFF }
	RESUMABLE = {ON | OFF }
	MAX_DURATION = <time> [MINUTES]
	MAXDOP = max_degree_of_parallelism
*/


--Example
ALTER INDEX idx_ShipmentDetails_ShipmentID ON DBO.ShipmentDetails REBUILD;
GO

/*
	GitHub: https://github.com/olahallengren/sql-server-maintenance-solution

	Backup: https://ola.hallengren.com/sql-server-backup.html
	Integrity Check: https://ola.hallengren.com/sql-server-integrity-check.html
	Index and Statistics Maintenance: https://ola.hallengren.com/sql-server-index-and-statistics-maintenance.html
*/