using System.Collections.Generic;
using pogoda.Models;

namespace pogoda.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase Content => new ChartsViewModel();
    }
}
