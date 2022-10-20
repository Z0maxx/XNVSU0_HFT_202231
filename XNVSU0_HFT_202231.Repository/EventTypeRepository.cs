using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class EventTypeRespository : Repository<EventType>, IRepository<EventType>
    {
        public EventTypeRespository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override EventType Read(int id)
        {
            return ctx.EventTypes.FirstOrDefault(e => e.Id == id);
        }
        public override void Update(EventType item)
        {
            var old = ctx.HourlyWageEmployees.FirstOrDefault(car => car.Id == item.Id);
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
