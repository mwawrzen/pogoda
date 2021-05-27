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


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
