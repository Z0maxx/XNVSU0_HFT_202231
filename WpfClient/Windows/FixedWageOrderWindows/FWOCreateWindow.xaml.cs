using System.Windows;
using WpfClient.ViewModels.Interfaces;

namespace WpfClient.Windows.FixedWageOrderWindows
{
    /// <summary>
    /// Interaction logic for FixedWageOrderCreateWindow.xaml
    /// </summary>
    public partial class FWOCreateWindow : Window
    {
        public FWOCreateWindow()
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
