using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class FixedWageOrderRespository : Repository<FixedWageOrder>, IRepository<FixedWageOrder>
    {
        public FixedWageOrderRespository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override FixedWageOrder Read(int? id)
        {
            return ctx.FixedWageOrders.FirstOrDefault(order => order.Id == id);
        }
        public override void Create(FixedWageOrder item)
        {
            if (ctx.FixedWageEmployees.FirstOrDefault(emp => emp.Id == item.EmployeeId) == null) throw new ArgumentException("Employee by this id not found: " + item.EmployeeId);
            if (ctx.EventTypes.FirstOrDefault(eventType => eventType.Id == item.EventTypeId) == null) throw new ArgumentException("Event type by this id not found: " + item.EventTypeId);
            base.Create(item);
        }
        public override void Update(FixedWageOrder item)
        {
            if (ctx.FixedWageEmployees.FirstOrDefault(emp => emp.Id == item.EmployeeId) == null) throw new ArgumentException("Employee by this id not found: " + item.EmployeeId);
            if (ctx.EventTypes.FirstOrDefault(eventType => eventType.Id == item.EventTypeId) == null) throw new ArgumentException("Event type by this id not found: " + item.EventTypeId);
            var old = ctx.FixedWageOrders.FirstOrDefault(order => order.Id == item.Id);
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
