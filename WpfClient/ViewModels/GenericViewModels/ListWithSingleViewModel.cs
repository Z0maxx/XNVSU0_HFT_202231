using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class ListWithSingleViewModel<T, S> : ListViewModel<T> where T : TableModel where S : TableModel
    {
        public ICommand SingleCommand { get; }
        public string SingleName { get; }
        public override T? SelectedItem
        {
            get => selectedItem;
            set
            {
                base.SelectedItem = value;
                (SingleCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public ListWithSingleViewModel(string[] propOrder, string singleName) :
            this(IsInDesignMode ? null : Ioc.Default.GetService<IListWithSingleLogic<T, S>>(), propOrder, singleName)
        {
        }
        public ListWithSingleViewModel(IListWithSingleLogic<T, S>? logic, string[] propOrder, string singleName) :
            base(logic, propOrder)
        {
            SingleName = singleName;

            SingleCommand = new RelayCommand(
                () =>
                {
                    if (selectedItem != null)
                    {
                        logic?.Single(selectedItem);
                    }
                },
                () =>
                {
                    if (selectedItem == null) return false;
                    var propInfos = typeof(T).GetProperties();
                    var value = propInfos.First(p => p.PropertyType.Name == typeof(S).Name).GetValue(selectedItem);
                    return value != null;
                }
            );
        }
    }
}
