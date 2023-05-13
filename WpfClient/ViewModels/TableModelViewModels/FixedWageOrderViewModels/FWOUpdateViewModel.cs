using CommunityToolkit.Mvvm.DependencyInjection;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.TableModelViewModels.OrderViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.FixedWageOrderViewModels
{
    class FWOUpdateViewModel : OrderUpdateViewModel<FixedWageOrder, FixedWageEmployee>
    {
        public RestCollection<EventType>? Eventtypes { get; }
        public FWOUpdateViewModel() : this (IsInDesignMode? null : Ioc.Default.GetService<ILiveRestService<EventType>>())
        {
        }
        public FWOUpdateViewModel(ILiveRestService<EventType>? eventtypeService)
        {
            Eventtypes = eventtypeService?.List;
        }
    }
}
