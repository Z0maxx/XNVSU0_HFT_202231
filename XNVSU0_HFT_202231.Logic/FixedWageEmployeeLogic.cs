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
    public class FixedWageEmployeeLogic : Logic<FixedWageEmployee>, IFixedWageEmployeeLogic
    {
        public FixedWageEmployeeLogic(IRepository<FixedWageEmployee> repository) : base(repository, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "Hours" })
        {
        }
        public IEnumerable<EmployeeOrdersCount> MostPopular()
        {
            int mostOrders = repository.ReadAll().Max(e => e.Orders.Count);
            var mostPopular = repository.ReadAll().Where(e => e.Orders.Count == mostOrders).Select(e => new EmployeeOrdersCount()
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                OrdersCount = e.Orders.Count
            });
            return mostPopular;
        }
    }
}
