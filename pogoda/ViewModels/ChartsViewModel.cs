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
            CurrentDate = DateService.CurrentDate.ToString("dd-MM-yyyy | HH:mm");
            TemperaturePlotModel = ChartService.RenderTemperatureChart();
            PressurePlotModel = ChartService.RenderPressureChart();
            MoisturePlotModel = ChartService.RenderMoistureChart();
            WindSpeedPlotModel = ChartService.RenderWindSpeedChart();
        }

        void UpdateCurrentDate(DateTime date)
        {
            CurrentDate = date.ToString("dd-MM-yyyy | HH:mm");
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
