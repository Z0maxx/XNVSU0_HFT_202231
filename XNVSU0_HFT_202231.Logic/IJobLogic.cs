using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IJobLogic : ILogic<Job>
    {
        public OrdersCount OrdersCount(int jobId);
        public Overview OverallOverview();
    }
}