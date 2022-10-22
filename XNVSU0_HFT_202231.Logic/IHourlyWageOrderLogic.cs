using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageOrderLogic
    {
        void Create(HourlyWageOrder item);
        void Delete(int id);
        HourlyWageOrder Read(int id);
        IQueryable<HourlyWageOrder> ReadAll();
        void Update(HourlyWageOrder item);
    }
}