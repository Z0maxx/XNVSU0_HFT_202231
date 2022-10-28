using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IFixedWageOrderLogic
    {
        void Create(FixedWageOrder item);
        void Delete(int id);
        FixedWageOrder Read(int id);
        IQueryable<FixedWageOrder> ReadAll();
        void Update(FixedWageOrder item);
        public double? IncomeInMonth(int month);
    }
}