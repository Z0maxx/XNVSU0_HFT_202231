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
        public override FixedWageEmployee Read(int? id)
        {
            return ctx.FixedWageEmployees.FirstOrDefault(emp => emp.Id == id);
        }
        public override void Create(FixedWageEmployee item)
        {
            base.Create(item);
        }
        public override void Update(FixedWageEmployee item)
        {
            var old = ctx.FixedWageEmployees.FirstOrDefault(emp => emp.Id == item.Id);
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
