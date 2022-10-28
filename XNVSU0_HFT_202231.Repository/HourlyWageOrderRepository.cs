using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageOrderRepository : Repository<HourlyWageOrder>, IRepository<HourlyWageOrder>
    {
        public HourlyWageOrderRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override HourlyWageOrder Read(int? id)
        {
            return ctx.HourlyWageOrders.FirstOrDefault(order => order.Id == id);
        }
        public override void Create(HourlyWageOrder item)
        {
            base.Create(item);
        }
        public override void Update(HourlyWageOrder item)
        {
            var old = ctx.HourlyWageOrders.FirstOrDefault(order => order.Id == item.Id);
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
