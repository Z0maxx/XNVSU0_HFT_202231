using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageOrderLogic
    {
        void Create(HourlyWageOrder item);
        void Delete(int id);
        HourlyWageOrder Read(int id);
        IQueryable<HourlyWageOrder> ReadAll();
        void Update(HourlyWageOrder item);
        public IEnumerable<IncomeFromOrder> Overview();
    }
}