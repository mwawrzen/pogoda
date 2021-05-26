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
        PlotModel temperaturePlotModel;
        PlotModel pressurePlotModel;
        PlotModel moisturePlotModel;
        PlotModel windSpeedPlotModel;

        public ChartsViewModel()
        {
            On = this;
        }

        public void LoadData()
        {
            Items = DataService.DataList;
            TemperaturePlotModel = ChartService.RenderTemperatureChart();
            PressurePlotModel = ChartService.RenderPressureChart();
            MoisturePlotModel = ChartService.RenderMoistureChart();
            WindSpeedPlotModel = ChartService.RenderWindSpeedChart();
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

        public static ChartsViewModel On;
    }
}
