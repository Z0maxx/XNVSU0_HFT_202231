using System.Windows;
using WpfClient.ViewModels.Interfaces;

namespace WpfClient.Windows.HourlyWageOrderWindows
{
    /// <summary>
    /// Interaction logic for HourlyWageOrderCreateWindow.xaml
    /// </summary>
    public partial class HWOCreateWindow : Window
    {
        public HWOCreateWindow()
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
