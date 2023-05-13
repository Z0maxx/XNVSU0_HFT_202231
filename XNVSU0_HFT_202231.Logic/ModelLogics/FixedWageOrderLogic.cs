using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class FixedWageOrderLogic : Logic<FixedWageOrder>, IFixedWageOrderLogic
    {
        readonly IRepository<FixedWageEmployee> employeeRepository;
        readonly IRepository<EventType> eventTypeRepository;
        public FixedWageOrderLogic(IRepository<FixedWageOrder> repository, IRepository<FixedWageEmployee> employeeRepository, IRepository<EventType> eventTypeRepository) : base(repository)
        {
            this.employeeRepository = employeeRepository;
            this.eventTypeRepository = eventTypeRepository;
        }
        public override void Create(FixedWageOrder item)
        {
            if (item.OrderDate.Value.Day <= DateTime.Now.Day)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} day must be at least today's date + 1 day");
            }
            if (item.OrderDate.Value.Hour < 8 || item.OrderDate.Value.Hour > 22 || item.OrderDate.Value.Hour == 22 && item.OrderDate.Value.Minute > 0)
            {
                throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} hours must be between 8 and 22");
            }
            if (item.OrderDate.Value.Minute % 15 != 0) throw new ArgumentException($"{GetPropertyInfo(item, "OrderDate").GetDisplayName()} minutes must be 0, 15, 30 or 45");
            if (eventTypeRepository.Read(item.EventTypeId) == null) throw new ArgumentException($"{typeof(EventType).GetDisplayName()} by this Id not found: {item.EventTypeId}");
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(FixedWageEmployee).GetDisplayName()} by this Id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date) != null)
            {
                throw new ArgumentException($"There is already an Order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            base.Create(item);
        }
        public override void Update(FixedWageOrder item)
        {
            if (eventTypeRepository.Read(item.EventTypeId) == null) throw new ArgumentException($"{typeof(EventType).GetDisplayName()} by this Id not found: {item.EventTypeId}");
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(FixedWageEmployee).GetDisplayName()} by this Id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date && o.Id != item.Id) != null)
            {
                throw new ArgumentException($"There is already an Order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            base.Update(item);
        }
        public IEnumerable<FixedWageOrder> GetAllForCustomer(DateTime? orderDate, string firstName, string lastName, string emailAddress)
        {
            return this.repository.ReadAll().Where(o => o.OrderDate == orderDate && o.FirstName == firstName && o.LastName == lastName && o.EmailAddress == emailAddress);
        }
        public IEnumerable<FixedWageEmployee> GetAvailableEmployeesForOrder(Order order)
        {
            var allForCustomerEmployeeIds = GetAllForCustomer(order.OrderDate, order.FirstName, order.LastName, order.EmailAddress).Select(o => o.EmployeeId);
            return this.employeeRepository.ReadAll().Where(e => !allForCustomerEmployeeIds.Contains(e.Id));
        }
        public double? IncomeInYear(int year)
        {
            return repository.ReadAll().Where(o => o.OrderDate.Value.Year == year).Sum(o => o.Employee.Wage);
        }
    }
}
