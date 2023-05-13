using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class CreateViewModel<T> : ViewModel<T>, ICloseWindow where T : TableModel, new()
    {
        protected ILiveRestService<T>? service;
        public T Item { get; set; }
        public ICommand CreateCommand { get; protected set; }
        public Action? Close { get; set; }
        public CreateViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>())
        {
        }
        public CreateViewModel(ILiveRestService<T>? service)
        {
            this.service = service;
            Item = new();

            CreateCommand = new RelayCommand(
                async () =>
                {
                    await Create();
                },
                () =>
                {
                    return CanCreate();
                }
            );
        }

        protected async virtual Task Create()
        {
            if (service != null)
            {
                Result res = await service.List.Add(Item);
                ShowResult(res, "Create");
                if (res?.Success == true)
                {
                    Close?.Invoke();
                }
            }
        }

        protected virtual bool CanCreate()
        {
            return service != null;
        }
    }
}
