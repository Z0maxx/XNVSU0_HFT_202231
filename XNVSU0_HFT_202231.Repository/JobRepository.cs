using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class JobRepository : Repository<Job>, IRepository<Job>
    {
        public JobRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
        public override Job Read(int? id)
        {
            return ctx.Jobs.FirstOrDefault(job => job.Id == id);
        }

        public override void Update(Job item)
        {
            var old = ctx.Jobs.FirstOrDefault(job => job.Id == item.Id);
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
