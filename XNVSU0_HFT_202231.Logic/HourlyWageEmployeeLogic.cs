using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class HourlyWageEmployeeLogic
    {
        IRepository<HourlyWageEmployee> repository;
        public HourlyWageEmployeeLogic(IRepository<HourlyWageEmployee> repository)
        {
            this.repository = repository;
        }
        public void Create(HourlyWageEmployee item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Employee by this id already exists: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "MinHours", "MaxHours" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            repository.Create(item);
        }

        public void Delete(int id)
        {
            Read(id);
            repository.Delete(id);
        }
        public HourlyWageEmployee Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Employee not found by this id: " + id);
            return result;
        }

        public IQueryable<HourlyWageEmployee> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(HourlyWageEmployee item)
        {
            if (repository.Read(item.Id) == null) throw new ArgumentException("Employee not found by this id: " + item.Id);
            repository.Update(item);
        }
    }
}
