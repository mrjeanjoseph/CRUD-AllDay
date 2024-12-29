using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace DemoLibrary48 {
    public class SunProcessor {

        public static async Task<SunModel> LoadSunData() {

            string url = "https://api.sunrise-sunset.org/json?lat=36.101666174210806&lng=-78.4578606123972";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url)) {

                if (response.IsSuccessStatusCode) {

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    SunResultModel result = JsonConvert.DeserializeObject<SunResultModel>(jsonResponse);

                    //SunResultModel result = await response.Content.ReadAsAsync<SunResultModel>();

                    return result.Results;

                } else {

                    throw new Exception(response.ReasonPhrase);

                }
            }
        }
    }
}
