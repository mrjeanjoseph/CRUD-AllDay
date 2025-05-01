using CCMS.DomainService.Models;
using CCMS.DomainService.UserData;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CCMS.DomainService.DataAccess
{
    public class InventoryData : IInventoryData
    {
        private readonly IConfiguration _config;
        private readonly ISqlDataAccess _sqlData;

        public InventoryData(IConfiguration config, ISqlDataAccess sqlData)
        {
            _config = config;
            _sqlData = sqlData;
        }

        public List<InventoryModel> GetInventory()
        {
            return _sqlData.LoadData<InventoryModel, dynamic>("dbo.spInventoryGetAll", new { }, "CCMSConn");
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            _sqlData.SaveData("dbo.spInventoryInsert", item, "CCMSConn");
        }
    }
}
