using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.EventtypeViewModels
{
    class EventtypeListViewModel : ListWithInnerListViewModel<EventType>
    {
        public EventtypeListViewModel() : base(PropOrders.Eventtype, "Orders")
        {
        }
    }
}
