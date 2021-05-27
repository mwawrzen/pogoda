using System;
using System.Collections.Generic;
using pogoda.Models;
using pogoda.Services;
using ReactiveUI;
using OxyPlot;
using System.Timers;

namespace pogoda.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        IEnumerable<Weather> items;

        string? currentStationName;
        string? currentTemperature;
        string? currentPressure;
        string? currentWindSpeed;
        string? currentMoisture;

        PlotModel temperaturePlotModel;
        PlotModel pressurePlotModel;
        PlotModel moisturePlotModel;
        PlotModel windSpeedPlotModel;

        string currentDate;
        static DateTime currDate;

        private static Timer aTimer;


        public ChartsViewModel()
        {
            On = this;
        }

        public void SetTimer()
        {
            aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            On.UpdateCurrentDate(currDate);
            currDate = currDate.AddSeconds(1);
        }

        public void LoadData()
        {
            currDate = DateService.CurrentDate;
            Items = DataService.DataList;
            CurrentDate = DateService.CurrentDate.ToString("dd MMMM yyyy | HH:mm");
            TemperaturePlotModel = ChartService.RenderTemperatureChart();
            PressurePlotModel = ChartService.RenderPressureChart();
            MoisturePlotModel = ChartService.RenderMoistureChart();
            WindSpeedPlotModel = ChartService.RenderWindSpeedChart();
        }

        void UpdateCurrentDate(DateTime date)
        {
            CurrentDate = date.ToString("dd MMMM yyyy | HH:mm");
        }
        public void LoadStationData()
        {
            CurrentStationName = DataService.CurrentData != null ? DataService.CurrentData.stacja : "";
            CurrentTemperature = DataService.CurrentData != null ? DataService.CurrentData.temperatura : "";
            CurrentPressure = DataService.CurrentData != null ? DataService.CurrentData.cisnienie : "";
            CurrentWindSpeed = DataService.CurrentData != null ? DataService.CurrentData.predkosc_wiatru : "";
            CurrentMoisture = DataService.CurrentData != null ? DataService.CurrentData.wilgotnosc_wzgledna : "";
        }

        public void DisplayData()
        {
            if (DataService.CurrentData != null)
                DataService.DisplayData(DataService.CurrentData);
        }

        public IEnumerable<Weather> Items
        {
            get => items;
            private set => this.RaiseAndSetIfChanged(ref items, value);
        }


        public string? CurrentStationName
        {
            get => currentStationName;
            private set => this.RaiseAndSetIfChanged(ref currentStationName, value);
        }

        public string? CurrentTemperature
        {
            get => currentTemperature;
            private set => this.RaiseAndSetIfChanged(ref currentTemperature, value + "\u2103");
        }

        public string? CurrentPressure
        {
            get => currentPressure;
            private set => this.RaiseAndSetIfChanged(ref currentPressure, value != null ? value + "hPa" : "---");
        }

        public string? CurrentWindSpeed
        {
            get => currentWindSpeed;
            private set => this.RaiseAndSetIfChanged(ref currentWindSpeed, value + "B");
        }

        public string? CurrentMoisture
        {
            get => currentMoisture;
            private set => this.RaiseAndSetIfChanged(ref currentMoisture, value + "%");
        }
        
        public PlotModel TemperaturePlotModel
        {
            get => temperaturePlotModel;
            private set => this.RaiseAndSetIfChanged(ref temperaturePlotModel, value);
        }

        public PlotModel PressurePlotModel
        {
            get => pressurePlotModel;
            private set => this.RaiseAndSetIfChanged(ref pressurePlotModel, value);
        }

        public PlotModel MoisturePlotModel
        {
            get => moisturePlotModel;
            private set => this.RaiseAndSetIfChanged(ref moisturePlotModel, value);
        }

        public PlotModel WindSpeedPlotModel
        {
            get => windSpeedPlotModel;
            private set => this.RaiseAndSetIfChanged(ref windSpeedPlotModel, value);
        }

        public string CurrentDate
        {
            get => currentDate;
            private set => this.RaiseAndSetIfChanged(ref currentDate, value);

        }

        public static ChartsViewModel On;
    }
}
