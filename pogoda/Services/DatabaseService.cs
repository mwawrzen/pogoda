using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using pogoda.Models;
using pogoda.ViewModels;

namespace pogoda.Services
{
    class DatabaseService
    {
        static string Path = "database.json";
        static List<StationMeasurement>? measurementsFromFile;

        static double Rep(string str)
        {
            double result = Convert.ToDouble(str.Replace(".",","));
            return result;
        }

        public static void SaveDataToDatabase()
        {
            string? data = File.ReadAllText(Path);

            if (data == "")
            {
                Initialize();
            }
                
            else
            {
                List<StationMeasurement> measurementList = Get();
                List<Weather> currentDataList = DataService.DataList;

                for (int i = 0; i < measurementList.Count; i++)
                {
                    Weather weatherObject = currentDataList[i];

                    double temperature = Rep(weatherObject.temperatura);
                    double moisture = Rep(weatherObject.wilgotnosc_wzgledna);
                    double windSpeed = Rep(weatherObject.predkosc_wiatru);
                    double pressure = 0;
                    
                    if (weatherObject.cisnienie != null)
                        pressure = Rep(weatherObject.cisnienie);

                    Measurement temperatureMeasurement = new Measurement { day = weatherObject.data_pomiaru, value = temperature };
                    Measurement pressureMeasurement = new Measurement { day = weatherObject.data_pomiaru, value = pressure };
                    Measurement moistureMeasurement = new Measurement { day = weatherObject.data_pomiaru, value = moisture };
                    Measurement windSpeedMeasurement = new Measurement { day = weatherObject.data_pomiaru, value = windSpeed };

                    measurementList[i].pressure.Add(pressureMeasurement);
                    measurementList[i].moisture.Add(moistureMeasurement);
                    measurementList[i].windSpeed.Add(windSpeedMeasurement);
                }

                Save(measurementList);
            }
        }

        public static void Initialize()
        {
            var dbData = new List<StationMeasurement>();

            foreach (var w in DataService.DataList)
            {
                double temperature = Rep(w.temperatura);
                double moisture = Rep(w.wilgotnosc_wzgledna);
                double windSpeed = Rep(w.predkosc_wiatru);
                double pressure = 0;

                if (w.cisnienie != null)
                    pressure = Rep(w.cisnienie);

                StationMeasurement stationMeasurement = new StationMeasurement
                {
                    station = w.stacja,
                    temperature = new List<Measurement>(),
                    pressure = new List<Measurement>(),
                    moisture = new List<Measurement>(),
                    windSpeed = new List<Measurement>()
                };

                Measurement temperatureMeasurement = new Measurement { day = w.data_pomiaru, value = temperature };
                Measurement pressureMeasurement = new Measurement { day = w.data_pomiaru, value = pressure };
                Measurement moistureMeasurement = new Measurement { day = w.data_pomiaru, value = moisture };
                Measurement windSpeedMeasurement = new Measurement { day = w.data_pomiaru, value = windSpeed };

                stationMeasurement.temperature.Add(temperatureMeasurement);
                stationMeasurement.pressure.Add(pressureMeasurement);
                stationMeasurement.moisture.Add(moistureMeasurement);
                stationMeasurement.windSpeed.Add(windSpeedMeasurement);

                dbData.Add(stationMeasurement);
            }

            Save(dbData);

            Console.WriteLine("Pomyślnie zainicjowano bazę danych");
        }

        public static void Save(List<StationMeasurement> data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(Path, jsonString);

            Console.WriteLine("Pomyślnie zapisano dane");

            ChartService.SetMeasurements();
            ChartsViewModel.On.LoadData();
            ChartsViewModel.On.LoadStationData();
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
