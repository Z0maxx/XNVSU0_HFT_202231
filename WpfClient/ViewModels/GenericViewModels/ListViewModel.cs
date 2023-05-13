using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.RestServices;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class ListViewModel<T> : ViewModel<T> where T : TableModel
    {
        protected readonly IListLogic<T>? logic;
        public List<string> ColumnNames { get; }

        public RestCollection<T>? List { get => logic?.List; }
        protected T? selectedItem;
        virtual public T? SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                (DeleteCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }
        public ListViewModel(string[] propOrder) : this(IsInDesignMode ? null : Ioc.Default.GetService<IListLogic<T>>(), propOrder)
        {
        }

        public ListViewModel(IListLogic<T>? logic, string[] propOrder)
        {
            this.logic = logic;

            CreateCommand = new RelayCommand(
                () =>
                {
                    logic?.Create();
                }
            );

            DeleteCommand = new RelayCommand(
                async () =>
                {
                    if (selectedItem != null && logic != null)
                    {
                        Result? res = await logic.Delete(selectedItem);
                        ShowResult(res, "Delete");
                    }
                },
                () =>
                {
                    return selectedItem != null;
                }
            );

            UpdateCommand = new RelayCommand(
                () =>
                {
                    if (selectedItem != null && logic != null)
                    {
                        logic.Update(selectedItem);
                    }
                },
                () =>
                {
                    return selectedItem != null;
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
    }
}
