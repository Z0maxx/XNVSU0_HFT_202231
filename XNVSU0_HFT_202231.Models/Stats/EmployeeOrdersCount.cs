using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class EmployeeOrdersCount : Stat
    {
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        [DisplayName("Number of orders")]
        public int OrdersCount { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as EmployeeOrdersCount;
            return EmployeeName == other.EmployeeName && OrdersCount == other.OrdersCount;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
