using System.Linq;
using XNVSU0_HFT_202231.Models;
using XNVSU0_HFT_202231.Models.Stats;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IJobLogic
    {
        void Create(Job item);
        void Delete(int id);
        Job Read(int id);
        IQueryable<Job> ReadAll();
        void Update(Job item);
        public OrdersCount OrdersCount(int jobId);
        public Overview OverallOverview();
    }
}