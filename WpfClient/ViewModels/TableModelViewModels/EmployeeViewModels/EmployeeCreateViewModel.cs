using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.EmployeeViewModels
{
    abstract class EmployeeCreateViewModel<T> : CreateViewModel<T> where T : Employee, new()
    {
        public RestCollection<Job>? JobList { get; }
        public EmployeeCreateViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<Job>>())
        {
        }
        public EmployeeCreateViewModel(ILiveRestService<Job>? jobService)
        {
            JobList = jobService?.List;
        }
        protected override async Task Create()
        {
            Item.HireDate = DateTime.Now;
            await base.Create();
        }
    }
}
