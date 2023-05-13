using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.ViewModels.GenericViewModels;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.TableModelViewModels.EmployeeViewModels
{
    abstract class EmployeeListViewModel<T> : ListWithInnerListViewModel<T> where T : Employee
    {
        public EmployeeListViewModel(string[] propOrder) : base(propOrder, "Orders")
        {
        }
    }
}
