using System;
using pogoda.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using pogoda.Services;

namespace pogoda.Views
{
    public partial class ChartsView : UserControl
    {
        public ChartsView()
        {
            InitializeComponent();
        }

        public void StationButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) sender;
            string stationName = (string) btn.Content;

            DataService.GetWeatherByName(stationName);
            ChartService.SetMeasurements();
            ChartsViewModel.On.LoadData();
            ChartsViewModel.On.LoadStationData();

            DropDown dp = this.FindControl<DropDown>("StationList");
            dp.IsDropDownOpen = false;
        }

        public void CommunicationButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) sender;
            string? buttonName = btn.Name;

            string message = "";
            switch(buttonName)
            {
                case "Temperature":
                    message = $"temperatura-{DataService.CurrentData.temperatura} C";
                    break;
                case "Pressure":
                    message = $"cisnienie-{DataService.CurrentData.cisnienie}hPa";
                    break;
                case "Moisture":
                    message = $"wilgotnosc-{DataService.CurrentData.wilgotnosc_wzgledna}\u0025";
                    break;
                case "WindSpeed":
                    message = $"predkosc wiatru-{DataService.CurrentData.predkosc_wiatru}B";
                    break;
            }

            Console.WriteLine(message);
            CommunicationService.Connect(message);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
