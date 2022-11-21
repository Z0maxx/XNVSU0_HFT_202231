﻿using System.Collections.Generic;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageOrderLogic : ILogic<HourlyWageOrder>
    {
        public IEnumerable<IncomeFromOrder> Overview();
    }
}