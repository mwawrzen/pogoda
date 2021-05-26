using System;
using System.Collections.Generic;
using pogoda.Models;
using pogoda.Services;
using ReactiveUI;
using OxyPlot;

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


        public ChartsViewModel()
        {
            On = this;
        }

        public void LoadData()
        {
            Items = DataService.DataList;
            CurrentDate = DateService.CurrentDate.ToString("dd-MM-yyyy | HH:mm");
            TemperaturePlotModel = ChartService.RenderTemperatureChart();
            PressurePlotModel = ChartService.RenderPressureChart();
            MoisturePlotModel = ChartService.RenderMoistureChart();
            WindSpeedPlotModel = ChartService.RenderWindSpeedChart();
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
            private set => this.RaiseAndSetIfChanged(ref currentTemperature, value + "°C");
        }

        public string? CurrentPressure
        {
            get => currentPressure;
            private set => this.RaiseAndSetIfChanged(ref currentPressure, value + "hPa");
        }

        public string? CurrentWindSpeed
        {
            get => currentWindSpeed;
            private set => this.RaiseAndSetIfChanged(ref currentWindSpeed, value + "km/h");
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
