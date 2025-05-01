using CCMS.DesktopService.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CCMS.DesktopService.Api
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private readonly IAPIHelper _apiHelper;
        public SaleEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Log Successful call?
                    return;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
