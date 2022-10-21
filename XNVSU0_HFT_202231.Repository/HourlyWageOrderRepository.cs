using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageOrderRespository : Repository<HourlyWageOrder>, IRepository<HourlyWageOrder>
    {
        public HourlyWageOrderRespository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override HourlyWageOrder Read(int? id)
        {
            return ctx.HourlyWageOrders.FirstOrDefault(order => order.Id == id);
        }
        public override void Create(HourlyWageOrder item)
        {
            if (ctx.HourlyWageEmployees.FirstOrDefault(emp => emp.Id == item.EmployeeId) == null) throw new ArgumentException("Employee by this id not found: " + item.EmployeeId);
            base.Create(item);
        }
        public override void Update(HourlyWageOrder item)
        {
            if (ctx.HourlyWageEmployees.FirstOrDefault(emp => emp.Id == item.EmployeeId) == null) throw new ArgumentException("Employee by this id not found: " + item.EmployeeId);
            var old = ctx.HourlyWageEmployees.FirstOrDefault(order => order.Id == item.Id);
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
