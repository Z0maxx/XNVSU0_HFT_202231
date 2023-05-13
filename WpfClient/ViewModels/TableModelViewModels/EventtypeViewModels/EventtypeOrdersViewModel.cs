using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.EventtypeViewModels
{
    class EventtypeOrdersViewModel : InnerListViewModel<EventType, FixedWageOrder>
    {
        public EventtypeOrdersViewModel() : base(PropOrders.InnerListOrder)
        {
        }

        protected override void SetTitle(EventType item)
        {
            Title = $"Orders For {item.Name}";
        }
    }
}
