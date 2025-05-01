using System.Collections.Generic;

namespace CCMS.DesktopService.Models
{
    public class SaleModel
    {
        public List<SaleDetailModel> SaleDetails { get; set; }
            = new List<SaleDetailModel>();
    }
}
