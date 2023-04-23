using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Logic
{
    public interface ILogic<T> where T : TableModel
    {
        void Create(T item);
        void CreateBulk(IEnumerable<T> items);
        void Delete(int id);
        T Read(int id);
        IQueryable<T> ReadAll();
        IEnumerable<T> ReadBulk(IEnumerable<int> ids);
        void Update(T item);
        void UpdateBulk(IEnumerable<T> items);
    }
}
