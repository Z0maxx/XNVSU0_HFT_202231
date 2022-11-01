using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class EventTypeRepository : Repository<EventType>, IRepository<EventType>
    {
        public EventTypeRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
    }
}
