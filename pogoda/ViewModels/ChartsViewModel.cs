using System.Collections.Generic;
using pogoda.Models;
using pogoda.Services;

namespace pogoda.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        public ChartsViewModel()
        {
            On = this;
        }

        public void LoadData()
        {
            foreach(var obj in DataService.DataList)
            {
                Items.Add(obj);
            }
        }

        public List<Weather> Items { get; set;  }
        public static ViewModelBase On { get; set; }
    }
}
