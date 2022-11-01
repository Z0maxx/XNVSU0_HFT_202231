using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Model
    {
        protected EmployeeDbContext ctx;
        public Repository(EmployeeDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public T Read(int? id)
        {
            return ctx.Set<T>().FirstOrDefault(model => model.Id == id);
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public void Update(T item)
        {
            var old = ctx.Set<T>().FirstOrDefault(model => model.Id == item.Id);
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
