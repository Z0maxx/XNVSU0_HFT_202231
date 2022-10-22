using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IJobLogic
    {
        void Create(Job item);
        void Delete(int id);
        Job Read(int id);
        IQueryable<Job> ReadAll();
        void Update(Job item);
    }
}