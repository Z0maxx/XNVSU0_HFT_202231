using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class EventTypeLogic : Logic<EventType>, IEventTypeLogic
    {
        public EventTypeLogic(IRepository<EventType> repository) : base(repository, new string[] { "Id", "Name" })
        {
        }
        public IEnumerable<IncomeFromJob> IncomeByJobs(int eventTypeId)
        {
            var a = Read(eventTypeId).Orders.GroupBy(o => o.Employee.Job.Name, (jobName, orders) => new IncomeFromJob()
            {
                Income = orders.Sum(o => o.Employee.Wage),
                Job = jobName
            });
            return a;
        }
    }
}
