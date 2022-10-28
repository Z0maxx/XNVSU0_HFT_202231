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
    public class HourlyWageOrderLogic : IHourlyWageOrderLogic
    {
        readonly IRepository<HourlyWageOrder> repository;
        public HourlyWageOrderLogic(IRepository<HourlyWageOrder> repository)
        {
            this.repository = repository;
        }
        public void Create(HourlyWageOrder item)
        {
            if (repository.Read(item.Id) != null) throw new ArgumentException("Order by this id already exists: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "Hours" };
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Create(item);
            if (item.Employee == null || item.Id == null) item = repository.ReadAll().Where(order => order.Equals(item)).First();
            if (item.Hours < item.Employee.MinHours || item.Hours > item.Employee.MaxHours)
            {
                repository.Delete((int)item.Id);
                throw new ArgumentException($"Hours must be between {item.Employee.MinHours} and {item.Employee.MaxHours}");
            }
        }

        public void Delete(int id)
        {
            Read(id);
            repository.Delete(id);
        }
        public HourlyWageOrder Read(int id)
        {
            var result = repository.Read(id);
            if (result == null) throw new ArgumentException("Order not found by this id: " + id);
            return result;
        }

        public IQueryable<HourlyWageOrder> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(HourlyWageOrder item)
        {
            if (item.Id == null) throw new ArgumentException("Id is required");
            if (repository.Read(item.Id) == null) throw new ArgumentException("Order not found by this id: " + item.Id);
            PropertyInfo[] propInfos = item.GetType().GetProperties();
            string[] propOrder = { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "Hours" };
            var old = repository.Read(item.Id);
            var oldCopy = new HourlyWageOrder();
            foreach (var prop in propOrder)
            {
                var propInfo = propInfos.First(propInfo => propInfo.Name == prop);
                propInfo.SetValue(oldCopy, propInfo.GetValue(old));
                var attributes = propInfo.GetCustomAttributes<ValidationAttribute>();
                Validator.ValidateValue(propInfo.GetValue(item), new ValidationContext(item), attributes);
            }
            repository.Update(item);
            if (item.Employee == null || item.Id == null) item = repository.ReadAll().Where(order => order.Equals(item)).First();
            if (item.Hours < item.Employee.MinHours || item.Hours > item.Employee.MaxHours)
            {
                repository.Update(oldCopy);
                throw new ArgumentException($"Hours must be between {item.Employee.MinHours} and {item.Employee.MaxHours}");
            }
        }
        public IEnumerable<IncomeFromOrder> Overview()
        {
            return repository.ReadAll().Select(o => new IncomeFromOrder()
            {
                OrderDate = o.OrderDate,
                EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
                Income = o.Hours * o.Employee.Wage
            });
        }
    }
}
