using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.StatModels
{
    public class EmployeeOrdersCount : StatModel
    {
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        [DisplayName("Number of orders")]
        public int OrdersCount { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is not EmployeeOrdersCount other) return false;
            return EmployeeName == other.EmployeeName && OrdersCount == other.OrdersCount;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
