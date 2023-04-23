using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Repository;
using System.Security.Cryptography;

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
            if (jobRepository.Read(item.JobId) == null) throw new ArgumentException($"{typeof(Job).GetDisplayName()} by this id not found: {item.JobId}");
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            base.Create(item);
        }
        public override void Update(HourlyWageEmployee item)
        {
            if (jobRepository.Read(item.JobId) == null) throw new ArgumentException($"{typeof(Job).GetDisplayName()} by this id not found: {item.JobId}");
            if (item.MinHours >= item.MaxHours) throw new ArgumentException("Minimum hours must be less than maximum hours");
            base.Update(item);
        }
        public IEnumerable<EmployeeAverageHours> AverageHours()
        {
            var averageHours = repository.ReadAll().Where(e => e.Orders.Any()).Select(e => new EmployeeAverageHours()
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                AverageHours = e.Orders.Average(o => o.Hours)
            });
            return averageHours;
        }
        public IEnumerable<HourlyWageEmployee> GetAvailable(DateTime date, int jobId)
        {
            return repository.ReadAll().Where(e => e.Orders.Count(o => o.OrderDate.Value.Date == date.Date) == 0 && e.Job.Id == jobId);
        }
    }
}
