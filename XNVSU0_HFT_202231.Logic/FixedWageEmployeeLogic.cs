using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class FixedWageEmployeeLogic
    {
        readonly IRepository<FixedWageEmployee> repository;
        public FixedWageEmployeeLogic(IRepository<FixedWageEmployee> repository)
        {
            this.repository = repository;
        }
        public void Create(FixedWageEmployee item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Employee by this id already exists: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours", "PhoneNumber" };
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
        public FixedWageEmployee Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Employee not found by this id: " + id);
            return result;
        }

        public IQueryable<FixedWageEmployee> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(FixedWageEmployee item)
        {
            if (repository.Read(item.Id) == null) throw new ArgumentException("Employee not found by this id: " + item.Id);
            repository.Update(item);
        }
    }
}
