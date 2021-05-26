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
        public override void Initialize()
        {
            GetWeatherData();
            GetDate();

            AvaloniaXamlLoader.Load(this);
        }

        public async void GetWeatherData()
        {
            await DataService.GetWeather();
            DataService.CurrentData = DataService.GetWeatherByName("Kraków");
            ChartsViewModel.On.LoadData();
        }

        public async void GetDate()
        {
            await DateService.GetDate();
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
