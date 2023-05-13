using System.Windows;
using WpfClient.ViewModels.Interfaces;

namespace WpfClient.Windows.EventtypeWindows
{
    /// <summary>
    /// Interaction logic for EventtypeCreateWindow.xaml
    /// </summary>
    public partial class EventtypeCreateWindow : Window
    {
        public EventtypeCreateWindow()
        {
            InitializeComponent();
            Loaded += (_, _) =>
            {
                if (DataContext is ICloseWindow vm)
                {
                    vm.Close += () => DialogResult = true;
                    Closing += (_, _) => vm.Close?.Invoke();
                }
            };
        }
    }
}
