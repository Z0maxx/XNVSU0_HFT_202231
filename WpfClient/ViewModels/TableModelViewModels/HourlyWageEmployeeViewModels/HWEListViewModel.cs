using WpfClient.ViewModels.TableModelViewModels.EmployeeViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.HourlyWageEmployeeViewModels
{
    class HWEListViewModel : EmployeeListViewModel<HourlyWageEmployee>
    {
        public HWEListViewModel() : base(PropOrders.HourlyWageEmployee)
        {
        }
    }
}
