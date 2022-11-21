using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Logic
{
    public interface ILogic<T> where T : TableModel
    {
        void Create(T item);
        void Delete(int id);
        T Read(int id);
        IQueryable<T> ReadAll();
        void Update(T item);
    }
}
