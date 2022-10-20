using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class JobRespository : Repository<Job>, IRepository<Job>
    {
        public JobRespository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override Job Read(int id)
        {
            return ctx.Jobs.FirstOrDefault(e => e.Id == id);
        }

        public override void Update(Job item)
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
