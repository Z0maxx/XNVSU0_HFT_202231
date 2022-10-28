using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class FixedWageEmployeeLogic : IFixedWageEmployeeLogic
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
            string[] propOrder = { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours" };
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
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException("Employee not found by this id: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours", "PhoneNumber" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
        }
        public IEnumerable<EmployeeOrdersCount> MostPopular()
        {
            int mostOrders = repository.ReadAll().Select(e => e.Orders.Count).OrderByDescending(c => c).Take(1).First();
            var mostPopular = repository.ReadAll().Where(e => e.Orders.Count == mostOrders).Select(e => new EmployeeOrdersCount()
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                OrdersCount = e.Orders.Count
            });
            return mostPopular;
        }
    }
}
