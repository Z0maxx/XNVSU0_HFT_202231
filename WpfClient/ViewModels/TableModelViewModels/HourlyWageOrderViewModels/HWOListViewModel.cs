using WpfClient.ViewModels.TableModelViewModels.OrderViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.HourlyWageOrderViewModels
{
    class HWOListViewModel : OrderListViewModel<HourlyWageOrder, HourlyWageEmployee>
    {
        public HWOListViewModel() : base(PropOrders.HourlyWageOrder)
        {
        }
    }
}
