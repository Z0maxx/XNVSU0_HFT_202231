using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.StatModels
{
    public class Overview : StatModel
    {
        [DisplayName("Number of overall orders")]
        public int OrdersCount { get; set; }
        [DisplayName("Overall income")]
        public double? Income { get; set; }
        [DisplayName("Overall hours of work")]
        public double? Hours { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is not Overview other) return false;
            return OrdersCount == other.OrdersCount && Income == other.Income && Hours == other.Hours;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
