using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
    }

    public class SampleAPIClient
    {
        private const string ApiUri = "http:/localhost:2023/api/cars";
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUri);

                response.EnsureSuccessStatusCode();
                //return await response.Content.ReadAsAsync<IEnumerable<Car>>();

                var responseTwo = await client.GetAsync(ApiUri)
                    .ConfigureAwait(continueOnCapturedContext: false);

                responseTwo.EnsureSuccessStatusCode();
                //return await response.Content.ReadAsAsync<IEnumerable<Car>>()
                //.ConfigureAwait(continueOnCapturedContext: false);

                return null;
            }
        }
    }


}
