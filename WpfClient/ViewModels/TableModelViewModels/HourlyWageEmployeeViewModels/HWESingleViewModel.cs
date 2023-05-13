using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.HourlyWageEmployeeViewModels
{
    class HWESingleViewModel : SingleWithInnerListViewModel<HourlyWageEmployee>
    {
        public HWESingleViewModel() : base(PropOrders.HourlyWageEmployee, "Orders")
        {
        }
    }
}
