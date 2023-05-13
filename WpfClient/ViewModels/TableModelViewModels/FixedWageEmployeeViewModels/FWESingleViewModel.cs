using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.FixedWageEmployeeViewModels
{
    class FWESingleViewModel : SingleWithInnerListViewModel<FixedWageEmployee>
    {
        public FWESingleViewModel() : base(PropOrders.FixedWageEmployee, "Orders")
        {
        }
    }
}
