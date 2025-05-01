using CCMS.DesktopService.Models;
using System.Threading.Tasks;

namespace CCMS.DesktopService.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}