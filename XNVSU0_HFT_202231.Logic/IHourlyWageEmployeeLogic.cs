using System.Linq;
using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IHourlyWageEmployeeLogic
    {
        void Create(HourlyWageEmployee item);
        void Delete(int id);
        HourlyWageEmployee Read(int id);
        IQueryable<HourlyWageEmployee> ReadAll();
        void Update(HourlyWageEmployee item);
    }
}