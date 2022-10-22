using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class JobLogic : IJobLogic
    {
        readonly IRepository<Job> repository;
        public JobLogic(IRepository<Job> repository)
        {
            this.repository = repository;
        }
        public void Create(Job item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Job by this id already exists: " + item.Id);
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
        public Job Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Job not found by this id: " + id);
            return result;
        }

        public IQueryable<Job> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Job item)
        {
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException("Job not found by this id: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "Name" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
        }
    }
}
