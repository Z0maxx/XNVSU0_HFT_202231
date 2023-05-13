using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.JobViewModels
{
    class JobEmployeesViewModel : InnerListViewModel<Job, FixedWageEmployee, HourlyWageEmployee>
    {
        public JobEmployeesViewModel() : base(PropOrders.InnerListEmployee, PropOrders.InnerListEmployee)
        {
        }

        protected override void SetTitle(Job item)
        {
            Title = $"Employees Working As {item.Name}";
        }
    }
}
