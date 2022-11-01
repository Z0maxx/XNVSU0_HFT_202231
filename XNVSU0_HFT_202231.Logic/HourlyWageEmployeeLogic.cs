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
    public class HourlyWageEmployeeLogic : Logic<HourlyWageEmployee>, IHourlyWageEmployeeLogic
    {
        public HourlyWageEmployeeLogic(IRepository<HourlyWageEmployee> repository) : base(repository, new string[] { "Id", "FirstName", "LastName", "Wage", "HireDate", "EmailAddress", "MinHours", "MaxHours" })
        {
        }
        public override void Create(HourlyWageEmployee item)
        {
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            base.Create(item);
        }
        public override void Update(HourlyWageEmployee item)
        {
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            base.Update(item);
        }
        public IEnumerable<EmployeeAverageHours> AverageHours()
        {
            var averageHours = repository.ReadAll().Select(e => new EmployeeAverageHours()
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                AverageHours = e.Orders.Average(o => o.Hours)
            });
            return averageHours;
        }
    }
}
