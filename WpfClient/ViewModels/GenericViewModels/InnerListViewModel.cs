using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.RestServices;
using WpfClient.Services.RestServices.Interfaces;
using WpfClient.ViewModels.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class InnerListViewModel<T, S> : ViewModel<T>, ICloseWindow where T : TableModel where S : TableModel
    {
        private readonly IInnerListLogic<T, S>? logic;
        public List<string> ColumnNames1 { get; private set; }

        public ObservableCollection<S>? List1 { get => logic?.List1; }
        public Action? Close { get; set; }

        private string? title;
        public string? Title { get => title; set => SetProperty(ref title, value); }

        private S? selectedItem1;
        public S? SelectedItem1
        {
            get => selectedItem1;
            set
            {
                SetProperty(ref selectedItem1, value);
                (DetailsCommand1 as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public ICommand DetailsCommand1 { get; set; }
        public InnerListViewModel(string[] propOrder1) :
            this(IsInDesignMode ? null : Ioc.Default.GetService<IInnerListLogic<T, S>>(), IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>(), propOrder1)
        {
        }
        public InnerListViewModel(IInnerListLogic<T, S>? logic, string[] propOrder1) : this(logic, IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>(), propOrder1)
        {
        }

        public InnerListViewModel(IInnerListLogic<T, S>? logic, ILiveRestService<T>? service, string[] propOrder1)
        {
            this.logic = logic;

            if (service != null)
            {
                service.Single.PropertyChanged += Single_PropertyChanged;
                Close += () => service.Single.PropertyChanged -= Single_PropertyChanged;
            }

            DetailsCommand1 = new RelayCommand(
                () =>
                {
                    if (selectedItem1 != null)
                    {
                        logic?.Details(selectedItem1);
                    }
                },
                () =>
                {
                    return selectedItem1 != null;
                }
            );

            var props = typeof(S).GetProperties().ToList();
            ColumnNames1 = new();

            foreach (string prop in propOrder1)
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

                    ColumnNames1.Add($"{name} {innerName}");
                }
                else
                {
                    var propInfo = props.First(p => p.Name == prop);
                    string name = propInfo.Name;
                    var attribute = propInfo.GetCustomAttribute<DisplayNameAttribute>();
                    if (attribute != null) name = attribute.DisplayName;
                    ColumnNames1.Add(name);
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
                    Task.Run(() => MessageBox.Show($"Selected {ModelName} got deleted"));
                    Close?.Invoke();
                }
                else
                {
                    SetTitle(single.Item);
                }
            }
        }

        protected abstract void SetTitle(T item);
    }

    abstract class InnerListViewModel<T, S, U> : InnerListViewModel<T, U>, ICloseWindow where T : TableModel where S : TableModel where U : TableModel
    {
        private readonly IInnerListLogic<T, S, U>? logic;
        public ObservableCollection<S>? List2 { get => logic?.List2; }
        public List<string> ColumnNames2 { get; private set; }
        private S? selectedItem2;
        public S? SelectedItem2
        {
            get => selectedItem2;
            set
            {
                SetProperty(ref selectedItem2, value);
                (DetailsCommand2 as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public ICommand DetailsCommand2 { get; set; }
        public InnerListViewModel(string[] propOrder2, string[] propOrder1) :
            this(IsInDesignMode ? null : Ioc.Default.GetService<IInnerListLogic<T, S, U>>(), propOrder2, propOrder1)
        {
        }
        public InnerListViewModel(IInnerListLogic<T, S, U>? logic, string[] propOrder2, string[] propOrder1) : base(logic, propOrder1)
        {
            this.logic = logic;

            DetailsCommand2 = new RelayCommand(
                () =>
                {
                    if (selectedItem2 != null)
                    {
                        logic?.Details(selectedItem2);
                    }
                },
                () =>
                {
                    return selectedItem2 != null;
                }
            );

            var props = typeof(S).GetProperties().ToList();
            ColumnNames2 = new();

            foreach (string prop in propOrder2)
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

                    ColumnNames2.Add($"{name} {innerName}");
                }
                else
                {
                    var propInfo = props.First(p => p.Name == prop);
                    string name = propInfo.Name;
                    var attribute = propInfo.GetCustomAttribute<DisplayNameAttribute>();
                    if (attribute != null) name = attribute.DisplayName;
                    ColumnNames2.Add(name);
                }
            }
        }
        
    }
}
