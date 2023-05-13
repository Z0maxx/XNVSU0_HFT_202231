using System.Windows;
using WpfClient.ViewModels.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.EventtypeViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Windows.EventtypeWindows
{
    /// <summary>
    /// Interaction logic for EventtypeUpdateWindow.xaml
    /// </summary>
    public partial class EventtypeUpdateWindow : Window, IParameteredWindow<EventType>
    {
        public EventtypeUpdateWindow()
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

        public void Setup(EventType item)
        {
            Loaded += (_, _) => (DataContext as EventtypeUpdateViewModel)?.Setup(item);
        }
    }
}
