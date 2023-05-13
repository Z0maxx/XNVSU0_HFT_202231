using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Services.GenericServices.Interfaces
{
    interface IUpdateService<T> where T : TableModel
    {
        void Update(T item);
    }
}