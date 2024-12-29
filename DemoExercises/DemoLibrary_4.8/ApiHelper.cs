using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DemoLibrary48 {
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

		public static void InitializeClient() {
			ApiClient = new HttpClient();
			ApiClient.BaseAddress = new Uri("https://xkcd.com/"); // Base URL for the API

			ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			ApiClient.DefaultRequestHeaders.UserAgent.ParseAdd("API Client 1.0"); // User agent for the API
		}
	}
}
