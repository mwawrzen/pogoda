using System;
using System.Collections.Generic;
using System.Text;

namespace pogoda.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase Content => new ChartsViewModel();
    }
}
