using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.HourlyWageEmployeeViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.HourlyWageEmployeeWindows
{
    /// <summary>
    /// Interaction logic for HourlyWageEmployeeUpdateWindow.xaml
    /// </summary>
    public partial class HWEUpdateWindow : Window, IParameteredWindow<HourlyWageEmployee>
    {
        public HWEUpdateWindow()
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
        public void Setup(HourlyWageEmployee item)
        {
            Loaded += (_, _) => (DataContext as HWEUpdateViewModel)?.Setup(item);
        }
    }
}
