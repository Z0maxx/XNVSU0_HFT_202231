using CommunityToolkit.Mvvm.DependencyInjection;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.OrderViewModels
{
    abstract class OrderCreateViewModel<T, S> : CreateViewModel<T> where T : Order, new() where S : Employee
    {
        public RestCollection<S>? Employees { get; }
        public OrderCreateViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<S>>())
        {
        }
        public OrderCreateViewModel(ILiveRestService<S>? employeeService)
        {
            Employees = employeeService?.List;
        }
    }
}
