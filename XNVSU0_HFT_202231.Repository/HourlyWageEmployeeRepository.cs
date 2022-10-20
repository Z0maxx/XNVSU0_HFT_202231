using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageEmployeeRespository : Repository<HourlyWageEmployee>, IRepository<HourlyWageEmployee>
    {
        public HourlyWageEmployeeRespository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override HourlyWageEmployee Read(int? id)
        {
            return ctx.HourlyWageEmployees.FirstOrDefault(emp => emp.Id == id);
        }
        public override void Update(HourlyWageEmployee item)
        {
            var old = ctx.HourlyWageEmployees.FirstOrDefault(emp => emp.Id == item.Id);
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
