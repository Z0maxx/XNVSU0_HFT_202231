using System;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels.DesignViewModels
{
    sealed class DSingleWithInnerListViewModel : SingleWithInnerListViewModel<TableModel>
    {
        public DSingleWithInnerListViewModel() : base(Array.Empty<string>(), "TableModel")
        {
        }
    }
}
