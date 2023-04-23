using System.Collections.Generic;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using System;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IFixedWageEmployeeLogic : ILogic<FixedWageEmployee>
    {
        public IEnumerable<EmployeeOrdersCount> MostPopular();
        public IEnumerable<FixedWageEmployee> GetAvailable(DateTime date, int jobId);
    }
}