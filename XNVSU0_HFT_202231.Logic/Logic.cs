using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public abstract class Logic<T> : ILogic<T> where T: Model
    {
        readonly protected IRepository<T> repository;
        readonly string[] propOrder;
        public Logic(IRepository<T> repository, string[] propOrder)
        {
            this.repository = repository;
            this.propOrder = propOrder;
        }
        public virtual void Create(T item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException($"{GetDisplayName(typeof(T))} by this id already exists: {item.Id}");
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Create(item);
        }
        public virtual void Delete(int id)
        {
            Read(id);
            repository.Delete(id);
        }
        public T Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException($"{GetDisplayName(typeof(T))} not found by this id: {id}");
            return result;
        }

        public IQueryable<T> ReadAll()
        {
            return repository.ReadAll();
        }

        public virtual void Update(T item)
        {
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException($"{GetDisplayName(typeof(T))} not found by this id: {item.Id}");
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
        }
        static string GetDisplayName(Type type)
        {
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attribute == null) return type.Name;
            return attribute.DisplayName;
        }
    }
}
