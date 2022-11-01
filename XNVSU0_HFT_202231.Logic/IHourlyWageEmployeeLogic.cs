using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageEmployeeLogic : ILogic<HourlyWageEmployee>
    {
        public IEnumerable<EmployeeAverageHours> AverageHours();
    }
}