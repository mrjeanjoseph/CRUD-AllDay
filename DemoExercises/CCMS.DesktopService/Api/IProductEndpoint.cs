using CCMS.DesktopService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCMS.DesktopService.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}