using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLibrary48 {
    public class ComicProcessor {
        //public int MaxComicNumber { get; set; }

        public async Task<ComicModel> LoadComic(int comicNumber = 0) {

            string url;
            if (comicNumber > 0) url = $"https://xkcd.com/{comicNumber}/info.0.json";
            else url = "https://xkcd.com/info.0.json";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url)) {

                if (response.IsSuccessStatusCode) {

                    //ComicModel comic2 = await response.Content.ReadAsAsync<ComicModel>();

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ComicModel comic = JsonConvert.DeserializeObject<ComicModel>(jsonResponse);

                    //if(comicNumber == 0) MaxComicNumber = comic.Num;

                    return comic;
                } else {

                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
