using CommunityToolkit.Mvvm.DependencyInjection;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.OrderViewModels
{
    abstract class OrderUpdateViewModel<T, S> : UpdateViewModel<T> where T : Order where S : Employee
    {
        public RestCollection<S>? Employees { get; }
        public OrderUpdateViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<S>>())
        {
        }
        public OrderUpdateViewModel(ILiveRestService<S>? employeeService)
        {
            Employees = employeeService?.List;
        }
    }
}
