using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.JobViewModels
{
    class JobListViewModel : ListWithInnerListViewModel<Job>
    {
        public JobListViewModel() : base(PropOrders.Job, "Employees")
        {
        }
    }
}
