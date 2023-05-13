using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics.Interfaces
{
    interface IListWithSingleLogic<T, S> : IListLogic<T> where T : TableModel where S : TableModel
    {
        void Single(T item);
    }
}
