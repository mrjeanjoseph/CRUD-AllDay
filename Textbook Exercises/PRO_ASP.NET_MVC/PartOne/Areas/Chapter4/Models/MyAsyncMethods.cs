using System.Net.Http;
using System.Threading.Tasks;

namespace EssentialFeatures.Models {
    public class MyAsyncMethods {
        public static async Task<long?> GetPageLength() {
            HttpClient client = new HttpClient();

            var httpMessage = await client.GetAsync("www.laskawobas.com");

            //we could do other things here while we are waiting
            //for the HTTP request to complete
            return httpMessage.Content.Headers.ContentLength;
        }
        public static Task<long?> GetPageLength_OldMethod() {
            HttpClient client = new HttpClient();

            var httpTask = client.GetAsync("www.laskawobas.com");

            //we could do other things here while we are waiting
            //for the HTTP request to complete

            return httpTask.ContinueWith((Task<HttpResponseMessage> antecent) => {
                return antecent.Result.Content.Headers.ContentLength;
            });
        }
    }
}