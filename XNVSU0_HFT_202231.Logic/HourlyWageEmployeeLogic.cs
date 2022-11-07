using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class HourlyWageEmployeeLogic : Logic<HourlyWageEmployee>, IHourlyWageEmployeeLogic
    {
        readonly IRepository<Job> jobRepository;
        public HourlyWageEmployeeLogic(IRepository<HourlyWageEmployee> repository, IRepository<Job> jobRepository) : base(repository)
        {
            this.jobRepository = jobRepository;
        }
        public override void Create(HourlyWageEmployee item)
        {
            if (jobRepository.Read(item.JobId) == null) throw new ArgumentException($"{GetDisplayName(typeof(Job))} by this id not found: {item.JobId}");
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            base.Create(item);
        }
        public override void Update(HourlyWageEmployee item)
        {
            if (jobRepository.Read(item.JobId) == null) throw new ArgumentException($"{GetDisplayName(typeof(Job))} by this id not found: {item.JobId}");
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            base.Update(item);
        }
        public IEnumerable<EmployeeAverageHours> AverageHours()
        {
            var averageHours = repository.ReadAll().Where(e => e.Orders.Count > 0).Select(e => new EmployeeAverageHours()
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                AverageHours = e.Orders.Average(o => o.Hours)
            });
            return averageHours;
        }
    }
}
