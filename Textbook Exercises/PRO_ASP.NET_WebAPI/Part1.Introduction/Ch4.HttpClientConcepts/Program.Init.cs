using System.Collections.Generic;
using System.Net.Http.Extensions.Compression.Core.Models;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Net;
using System.Linq;

namespace HttpClientConcepts
{
    public class ProgramInit
    {
        static string apiUrl = "https://example.com/api/customer";
        static string clientUri = "https://github.com/mrjeanjoseph/TSQL_Exercises/blob/main/README.md";
        static string loginUrl = "https://localhost:2023/Account/Login";
        static void RunToMain(string[] args)
        {

            ListingFourSeventeen_HttpClient();
            Console.ReadLine();

            ListingFourEleven_StringContent();
            ListingFourTen_StringContent();
            ListingFourThree_RequestHeaders();
            Console.ReadLine();
        }

        static void ListingFourTwentyThree()
        {
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();

            var client = new HttpClient();

            //Setup inital authentication request
            var authRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(loginUrl),
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Username", "username"),
                    new KeyValuePair<string, string>("Password", "Password"),
                })
            };

            var authResponse = client.SendAsync(authRequest).Result;
            IEnumerable<string> values;
            authResponse.Headers.TryGetValues("Set-Cookies", out values);
            if (null == values || string.IsNullOrEmpty(values.First()))
            {
                Console.WriteLine("Username and password must equal.");
                Console.ReadLine();
                return;
            }

            var cookie = values.First();

            //setup request to retrieve data from the server
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(apiUrl + "/get/1"),
            };

            //assign cookie
            request.Headers.Add("Cookie", cookie);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.SendAsync(request).Result;
            Console.WriteLine("Customer: {0}", response.Content.ReadAsAsync<Customer>().Result.FullNameLegal);
        }

        static void ListingFourTwentyOne()
        {
            var customer = new Customer() { FullNameLegal = "Kervens Jean-Joseph" };
            var client = new HttpClient();
            client.PostAsync("https://example.com/api/customer", customer,
                new JsonMediaTypeFormatter()).ContinueWith(tc =>
                {
                    if (tc.Result.StatusCode == HttpStatusCode.Created)
                    {
                        tc.Result.Content.ReadAsAsync<Customer>().ContinueWith(
                            tc2 => Console.WriteLine("Id: {0}, FullNameLegal: {1}",
                            tc2.Result.Id, tc2.Result.FullNameLegal));
                    }
                });
            var anotherOptionForResponse = client.PostAsJsonAsync(apiUrl, customer).Result;
        }

        static void ListingFourTwenty_HttpClient()
        {
            var client = new HttpClient();
            client.GetAsync("")
                .ContinueWith(tr => tr.Result.Content.ReadAsAsync<Customer>()
                .ContinueWith(tc => Console.WriteLine("Id: {0}, Name: {1}",
                tc.Result.Id, tc.Result.FullNameLegal)));
        }

        static void ListingFourSeventeen_HttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.GetStringAsync(clientUri).ContinueWith(t =>
            Console.WriteLine(t.Result));
        }

        static void ListingFourEleven_StringContent()
        {
            var httpContent = new StringContent(@"Lorem Ipsum");
            httpContent.ReadAsStringAsync().ContinueWith((t) =>
            {
                Console.WriteLine(t.Result);
                Console.WriteLine("Intial Size: {0} bytes", httpContent.Headers.ContentLength);
            });

            var compressedContent = new CompressedContent(httpContent, null);
            compressedContent.ReadAsStringAsync().ContinueWith((t) =>
            {
                var result = t.Result;
                Console.WriteLine("Intial Size: {0} bytes", compressedContent.Headers.ContentLength);
            });
        }

        static void ListingFourTen_StringContent()
        {
            var stringContent = new StringContent("Hello C-Sharp");
            stringContent.ReadAsStringAsync().ContinueWith((t) => Console.WriteLine(t.Result));
            Console.ReadLine();
        }

        static void ListingFourThree_RequestHeaders()
        {
            var siteUrl = "https://example.com/";
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(siteUrl));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
            request.Headers.Add("X-Name", "DeepVue CRUD");
            Console.WriteLine(request.ToString());
        }

    }

}
