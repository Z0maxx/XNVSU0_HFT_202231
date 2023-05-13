using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices.Interfaces
{
    interface IListService<T> where T : TableModel
    {
        void List();
    }
}
