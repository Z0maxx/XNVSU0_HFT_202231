using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.EventtypeViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.EventtypeWindows
{
    /// <summary>
    /// Interaction logic for EventtypeOrdersWindow.xaml
    /// </summary>
    public partial class EventtypeOrdersWindow : Window, IParameteredWindow<EventType>
    {
        public EventtypeOrdersWindow()
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

        public void Setup(EventType item)
        {
            Loaded += (_, _) => (DataContext as EventtypeOrdersViewModel)?.Setup(item);
        }
    }
}
