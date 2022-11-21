using System;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;

namespace XNVSU0_HFT_202231.Repository
{
    public class FixedWageOrderRepository : Repository<FixedWageOrder>, IRepository<FixedWageOrder>
    {
        public FixedWageOrderRepository(EmployeeDbContext ctx) : base(ctx)
        {
        }
    }
}
