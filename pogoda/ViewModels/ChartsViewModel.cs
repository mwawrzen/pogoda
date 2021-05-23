using System.Collections.Generic;
using pogoda.Models;
using pogoda.Services;
using ReactiveUI;

namespace pogoda.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        IEnumerable<Weather> items;

        public ChartsViewModel()
        {
            On = this;
        }

        public void LoadData()
        {
            Items = DataService.DataList;
        }

        public IEnumerable<Weather> Items
        {
            get => items;
            private set => this.RaiseAndSetIfChanged(ref items, value);
        }
        public static ChartsViewModel On;
    }
}
