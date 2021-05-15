using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace pogoda.Services
{
    public class Weather
    {
        public string id_stacji { get; set; }
        public string stacja { get; set; }
        public string data_pomiaru { get; set; }
        public string godzina_pomiaru { get; set; }
        public string temperatura { get; set; }
        public string predkosc_wiatru { get; set; }
        public string wilgotnosc_wzgledna { get; set; }
        public string cisnienie { get; set; }
    }

    class DataService
    {
        static HttpClient client = new HttpClient();

        static void ShowWeather(Weather weather)
        {
            Console.WriteLine($"Nazwa stacji: {weather.stacja}");
        }

        static async Task<Weather> GetWeatherAsync(string path)
        {
            Weather weather = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                weather = await response.Content.ReadAsAsync<Weather>();
            }
            return weather;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            string api = "https://danepubliczne.imgw.pl/api/data/synop/";

            client.BaseAddress = new Uri(api);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the weather
                Weather weather = await GetWeatherAsync($"{api}/id/12600");
                ShowWeather(weather);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
