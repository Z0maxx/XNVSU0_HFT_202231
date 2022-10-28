using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IFixedWageEmployeeLogic
    {
        void Create(FixedWageEmployee item);
        void Delete(int id);
        FixedWageEmployee Read(int id);
        IQueryable<FixedWageEmployee> ReadAll();
        void Update(FixedWageEmployee item);
        public IEnumerable<EmployeeOrdersCount> MostPopular();
    }
}