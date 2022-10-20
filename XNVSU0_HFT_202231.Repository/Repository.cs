﻿using System.Linq;

namespace XNVSU0_HFT_202231.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
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
        }

        public abstract T Read(int id);

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract void Update(T item);
    }
}
