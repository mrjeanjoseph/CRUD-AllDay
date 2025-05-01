using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CCMS.DomainService.UserData
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool isClosed = false;
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;

        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }


        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connStringName)
        {
            string connectionString = GetConnectionString(connStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connStringName)
        {
            string connectionString = GetConnectionString(connStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // An error is thrown here
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            }
        }

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            isClosed = false;

        }

        public List<T> LoadDataForTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure,
                transaction: _transaction).ToList();

            return rows;
        }

        public void SaveDataForTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure,
                transaction: _transaction);
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            isClosed = true;
        }

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            isClosed = true;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Commit transaction failed in the dispose method");
                }
            }

            _transaction = null;
            _connection = null;
        }
    }
}
