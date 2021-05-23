using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using pogoda.Models;

namespace pogoda.Services
{
    class DataService
    {
        const string API = "https://danepubliczne.imgw.pl/api/data/synop";
        public static List<Weather> DataList = new List<Weather>();

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

        public static void DisplayData(Weather weather)
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

            DataList.AddRange(weather);

            return weather;
        }


        public static dynamic GetWeatherById(string id)
        {
            foreach(var w in DataList)
            {
                if (w.id_stacji == id)
                    return w;
            }

            return false;
        }

        public static dynamic GetWeatherByName(string name)
        {
            foreach (var w in DataList)
            {
                if (w.stacja == name)
                    return w;
            }

            return false;
        }

        public static dynamic SearchStation(string word)
        {
            List<Weather> data = new List<Weather>();
            string phrase = word.ToLower();

            foreach (var w in DataList)
            {
                if (w.stacja.ToLower().Contains(phrase))
                    data.Add(w);
            }

            return data;
        }
    }
}
