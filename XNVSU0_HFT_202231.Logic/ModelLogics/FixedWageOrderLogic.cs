using System;
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
            if (eventTypeRepository.Read(item.EventTypeId) == null) throw new ArgumentException($"{typeof(EventType).GetDisplayName()} by this id not found: {item.EventTypeId}");
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(FixedWageEmployee).GetDisplayName()} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date) != null)
            {
                throw new ArgumentException($"There is already an order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            base.Create(item);
        }
        public override void Update(FixedWageOrder item)
        {
            if (eventTypeRepository.Read(item.EventTypeId) == null) throw new ArgumentException($"{typeof(EventType).GetDisplayName()} by this id not found: {item.EventTypeId}");
            var employee = employeeRepository.Read(item.EmployeeId);
            if (employee == null) throw new ArgumentException($"{typeof(FixedWageEmployee).GetDisplayName()} by this id not found: {item.EmployeeId}");
            if (repository.ReadAll().FirstOrDefault(o => o.EmployeeId == item.EmployeeId && o.OrderDate.Value.Date == item.OrderDate.Value.Date && o.Id != item.Id) != null)
            {
                throw new ArgumentException($"There is already an order for {employee.FirstName} {employee.LastName} on {item.OrderDate.Value.ToShortDateString()}");
            }
            base.Update(item);
        }
        public double? IncomeInYear(int year)
        {
            return repository.ReadAll().Where(o => o.OrderDate.Value.Year == year).Sum(o => o.Employee.Wage);
        }
    }
}
