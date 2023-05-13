using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.FixedWageEmployeeViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.FixedWageEmployeeWindows
{
    /// <summary>
    /// Interaction logic for FixedWageEmployeeSingleWindow.xaml
    /// </summary>
    public partial class FWESingleWindow : Window, IParameteredWindow<FixedWageEmployee>
    {
        public FWESingleWindow()
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
        public void Setup(FixedWageEmployee item)
        {
            Loaded += (_, _) => (DataContext as FWESingleViewModel)?.Setup(item);
        }
    }
}
