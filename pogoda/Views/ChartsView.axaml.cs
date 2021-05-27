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

            Console.WriteLine("Liczba: " + DatabaseService.Get().Count);
        }

        public void CommunicationButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) sender;
            string? buttonName = btn.Name;

            string message = "";
            switch(buttonName)
            {
                case "Temperature":
                    message = $"Temperatura {DataService.CurrentData.temperatura}C";
                    break;
                case "Pressure":
                    message = $"Cisnienie {DataService.CurrentData.cisnienie}hPa";
                    break;
                case "Moisture":
                    message = $"Wilgotnosc {DataService.CurrentData.wilgotnosc_wzgledna}%";
                    break;
                case "WindSpeed":
                    message = $"Predkosc wiatru {DataService.CurrentData.predkosc_wiatru}B";
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
