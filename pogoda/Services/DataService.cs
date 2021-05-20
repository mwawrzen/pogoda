using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using pogoda.Models;

namespace pogoda.Services
{
    class DataService
    {
        const string API = "https://danepubliczne.imgw.pl/api/data/synop";

        static void ShowDataObject(Weather[] weather)
        {
            foreach(Weather w in weather)
            {
                Console.WriteLine($"{w.stacja}");
            }
        }

        public static async void GetWeather(string id = "")
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (id != "")
            {
                var responseMessage = await client.GetAsync($"{API}/id/{id}");
                var resultArray = await responseMessage.Content.ReadAsStringAsync();
                var weather = JsonConvert.DeserializeObject<Weather>(resultArray);
                Console.WriteLine(weather);
            }

            else
            {
                var responseMessage = await client.GetAsync(API);
                var resultArray = await responseMessage.Content.ReadAsStringAsync();
                var weather = JsonConvert.DeserializeObject<Weather[]>(resultArray);
                ShowDataObject(weather);
            }
        }
    }
}
