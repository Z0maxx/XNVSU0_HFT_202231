using System;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Repository
{
    public class HourlyWageOrderRepository : Repository<HourlyWageOrder>, IRepository<HourlyWageOrder>
    {
        public HourlyWageOrderRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
    }
}
