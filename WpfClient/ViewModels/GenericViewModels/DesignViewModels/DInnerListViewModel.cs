using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels.DesignViewModels
{
    sealed class DInnerListViewModel : InnerListViewModel<TableModel, TableModel>
    {
        public DInnerListViewModel() : base(Array.Empty<string>())
        {
        }
        protected override void SetTitle(TableModel item)
        {
            throw new NotImplementedException();
        }
    }
}
