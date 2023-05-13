using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class UpdateViewModel<T> : ViewModel<T>, ICloseWindow where T : TableModel
    {
        private readonly ILiveRestService<T>? service;
        private T? item;
        public T? Item {
            get => item;
            private set
            {
                SetProperty(ref item, value);
                (UpdateCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public ICommand UpdateCommand { get; private set; }
        public Action? Close { get; set; }

        public UpdateViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>())
        {
        }

        public UpdateViewModel(ILiveRestService<T>? service)
        {
            this.service = service;

            UpdateCommand = new RelayCommand(
                async () =>
                {
                    await Update();
                },
                () =>
                {
                    return CanUpdate();
                }
            );
        }

        protected virtual async Task Update()
        {
            if (Item != null && service != null)
            {
                var propInfos = Item.GetType().GetProperties().Where(p => p.GetAccessors().FirstOrDefault(a => a.IsVirtual) != null);
                foreach (PropertyInfo propInfo in propInfos)
                {
                    propInfo.SetValue(Item, null);
                }

                Result? res = await service.Single.Update(Item);
                ShowResult(res, "Update");
                if (res?.Success == true)
                {
                    Close?.Invoke();
                }
            }
        }
        protected virtual bool CanUpdate()
        {
            return Item != null && service != null;
        }
        public void Setup(T item)
        {
            int id = item.Id == null ? 0 : (int)item.Id;
            service?.Single.Setup(id);
            Item = service?.Single.Item;
            if (service != null)
            {
                service.Single.PropertyChanged += Single_PropertyChanged;
                Close += () =>
                {
                    service.Single.PropertyChanged -= Single_PropertyChanged;
                };
            }
        }

        private void Single_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is RestSingle<T> single)
            {
                if (single.Item == null)
                {
                    Task.Run(() => MessageBox.Show($"Selected {ModelName} got deleted"));
                    Close?.Invoke();
                }
                else
                {
                    Item = single.Item;
                }
            }
        }
    }
}
