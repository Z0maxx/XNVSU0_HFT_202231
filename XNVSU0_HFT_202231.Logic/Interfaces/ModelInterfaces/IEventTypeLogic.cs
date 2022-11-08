﻿using System.Collections.Generic;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IEventTypeLogic : ILogic<EventType>
    {
        public IEnumerable<IncomeFromJob> IncomeByJobs(int eventTypeId);
    }
}