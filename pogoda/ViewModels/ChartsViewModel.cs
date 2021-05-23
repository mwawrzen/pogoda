using System.Collections.Generic;
using pogoda.Models;
using pogoda.Services;

namespace pogoda.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        public void LoadData()
        {
            Items = DataService.DataList;
        }

        public List<Item> Items { get; set;  }
    }
}
