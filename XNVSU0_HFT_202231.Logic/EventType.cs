using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class EventTypeLogic
    {
        readonly IRepository<EventType> repository;
        public EventTypeLogic(IRepository<EventType> repository)
        {
            this.repository = repository;
        }
        public void Create(EventType item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Event Tpye by this id already exists: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "Name" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Create(item);
        }

        public void Delete(int id)
        {
            Read(id);
            repository.Delete(id);
        }
        public EventType Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Event type not found by this id: " + id);
            return result;
        }

        public IQueryable<EventType> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(EventType item)
        {
            if (repository.Read(item.Id) == null) throw new ArgumentException("Event type not found by this id: " + item.Id);
            repository.Update(item);
        }
    }
}
