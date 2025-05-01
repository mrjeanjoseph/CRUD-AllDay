using DPE.DomainService.Models;
using System.Collections.Generic;

namespace DPE.DomainService.DataAccess
{
    public interface IInventoryData
    {
        List<InventoryModel> GetInventory();
        void SaveInventoryRecord(InventoryModel item);
    }
}