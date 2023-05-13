using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.HourlyWageOrderViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.HourlyWageOrderWindows
{
    /// <summary>
    /// Interaction logic for HWOSingleWindow.xaml
    /// </summary>
    public partial class HWOSingleWindow : Window, IParameteredWindow<HourlyWageOrder>
    {
        public HWOSingleWindow()
        {
            InitializeComponent();
            Loaded += (_, _) =>
            {
                if (DataContext is ICloseWindow vm)
                {
                    vm.Close += () => DialogResult = false;
                    Closing += (_, _) => vm.Close?.Invoke();
                }
            };
        }
        public void Setup(HourlyWageOrder item)
        {
            Loaded += (_, _) => (DataContext as HWOSingleViewModel)?.Setup(item);
        }
    }
}
