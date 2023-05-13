using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.OrderViewModels
{
    abstract class OrderListViewModel<T, S> : ListWithSingleViewModel<T, S> where T : Order where S : TableModel
    {
        public OrderListViewModel(string[] propOrder) : base(propOrder, "Employee")
        {
        }
    }
}
