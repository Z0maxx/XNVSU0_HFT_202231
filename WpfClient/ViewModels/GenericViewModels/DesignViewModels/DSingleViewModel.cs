using System;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.ViewModels.GenericViewModels.DesignViewModels
{
    class DSingleViewModel : SingleViewModel<TableModel>
    {
        public DSingleViewModel() : base(Array.Empty<string>())
        {
        }
    }
}
