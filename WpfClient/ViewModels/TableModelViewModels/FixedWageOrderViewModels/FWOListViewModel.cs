using WpfClient.ViewModels.TableModelViewModels.OrderViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.FixedWageOrderViewModels
{
    class FWOListViewModel : OrderListViewModel<FixedWageOrder, FixedWageEmployee>
    {
        public FWOListViewModel() : base(PropOrders.FixedWageOrder)
        {
        }
    }
}
