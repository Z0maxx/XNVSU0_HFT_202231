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
    public class HourlyWageOrderLogic : Logic<HourlyWageOrder>, IHourlyWageOrderLogic
    {
        public HourlyWageOrderLogic(IRepository<HourlyWageOrder> repository) : base(repository, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "Hours" })
        {
        }
        public override void Create(HourlyWageOrder item)
        {
            base.Create(item);
            if (item.Employee == null || item.Id == null) item = repository.ReadAll().Where(order => order.Equals(item)).First();
            if (item.Hours < item.Employee.MinHours || item.Hours > item.Employee.MaxHours)
            {
                repository.Delete((int)item.Id);
                throw new ArgumentException($"Hours must be between {item.Employee.MinHours} and {item.Employee.MaxHours}");
            }
        }
        public override void Update(HourlyWageOrder item)
        {
            var old = repository.Read(item.Id);
            var oldCopy = new HourlyWageOrder();
            foreach (var propInfo in item.GetType().GetProperties())
            {
                propInfo.SetValue(oldCopy, propInfo.GetValue(old));
            }
            base.Update(item);
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
