using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.FixedWageOrderViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.FixedWageOrderWindows
{
    /// <summary>
    /// Interaction logic for FWOSingleWindow.xaml
    /// </summary>
    public partial class FWOSingleWindow : Window, IParameteredWindow<FixedWageOrder>
    {
        public FWOSingleWindow()
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
        public void Setup(FixedWageOrder item)
        {
            Loaded += (_, _) => (DataContext as FWOSingleViewModel)?.Setup(item);
        }
    }
}
