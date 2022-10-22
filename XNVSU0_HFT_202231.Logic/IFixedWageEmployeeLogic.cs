using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IFixedWageEmployeeLogic
    {
        void Create(FixedWageEmployee item);
        void Delete(int id);
        FixedWageEmployee Read(int id);
        IQueryable<FixedWageEmployee> ReadAll();
        void Update(FixedWageEmployee item);
    }
}