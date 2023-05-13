using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels.DesignViewModels
{
    class DSingleWithSingleViewModel : SingleWithSingleViewModel<TableModel, TableModel>
    {
        public DSingleWithSingleViewModel() : base(Array.Empty<string>(), "TableModel")
        {
        }
    }
}
