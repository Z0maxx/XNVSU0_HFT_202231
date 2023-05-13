using System.Threading.Tasks;
using WpfClient.Services.RestServices;
using XNVSU0_HFT_202231.Models.TableModels;

namespace WpfClient.Logics.GenericLogics.Interfaces
{
    interface ISingleLogic<T> where T : TableModel
    {
        RestSingle<T> Single { get; }
        void Update();
        Task<Result?> Delete();
        void Setup(T item);
    }
}
