using XNVSU0_HFT_202231.Models;

namespace XNVSU0_HFT_202231.Logic
{
    public interface IFixedWageOrderLogic : ILogic<FixedWageOrder>
    {
        public double? IncomeInYear(int year);
    }
}