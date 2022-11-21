using System.Linq;
using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;
using XNVSU0_HFT_202231.Repository;

namespace XNVSU0_HFT_202231.Logic
{
    public class JobLogic : Logic<Job>, IJobLogic
    {
        public JobLogic(IRepository<Job> repository) : base(repository)
        {
        }
        public OrdersCount OrdersCount(int jobId)
        {
            var job = Read(jobId);
            var ordersCounts = new OrdersCount()
            {
                FixedWageOrderCount = job.FixedWageEmployees.Sum(e => e.Orders.Count),
                HourlyWageOrderCount = job.HourlyWageEmployees.Sum(e => e.Orders.Count)
            };
            return ordersCounts;
        }
        public Overview OverallOverview()
        {
            var overview = new Overview()
            {
                OrdersCount = repository.ReadAll().Sum(j => j.FixedWageEmployees.Sum(e => e.Orders.Count) + j.HourlyWageEmployees.Sum(e => e.Orders.Count)),
                Income = repository.ReadAll().Sum(j => j.FixedWageEmployees.Sum(e => e.Orders.Count * e.Hours * e.Wage) + j.HourlyWageEmployees.Sum(e => e.Orders.Sum(o => o.Hours) * e.Wage)),
                Hours = repository.ReadAll().Sum(j => j.FixedWageEmployees.Sum(e => e.Hours * e.Orders.Count) + j.HourlyWageEmployees.Sum(e => e.Orders.Sum(o => o.Hours)))
            };
            return overview;
        }
    }
}
