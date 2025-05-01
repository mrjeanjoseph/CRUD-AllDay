using System.Collections.Generic;

namespace CCMS.DomainService.UserData
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connStringName);
        List<T> LoadDataForTransaction<T, U>(string storedProcedure, U parameters);
        void RollBackTransaction();
        void SaveData<T>(string storedProcedure, T parameters, string connStringName);
        void SaveDataForTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}