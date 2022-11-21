using System;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageEmployeeRepository : Repository<HourlyWageEmployee>, IRepository<HourlyWageEmployee>
    {
        public HourlyWageEmployeeRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
    }
}
