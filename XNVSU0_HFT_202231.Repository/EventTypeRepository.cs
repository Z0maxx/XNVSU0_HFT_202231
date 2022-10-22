﻿using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class EventTypeRepository : Repository<EventType>, IRepository<EventType>
    {
        public EventTypeRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override EventType Read(int? id)
        {
            return ctx.EventTypes.FirstOrDefault(eventType => eventType.Id == id);
        }
        public override void Update(EventType item)
        {
            var old = ctx.EventTypes.FirstOrDefault(eventType => eventType.Id == item.Id);
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.GetAccessors().FirstOrDefault(a => a.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
