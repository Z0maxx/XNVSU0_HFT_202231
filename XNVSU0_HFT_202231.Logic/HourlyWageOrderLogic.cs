using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class HourlyWageOrderLogic : Logic<HourlyWageOrder>, IHourlyWageOrderLogic
    {
        readonly IRepository<HourlyWageEmployee> employeeRepository;
        public HourlyWageOrderLogic(IRepository<HourlyWageOrder> repository, IRepository<HourlyWageEmployee> employeeRepository) : base(repository)
        {
            this.employeeRepository = employeeRepository;
        }
        public override void Create(HourlyWageOrder item)
        {
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{GetDisplayName(typeof(HourlyWageEmployee))} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date && o.Id != item.Id) != null)
            {
                throw new ArgumentException($"There is already an order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            if (item.Hours < employee.MinHours || item.Hours > employee.MaxHours)
            {
                throw new ArgumentException($"Work hours must be between {employee.MinHours} and {employee.MaxHours} for {employee.FirstName} {employee.LastName}");
            }
            base.Create(item);
        }
        public override void Update(HourlyWageOrder item)
        {
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{GetDisplayName(typeof(HourlyWageEmployee))} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date && o.Id != item.Id) != null)
            {
                throw new ArgumentException($"There is already an order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            if (item.Hours < item.Employee.MinHours || item.Hours > item.Employee.MaxHours)
            {
                throw new ArgumentException($"Work hours must be between {employee.MinHours} and {employee.MaxHours} for {employee.FirstName} {employee.LastName}");
            }
            base.Update(item);
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
