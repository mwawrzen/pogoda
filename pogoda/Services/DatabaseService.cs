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

        public static void SaveDataToDatabase()
        {
            string? data = File.ReadAllText(Path);

            if (data == "")
            {
                Console.WriteLine("INIT");
                Initialize();
            }
                
            else
            {
                Console.WriteLine("SAVE");
                List<StationMeasurement> measurementList = Get();
                var curr = DataService.DataList;

                var dt = Convert.ToDateTime(DateService.CurrentDate);
                dt = dt.AddDays(1);
                string dtString = dt.ToString("yyyy-MM-dd");

                for (int i = 0; i < measurementList.Count; i++)
                {
                    Console.WriteLine("LOOP");
                    foreach (var item in measurementList[i].temperature)
                    {
                        if (dtString == item.day) //curr[0].data_pomiaru
                            goto end;
                            
                    }

                    Console.WriteLine("SAVING DATA");

                    double temperature = Convert.ToDouble(curr[i].temperatura.Replace(".", ","));
                    double wilgotnosc = Convert.ToDouble(curr[i].wilgotnosc_wzgledna.Replace(".", ","));
                    double predkosc = Convert.ToDouble(curr[i].predkosc_wiatru.Replace(".", ","));
                    double cisnienie = 0;
                    
                    if (curr[i].cisnienie != null)
                        cisnienie = Convert.ToDouble(curr[i].cisnienie.Replace(".", ","));

                    Measurement temperatureMeasurement = new Measurement { day = dtString, value = 34.5 };//temperature };
                    Measurement pressureMeasurement = new Measurement { day = dtString, value = 1001.4 }; //cisnienie };
                    Measurement moistureMeasurement = new Measurement { day = dtString, value = 48 };//wilgotnosc };
                    Measurement windSpeedMeasurement = new Measurement { day = dtString, value = 2 };//predkosc };

                    Console.WriteLine("Save data: " + measurementList[i].station);

                    measurementList[i].temperature.Add(temperatureMeasurement);
                    measurementList[i].pressure.Add(pressureMeasurement);
                    measurementList[i].moisture.Add(moistureMeasurement);
                    measurementList[i].windSpeed.Add(windSpeedMeasurement);

                    end:
                        continue;
                }
                

                Save(measurementList);
            }
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
