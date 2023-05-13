using System.Windows;
using WpfClient.ViewModels.Interfaces;

namespace WpfClient.Windows.HourlyWageEmployeeWindows
{
    /// <summary>
    /// Interaction logic for HourlyWageEmployeeCreateWindow.xaml
    /// </summary>
    public partial class HWECreateWindow : Window
    {
        public HWECreateWindow()
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
