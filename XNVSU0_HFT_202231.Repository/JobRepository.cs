using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class JobRepository : Repository<Job>, IRepository<Job>
    {
        public JobRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
    }
}
