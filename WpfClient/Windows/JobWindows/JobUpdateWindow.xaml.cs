using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.JobViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.JobWindows
{
    /// <summary>
    /// Interaction logic for JobUpdateWindow.xaml
    /// </summary>
    public partial class JobUpdateWindow : Window, IParameteredWindow<Job>
    {
        public JobUpdateWindow()
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

        public void Setup(Job item)
        {
            Loaded += (_, _) => (DataContext as JobUpdateViewModel)?.Setup(item);
        }
    }
}
