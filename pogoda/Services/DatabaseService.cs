using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using pogoda.Models;

namespace pogoda.Services
{
    class DatabaseService
    {
        static string Path = "database.json";
        static List<StationMeasurement>? measurementsFromFile;

        public static bool CheckIsEmpty()
        {
            string? data = File.ReadAllText(Path);

            if (data == "")
                return true;
            return false;
        }

        public static void Initialize()
        {
            var dbData = new List<StationMeasurement>();

            foreach (var w in DataService.DataList)
            {
                double temperature = Convert.ToDouble(w.temperatura.Replace(".", ","));
                double wilgotnosc = Convert.ToDouble(w.wilgotnosc_wzgledna.Replace(".", ","));
                double predkosc = Convert.ToDouble(w.predkosc_wiatru.Replace(".", ","));
                double cisnienie = 0;

                if (w.cisnienie != null)
                    cisnienie = Convert.ToDouble(w.cisnienie.Replace(".", ","));

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

            Save(dbData);

            Console.WriteLine("Pomyślnie zainicjowano bazę danych, liczba elementów: " + dbData.Count);
        }

        public static void Save(List<StationMeasurement> data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(Path, jsonString);

            Console.WriteLine("Pomyślnie zapisano dane");
        }

        public static List<StationMeasurement>? Get()
        {
            string? data = File.ReadAllText(Path);
            measurementsFromFile = JsonSerializer.Deserialize<List<StationMeasurement>>(data);

            Console.WriteLine("Pomyślnie pobrano dane");

            return measurementsFromFile;
        }
    }
}
