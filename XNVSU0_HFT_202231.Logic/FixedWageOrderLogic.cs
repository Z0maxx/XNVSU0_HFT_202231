using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class FixedWageOrderLogic : Logic<FixedWageOrder>, IFixedWageOrderLogic
    {
        public FixedWageOrderLogic(IRepository<FixedWageOrder> repository) : base(repository, new string[] { "Id", "FirstName", "LastName", "OrderDate", "EmailAddress", "EmployeeId", "EventTypeId" })
        {
        }
        public double? IncomeInMonth(int month)
        {
            return repository.ReadAll().Where(o => o.OrderDate.Value.Month == month).Sum(o => o.Employee.Wage);
        }
    }
}
