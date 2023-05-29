using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Models.TableModels;
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
            if (item.OrderDate.Value.Day <= DateTime.Now.Day)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} day must be at least today's date + 1 day");
            }
            if (item.OrderDate.Value.Hour < 8 || item.OrderDate.Value.Hour > 22 || item.OrderDate.Value.Hour == 22 && item.OrderDate.Value.Minute > 0)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} hours must be between 8 and 22");
            }
            if (item.OrderDate.Value.Minute % 15 != 0)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} minutes must be 0, 15, 30 or 45");
            }
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(HourlyWageEmployee).GetDisplayName()} by this Id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date) != null)
            {
                throw new ArgumentException($"There is already an Order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            if (item.Hours < employee.MinHours || item.Hours > employee.MaxHours)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "Hours").GetDisplayName()} must be between {employee.MinHours} and {employee.MaxHours} for {employee.FirstName} {employee.LastName}");
            }
            DateTime date = item.OrderDate.Value;
            item.OrderDate = DateTime.Parse($"{date.Year}.{date.Month}.{date.Day} {date.Hour}:{date.Minute}");
            base.Create(item);
        }
        public override void Update(HourlyWageOrder item)
        {
            if (item.OrderDate.Value.Hour < 8 || item.OrderDate.Value.Hour > 22 || item.OrderDate.Value.Hour == 22 && item.OrderDate.Value.Minute > 0)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} hours must be between 8 and 22");
            }
            if (item.OrderDate.Value.Minute % 15 != 0) throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} minutes must be 0, 15, 30 or 45");
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(HourlyWageEmployee).GetDisplayName()} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date && o.Id != item.Id) != null)
            {
                throw new ArgumentException($"There is already an Order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            if (item.Hours < employee.MinHours || item.Hours > employee.MaxHours)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} must be between {employee.MinHours} and {employee.MaxHours} for {employee.FirstName} {employee.LastName}");
            }
            DateTime date = item.OrderDate.Value;
            item.OrderDate = DateTime.Parse($"{date.Year}.{date.Month}.{date.Day} {date.Hour}:{date.Minute}");
            base.Update(item);
        }
        public IEnumerable<HourlyWageOrder> GetAllForCustomer(DateTime? orderDate, string firstName, string lastName, string emailAddress)
        {
            return this.repository.ReadAll().Where(o => o.OrderDate == orderDate && o.FirstName == firstName && o.LastName == lastName && o.EmailAddress == emailAddress);
        }
        public IEnumerable<HourlyWageEmployee> GetAvailableEmployeesForOrder(Order order)
        {
            var allForCustomerEmployeeIds = GetAllForCustomer(order.OrderDate, order.FirstName, order.LastName, order.EmailAddress).Select(o => o.EmployeeId);
            return this.employeeRepository.ReadAll().Where(e => !allForCustomerEmployeeIds.Contains(e.Id));
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
