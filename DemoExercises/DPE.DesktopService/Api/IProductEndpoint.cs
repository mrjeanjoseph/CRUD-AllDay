using DPE.DesktopService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPE.DesktopService.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}