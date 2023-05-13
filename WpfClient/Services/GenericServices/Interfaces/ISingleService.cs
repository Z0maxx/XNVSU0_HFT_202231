using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices.Interfaces
{
    interface ISingleService<T> where T : TableModel
    {
        void Single(T item);
    }
}
