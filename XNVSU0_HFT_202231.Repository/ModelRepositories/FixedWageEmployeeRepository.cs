using System;
using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public class FixedWageEmployeeRepository : Repository<FixedWageEmployee>, IRepository<FixedWageEmployee>
    {
        public FixedWageEmployeeRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
    }
}
