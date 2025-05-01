using CCMS.DomainService.Models;
using System.Collections.Generic;

namespace CCMS.DomainService.DataAccess
{
    public interface ISaleData
    {
        List<SaleReportModel> GetSaleReport();
        void SaveSale(SaleModel saleInfo, string cashierId);
    }
}