using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class FixedWageOrderLogic : IFixedWageOrderLogic
    {
        readonly IRepository<FixedWageOrder> repository;
        public FixedWageOrderLogic(IRepository<FixedWageOrder> repository)
        {
            this.repository = repository;
        }
        public void Create(FixedWageOrder item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Order by this id already exists: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "EventTypeId" };
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
        public FixedWageOrder Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Order not found by this id: " + id);
            return result;
        }

        public IQueryable<FixedWageOrder> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(FixedWageOrder item)
        {
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException("Order not found by this id: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "EventTypeId" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
        }
        public double? IncomeInMonth(int month)
        {
            return repository.ReadAll().Where(o => o.OrderDate.Value.Month == month).Sum(o => o.Employee.Wage);
        }
    }
}
