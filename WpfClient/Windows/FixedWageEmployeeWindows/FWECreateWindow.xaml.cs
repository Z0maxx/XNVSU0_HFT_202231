using System.Windows;
using WpfClient.ViewModels.Interfaces;

namespace WpfClient.Windows.FixedWageEmployeeWindows
{
    /// <summary>
    /// Interaction logic for FixedWageEmployeeCreateWindow.xaml
    /// </summary>
    public partial class FWECreateWindow : Window
    {
        public FWECreateWindow()
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
