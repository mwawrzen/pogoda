using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using pogoda.ViewModels;
using pogoda.Views;
using pogoda.Services;

using Avalonia.Controls;
using System.Collections.Generic;

namespace pogoda
{
    public class App : Application
    {
        public override void Initialize()
        {
            GetWeatherData();

            AvaloniaXamlLoader.Load(this);
        }

        public async void GetWeatherData()
        {
            await DataService.GetWeather();
            ChartsViewModel.On.LoadData();
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
