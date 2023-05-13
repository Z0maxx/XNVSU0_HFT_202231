using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.EmployeeViewModels
{
    abstract class EmployeeOrdersViewModel<T, S> : InnerListViewModel<T, S> where T : Employee where S : Order
    {
        public EmployeeOrdersViewModel() : base(PropOrders.InnerListOrder)
        {
        }
        protected override void SetTitle(T item)
        {
            Title = $"Orders For {item.FirstName} {item.LastName}";
        }
    }
}
