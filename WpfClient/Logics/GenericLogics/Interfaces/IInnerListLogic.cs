using System.Collections.ObjectModel;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics.Interfaces
{
    interface IInnerListLogic<T, S> where T : TableModel where S : TableModel
    {
        ObservableCollection<S>? List1 { get; }
        void Details(S item);
        void Setup(T item);
    }

    interface IInnerListLogic<T, S, U> : IInnerListLogic<T, U> where T : TableModel where S : TableModel where U : TableModel
    {
        ObservableCollection<S>? List2 { get; }
        void Details(S item);
    }
}