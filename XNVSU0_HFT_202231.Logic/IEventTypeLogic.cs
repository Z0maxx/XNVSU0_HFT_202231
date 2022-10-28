using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IEventTypeLogic
    {
        void Create(EventType item);
        void Delete(int id);
        EventType Read(int id);
        IQueryable<EventType> ReadAll();
        void Update(EventType item);
        public IEnumerable<IncomeFromJob> IncomeByJobs(int eventTypeId);
    }
}