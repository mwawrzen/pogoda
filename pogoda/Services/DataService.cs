using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using pogoda.Models;

namespace pogoda.Services
{
    class DataService
    {
        const string API = "https://danepubliczne.imgw.pl/api/data/synop";

        public static void DisplayAllData(Weather[] weather)
        {
            foreach (Weather w in weather)
            {
                Console.WriteLine($@"Stacja: {w.stacja}

    ID stacji:            {w.id_stacji}
    Data pomiaru:         {w.data_pomiaru}
    Godzina pomiaru:      {w.godzina_pomiaru}:00
    Temperatura:          {w.temperatura} C
    Prędkość wiatru:      {w.predkosc_wiatru} km/h (?)
    Kierunek wiatru:      {w.kierunek_wiatru} stopni (?)
    Wilgotność względna:  {w.wilgotnosc_wzgledna} (?)
    Suma opadu:           {w.suma_opadu} mm/rok (?)
    Ciśnienie:            {w.cisnienie} hPa
");
            }
        }

        static void DisplayData(Weather weather)
        {
            Console.WriteLine($@"Stacja: {weather.stacja}

    ID stacji:            {weather.id_stacji}
    Data pomiaru:         {weather.data_pomiaru}
    Godzina pomiaru:      {weather.godzina_pomiaru}:00
    Temperatura:          {weather.temperatura} C
    Prędkość wiatru:      {weather.predkosc_wiatru} km/h (?)
    Kierunek wiatru:      {weather.kierunek_wiatru} stopni (?)
    Wilgotność względna:  {weather.wilgotnosc_wzgledna} (?)
    Suma opadu:           {weather.suma_opadu} mm/rok (?)
    Ciśnienie:            {weather.cisnienie} hPa");
        }

        public static async Task<Weather[]> GetWeather()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await client.GetAsync(API);
            var resultArray = await responseMessage.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather[]>(resultArray);
            DisplayAllData(weather);

            return weather;
        }

        public static async Task<Weather> GetWeatherById(string id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await client.GetAsync($"{API}/id/{id}");
            var resultArray = await responseMessage.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather>(resultArray);
            DisplayData(weather);

            return weather;
        }
    }
}
