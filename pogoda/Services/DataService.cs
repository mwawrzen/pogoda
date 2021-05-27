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
        public static Weather? CurrentData = null;

        static List<StationMeasurement> measurements;

        public static void DisplayAllData(Weather[] weather)
        {
            foreach (Weather w in weather)
            {
                Console.WriteLine($@"Stacja: {w.stacja}

    ID stacji:            {w.id_stacji}
    Data pomiaru:         {w.data_pomiaru}
    Godzina pomiaru:      {w.godzina_pomiaru}:00
    Temperatura:          {w.temperatura} C
    Prędkość wiatru:      {w.predkosc_wiatru} B (skala Beauforta)
    Wilgotność względna:  {w.wilgotnosc_wzgledna} (?)
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
    Prędkość wiatru:      {weather.predkosc_wiatru} B (skala Beauforta)
    Wilgotność względna:  {weather.wilgotnosc_wzgledna} (?)
    Ciśnienie:            {weather.cisnienie} hPa
");
        }

        public static async Task<Weather[]> GetWeather()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await client.GetAsync(API);
            var resultArray = await responseMessage.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<Weather[]>(resultArray);

            DataList.AddRange(weather);

            DisplayAllData(weather);

            return weather;
        }

        /*public static void SaveDataToDatabase()
        {
            var dbData = DatabaseService.Get();

            foreach (var w in DataList)
            {
                if (w.stacja == CurrentData.stacja) continue;

                double temperature = Convert.ToDouble(w.temperatura.Replace(".", ","));
                double cisnienie;
                if (w.cisnienie == null)
                    cisnienie = 0;
                else
                    cisnienie = Convert.ToDouble(w.cisnienie.Replace(".", ","));
                double wilgotnosc = Convert.ToDouble(w.wilgotnosc_wzgledna.Replace(".", ","));
                double predkosc = Convert.ToDouble(w.predkosc_wiatru.Replace(".", ","));

                StationMeasurement stationMeasurement = new StationMeasurement
                {
                    station = w.stacja,
                    temperature = new List<Measurement>(),
                    pressure = new List<Measurement>(),
                    moisture = new List<Measurement>(),
                    windSpeed = new List<Measurement>()
                };

                Measurement temperatureMeasurement = new Measurement { day = w.data_pomiaru, value = temperature };
                Measurement pressureMeasurement = new Measurement { day = w.data_pomiaru, value = cisnienie };
                Measurement moistureMeasurement = new Measurement { day = w.data_pomiaru, value = wilgotnosc };
                Measurement windSpeedMeasurement = new Measurement { day = w.data_pomiaru, value = predkosc };

                stationMeasurement.temperature.Add(temperatureMeasurement);
                stationMeasurement.pressure.Add(pressureMeasurement);
                stationMeasurement.moisture.Add(moistureMeasurement);
                stationMeasurement.windSpeed.Add(windSpeedMeasurement);

                dbData.Add(stationMeasurement);
            }

            DatabaseService.Save(dbData);
        }*/


        public static dynamic GetWeatherById(string id)
        {
            foreach(var w in DataList)
            {
                if (w.id_stacji == id)
                {
                    CurrentData = w;
                    return w;
                }
            }

            return false;
        }

        public static dynamic GetWeatherByName(string name)
        {
            foreach (var w in DataList)
            {
                if (w.stacja == name)
                {
                    CurrentData = w;
                    return w;
                }
                    
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
