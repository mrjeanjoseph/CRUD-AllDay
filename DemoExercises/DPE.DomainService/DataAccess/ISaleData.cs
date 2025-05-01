using DPE.DomainService.Models;
using System.Collections.Generic;

namespace DPE.DomainService.DataAccess
{
    public interface ISaleData
    {
        List<SaleReportModel> GetSaleReport();
        void SaveSale(SaleModel saleInfo, string cashierId);
    }
}