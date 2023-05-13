using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.FixedWageEmployeeViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.FixedWageEmployeeWindows
{
    /// <summary>
    /// Interaction logic for FixedWageEmployeeUpdateWindow.xaml
    /// </summary>
    public partial class FWEUpdateWindow : Window, IParameteredWindow<FixedWageEmployee>
    {
        public FWEUpdateWindow()
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
        public void Setup(FixedWageEmployee item)
        {
            Loaded += (_, _) => (DataContext as FWEUpdateViewModel)?.Setup(item);
        }
    }
}
