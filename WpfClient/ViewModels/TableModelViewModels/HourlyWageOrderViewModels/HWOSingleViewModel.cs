using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.HourlyWageOrderViewModels
{
    class HWOSingleViewModel : SingleWithSingleViewModel<HourlyWageOrder, HourlyWageEmployee>
    {
        public HWOSingleViewModel() : base(PropOrders.HourlyWageOrder, "Employee")
        {
        }
    }
}
