using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public abstract class Logic<T> : ILogic<T> where T: TableModel
    {
        readonly protected IRepository<T> repository;
        public Logic(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public virtual void Create(T item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException($"{typeof(T).GetDisplayName()} by this id already exists: {item.Id}");
            foreach (var propInfo in item.GetType().GetProperties())
            {
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Create(item);
        }
        public virtual void CreateBulk(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Create(item);
            }
        }
        public virtual void Delete(int id)
        {
            Read(id);
            repository.Delete(id);
        }
        public T Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException($"{typeof(T).GetDisplayName()} not found by this id: {id}");
            return result;
        }

        public IQueryable<T> ReadAll()
        {
            return repository.ReadAll();
        }

        public IEnumerable<T> ReadBulk(IEnumerable<int> ids)
        {
            return repository.ReadAll().Where(item => ids.Contains((int)item.Id));
        }

        public virtual void Update(T item)
        {
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException($"{typeof(T).GetDisplayName()} not found by this id: {item.Id}");
            foreach (var propInfo in item.GetType().GetProperties())
            {
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
        }

        public virtual void UpdateBulk(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Update(item);
            }
        }
    }
}
