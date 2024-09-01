using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Console;

namespace VillaAG.ConsoleApp {

    internal class Program {

        static void Main(string[] args) {

            WriteLine("Welcome to Villa Agency Global!");

            WriteLine("Enter Username:");
            var username = ReadLine();
            WriteLine("Enter Password:");
            var password = ReadLine();

            var client = new HttpClient();

            //setup initial authentication request
            var authRequest = new HttpRequestMessage() {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:2025/Account/Login"),
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("Username", "username"),
                    new KeyValuePair<string, string>("Password", "password"),
                })
            };

            //Attempt to authenticate
            var authResponse = client.SendAsync(authRequest).Result;
            IEnumerable<string> values;
            authResponse.Headers.TryGetValues("Set-Cookie", out values);
            if (null == values || string.IsNullOrEmpty(values.First())) {
                WriteLine("Username and password must be equal");
                ReadLine();
                return;
            }

            var cookie = values.First();

            //setup request to retrieve data from the server
            var request = new HttpRequestMessage() {
                RequestUri = new Uri($"http://localhost:2025/customer/get/"),
            };

            //assign cookie
            request.Headers.Add("Cookie", cookie);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.SendAsync(request).Result;
            WriteLine("Customer: {0}", response.Content.ReadAsAsync<Customer>().Result.Name);

            ReadLine();
        }
    }
}
