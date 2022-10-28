using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageEmployeeLogic
    {
        void Create(HourlyWageEmployee item);
        void Delete(int id);
        HourlyWageEmployee Read(int id);
        IQueryable<HourlyWageEmployee> ReadAll();
        void Update(HourlyWageEmployee item);
        public IEnumerable<EmployeeAverageHours> AverageHours();
    }
}