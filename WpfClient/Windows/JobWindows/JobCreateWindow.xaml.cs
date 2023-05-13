using System.Windows;
using WpfClient.ViewModels.Interfaces;

namespace WpfClient.Windows.JobWindows
{
    /// <summary>
    /// Interaction logic for JobCreateWindow.xaml
    /// </summary>
    public partial class JobCreateWindow : Window
    {
        public JobCreateWindow()
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
