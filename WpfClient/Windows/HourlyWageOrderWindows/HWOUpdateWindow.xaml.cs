using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.HourlyWageOrderViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.HourlyWageOrderWindows
{
    /// <summary>
    /// Interaction logic for HourlyWageOrderUpdateWindow.xaml
    /// </summary>
    public partial class HWOUpdateWindow : Window, IParameteredWindow<HourlyWageOrder>
    {
        public HWOUpdateWindow()
        {
            InitializeComponent();
            Loaded += (_, _) =>
            {
                if (DataContext is ICloseWindow vm)
                {
                    vm.Close += () => DialogResult = DialogResult == true;
                    Closing += (_, _) => vm.Close?.Invoke();
                }
            };
        }
        public void Setup(HourlyWageOrder item)
        {
            Loaded += (_, _) => (DataContext as HWOUpdateViewModel)?.Setup(item);
        }
    }
}
