using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageOrderRespository : Repository<HourlyWageOrder>, IRepository<HourlyWageOrder>
    {
        public HourlyWageOrderRespository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override HourlyWageOrder Read(int id)
        {
            return ctx.HourlyWageOrders.FirstOrDefault(order => order.Id == id);
        }
        public override void Update(HourlyWageOrder item)
        {
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
