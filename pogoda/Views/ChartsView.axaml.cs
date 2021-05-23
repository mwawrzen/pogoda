using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace pogoda.Views
{
    public partial class ChartsView : UserControl
    {
        public ChartsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
