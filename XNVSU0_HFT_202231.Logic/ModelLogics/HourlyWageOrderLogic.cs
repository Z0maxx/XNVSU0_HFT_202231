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
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(HourlyWageEmployee).GetDisplayName()} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date) != null)
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
            if (employee == null) throw new ArgumentException($"{typeof(HourlyWageEmployee).GetDisplayName()} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date && o.Id != item.Id) != null)
            {
                throw new ArgumentException($"There is already an order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            if (item.Hours < employee.MinHours || item.Hours > employee.MaxHours)
            {
                throw new ArgumentException($"Work hours must be between {employee.MinHours} and {employee.MaxHours} for {employee.FirstName} {employee.LastName}");
            }
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
