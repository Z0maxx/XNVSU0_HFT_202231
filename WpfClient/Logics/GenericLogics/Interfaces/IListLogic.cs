using System.Threading.Tasks;
using WpfClient.Services.RestServices;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics.Interfaces
{
    interface IListLogic<T> where T : TableModel
    {
        RestCollection<T>? List { get; }
        void Create();
        void Update (T item);
        Task<Result?> Delete(T item);
    }
}