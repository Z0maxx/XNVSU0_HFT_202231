using XNVSU0_HFT_202231.Models.TableModels;
using XNVSU0_HFT_202231.Models.StatModels;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IJobLogic : ILogic<Job>
    {
        public OrdersCount OrdersCount(int jobId);
        public Overview OverallOverview();
    }
}