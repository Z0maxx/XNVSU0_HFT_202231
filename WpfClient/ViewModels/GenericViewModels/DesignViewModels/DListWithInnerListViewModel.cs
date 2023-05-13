using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels.DesignViewModels
{
    sealed class DListWithInnerListViewModel : ListWithInnerListViewModel<TableModel>
    {
        public DListWithInnerListViewModel() : base(null, Array.Empty<string>(), "Table Model")
        {
        }
    }
}
