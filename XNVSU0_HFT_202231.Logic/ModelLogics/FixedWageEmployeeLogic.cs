using System;
using System.Collections.Generic;
using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class FixedWageEmployeeLogic : Logic<FixedWageEmployee>, IFixedWageEmployeeLogic
    {
        readonly IRepository<Job> jobRepository;
        public FixedWageEmployeeLogic(IRepository<FixedWageEmployee> repository, IRepository<Job> jobRepository) : base(repository)
        {
            this.jobRepository = jobRepository;
        }
        public override void Create(FixedWageEmployee item)
        {
            if (jobRepository.Read(item.JobId) == null) throw new ArgumentException($"{typeof(Job).GetDisplayName()} by this id not found: {item.JobId}");
            base.Create(item);
        }
        public override void Update(FixedWageEmployee item)
        {
            if (jobRepository.Read(item.JobId) == null) throw new ArgumentException($"{typeof(Job).GetDisplayName()} by this id not found: {item.JobId}");
            base.Update(item);
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
