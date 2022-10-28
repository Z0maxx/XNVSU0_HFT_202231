using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class FixedWageOrderRepository : Repository<FixedWageOrder>, IRepository<FixedWageOrder>
    {
        public FixedWageOrderRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override FixedWageOrder Read(int? id)
        {
            return ctx.FixedWageOrders.FirstOrDefault(order => order.Id == id);
        }
        public override void Create(FixedWageOrder item)
        {
            base.Create(item);
        }
        public override void Update(FixedWageOrder item)
        {
            var old = ctx.FixedWageOrders.FirstOrDefault(order => order.Id == item.Id);
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
