using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class SingleViewModel<T> : ViewModel<T>, ICloseWindow where T : TableModel
    {
        private readonly ISingleLogic<T>? logic;
        public List<string> ColumnNames { get; }
        private T? item;
        public virtual T? Item
        {
            get => item;
            protected set
            {
                SetProperty(ref item, value);
                (DeleteCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }
        public Action? Close { get; set; }
        public SingleViewModel(string[] propOrder) :
            this(IsInDesignMode ? null : Ioc.Default.GetService<ISingleLogic<T>>(), IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>(), propOrder)
        {
        }
        public SingleViewModel(ISingleLogic<T>? logic, string[] propOrder) : this(logic, IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>(), propOrder)
        {
        }
        public SingleViewModel(ISingleLogic<T>? logic, ILiveRestService<T>? service, string[] propOrder)
        {
            this.logic = logic;

            if (service != null)
            {
                service.Single.PropertyChanged += Single_PropertyChanged;
                Close += () => service.Single.PropertyChanged -= Single_PropertyChanged;
            }

            DeleteCommand = new RelayCommand(
                async () =>
                {
                    if (logic != null)
                    {
                        Result? res = await logic.Delete();
                        ShowResult(res, "Delete");
                    }
                },
                () =>
                {
                    return Item != null;
                }
            );

            UpdateCommand = new RelayCommand(
                () =>
                {
                    if (logic != null)
                    {
                        logic?.Update();
                    }
                },
                () =>
                {
                    return Item != null;
                }
            );

            var props = typeof(T).GetProperties().ToList();

            ColumnNames = new();

            foreach (string prop in propOrder)
            {
                if (prop.Contains('.'))
                {
                    var propInfo = props.First(p => p.Name == prop.Split('.')[0]);
                    string name = propInfo.Name;
                    var attribute = propInfo.GetCustomAttribute<DisplayNameAttribute>();
                    if (attribute != null) name = attribute.DisplayName;

                    var innerProps = propInfo.PropertyType.GetProperties();
                    var innerPropInfo = innerProps.First(p => p.Name == prop.Split('.')[1]);
                    string innerName = innerPropInfo.Name;
                    var innerAttribute = innerPropInfo.GetCustomAttribute<DisplayNameAttribute>();
                    if (innerAttribute != null) innerName = innerAttribute.DisplayName;

                    ColumnNames.Add($"{name} {innerName}");
                }
                else
                {
                    var propInfo = props.First(p => p.Name == prop);
                    string name = propInfo.Name;
                    var attribute = propInfo.GetCustomAttribute<DisplayNameAttribute>();
                    if (attribute != null) name = attribute.DisplayName;
                    ColumnNames.Add(name);
                }
            }
        }

        public void Setup(T item)
        {
            logic?.Setup(item);
        }

        private void Single_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is RestSingle<T> single)
            {
                if (single.Item == null)
                {
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
