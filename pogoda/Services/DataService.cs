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

        static void ShowAllData(Weather[] weather)
        {
            foreach(Weather w in weather)
            {
                Console.WriteLine($@"Stacja: {w.stacja}

    ID stacji:          {w.id_stacji}
    Data pomiaru:       {w.data_pomiaru}
    Godzina pomiaru:    {w.godzina_pomiaru}:00
    Temperatura:        {w.temperatura} C
    Prędkość wiatru:    {w.predkosc_wiatru} km/h (?)
    Kierunek wiatru:    {w.kierunek_wiatru} stopni (?)
    Suma opadu:         {w.suma_opadu} mm/rok (?)
    Ciśnienie:          {w.cisnienie} hPa
");
            }
        }

        static void ShowData(Weather weather)
        {
            Console.WriteLine($@"Stacja: {weather.stacja}

    ID stacji:          {weather.id_stacji}
    Data pomiaru:       {weather.data_pomiaru}
    Godzina pomiaru:    {weather.godzina_pomiaru}:00
    Temperatura:        {weather.temperatura} C
    Prędkość wiatru:    {weather.predkosc_wiatru} km/h (?)
    Kierunek wiatru:    {weather.kierunek_wiatru} stopni (?)
    Suma opadu:         {weather.suma_opadu} mm/rok (?)
    Ciśnienie:          {weather.cisnienie} hPa");
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
                ShowData(weather);
            }

            else
            {
                var responseMessage = await client.GetAsync(API);
                var resultArray = await responseMessage.Content.ReadAsStringAsync();
                var weather = JsonConvert.DeserializeObject<Weather[]>(resultArray);
                ShowAllData(weather);
            }
        }
    }
}
