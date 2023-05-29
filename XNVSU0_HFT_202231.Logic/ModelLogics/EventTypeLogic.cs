using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class EventTypeLogic : Logic<EventType>, IEventTypeLogic
    {
        public EventTypeLogic(IRepository<EventType> repository) : base(repository)
        {
        }
        public IEnumerable<IncomeFromJob> IncomeByJobs(int eventTypeId)
        {
            var a = Read(eventTypeId).Orders.Where(o => o.Employee != null).GroupBy(o => o.Employee.Job.Name, (jobName, orders) => new IncomeFromJob()
            {
                Income = orders.Sum(o => o.Employee.Wage),
                Job = jobName
            });
            return a;
        }
    }
}
