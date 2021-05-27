using System;
using System.Timers;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using pogoda.ViewModels;
using pogoda.Views;
using pogoda.Services;

namespace pogoda
{
    public class App : Application
    {
        private static Timer aTimer;
        static DateTime currDate;

        public void SetTimer()
        {
            aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            currDate = currDate.AddSeconds(1);
        }

        public override void Initialize()
        {
            GetDate();
            
            AvaloniaXamlLoader.Load(this);
        }

        public async void GetWeatherData()
        {
            await DataService.GetWeather();
            DataService.CurrentData = DataService.GetWeatherByName("Kraków");
            currDate = DateService.CurrentDate;
            SetTimer();

            if (DatabaseService.CheckIsEmpty())
                DatabaseService.Initialize();

            ChartService.SetMeasurements();
            ChartsViewModel.On.LoadData();
            ChartsViewModel.On.LoadStationData();
        }

        public async void GetDate()
        {
            await DateService.GetDate();
            GetWeatherData();
            ChartsViewModel.On.SetTimer();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
