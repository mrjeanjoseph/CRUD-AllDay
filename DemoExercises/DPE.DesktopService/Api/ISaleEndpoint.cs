using DPE.DesktopService.Models;
using System.Threading.Tasks;

namespace DPE.DesktopService.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}