using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class ListWithInnerListViewModel<T> : ListViewModel<T> where T : TableModel
    {
        public ICommand InnerListCommand { get; }
        public string InnerListName { get; }
        public override T? SelectedItem
        {
            get => selectedItem;
            set
            {
                base.SelectedItem = value;
                (InnerListCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }
        public ListWithInnerListViewModel(string[] propOrder, string innerListName) :
            this(IsInDesignMode ? null : Ioc.Default.GetService<IListWithInnerListLogic<T>>(), propOrder, innerListName)
        {
        }
        public ListWithInnerListViewModel(IListWithInnerListLogic<T>? logic, string[] propOrder, string innerListName) :
            base(logic, propOrder)
        {
            InnerListName = innerListName;

            InnerListCommand = new RelayCommand(
                () =>
                {
                    if (selectedItem != null)
                    {
                        logic?.InnerList(selectedItem);
                    }
                },
                () =>
                {
                    return selectedItem != null;
                }
            );
        }
    }
}
