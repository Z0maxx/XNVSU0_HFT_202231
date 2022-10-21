using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageEmployeeRepository : Repository<HourlyWageEmployee>, IRepository<HourlyWageEmployee>
    {
        public HourlyWageEmployeeRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override HourlyWageEmployee Read(int? id)
        {
            return ctx.HourlyWageEmployees.FirstOrDefault(emp => emp.Id == id);
        }
        public override void Create(HourlyWageEmployee item)
        {
            if (ctx.Jobs.FirstOrDefault(job => job.Id == item.JobId) == null) throw new ArgumentException("Job by this id not found: " + item.JobId);
            base.Create(item);
        }
        public override void Update(HourlyWageEmployee item)
        {
            if (ctx.Jobs.FirstOrDefault(job => job.Id == item.JobId) == null) throw new ArgumentException("Job by this id not found: " + item.JobId);
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
