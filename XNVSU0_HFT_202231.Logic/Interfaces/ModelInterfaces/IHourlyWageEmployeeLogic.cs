using System.Collections.Generic;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using System;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageEmployeeLogic : ILogic<HourlyWageEmployee>
    {
        public IEnumerable<EmployeeAverageHours> AverageHours();
        public IEnumerable<HourlyWageEmployee> GetAvailable(DateTime date, int jobId);
    }
}