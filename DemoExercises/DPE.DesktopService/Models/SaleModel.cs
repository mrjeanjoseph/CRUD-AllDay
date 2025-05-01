using System.Collections.Generic;

namespace DPE.DesktopService.Models
{
    public class SaleModel
    {
        public List<SaleDetailModel> SaleDetails { get; set; }
            = new List<SaleDetailModel>();
    }
}
