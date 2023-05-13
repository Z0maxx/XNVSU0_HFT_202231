using CommunityToolkit.Mvvm.DependencyInjection;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.EmployeeViewModels
{
    abstract class EmployeeUpdateViewModel<T> : UpdateViewModel<T> where T : Employee
    {
        public RestCollection<Job>? JobList { get; }
        public EmployeeUpdateViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<Job>>())
        {
        }
        public EmployeeUpdateViewModel(ILiveRestService<Job>? jobService)
        {
            JobList = jobService?.List;
        }
    }
}
