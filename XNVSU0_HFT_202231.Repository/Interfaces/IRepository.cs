using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Repository
{
    public interface IRepository<T> where T : TableModel
    {
        IQueryable<T> ReadAll();
        T Read(int? id);
        void Create(T item);
        void Delete(int id);
        void Update(T item);
    }
}
