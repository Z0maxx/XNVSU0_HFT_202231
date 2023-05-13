using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WpfClient.Logics.GenericLogics.Interfaces;
using WpfClient.Services.RestServices.Interfaces;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels
{
    abstract class SingleWithInnerListViewModel<T> : SingleViewModel<T> where T : TableModel
    {
        public ICommand InnerListCommand { get; }
        public string InnerListName { get; }
        public SingleWithInnerListViewModel(string[] propOrder, string innerListName) :
           this(IsInDesignMode ? null : Ioc.Default.GetService<ISingleWithInnerListLogic<T>>(), IsInDesignMode ? null : Ioc.Default.GetService<ILiveRestService<T>>(), propOrder, innerListName)
        {
        }
        public SingleWithInnerListViewModel(ISingleWithInnerListLogic<T>? logic, ILiveRestService<T>? service, string[] propOrder, string innerListName) :
            base(logic, service, propOrder)
        {
            InnerListName = innerListName;

            InnerListCommand = new RelayCommand(
            () =>
            {
                logic?.InnerList();
            });
        }
    }
}
