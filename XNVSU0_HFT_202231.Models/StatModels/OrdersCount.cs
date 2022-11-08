using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class OrdersCount : StatModel
    {
        [DisplayName("Number of fixed wage orders")]
        public int FixedWageOrderCount { get; set; }
        [DisplayName("Number of hourly wage orders")]
        public int HourlyWageOrderCount { get; set; }
        [DisplayName("Number of orders overall")]
        public int OverallCount { 
            get
            {
                return FixedWageOrderCount + HourlyWageOrderCount;
            } 
        }
        public override bool Equals(object obj)
        {
            if (obj is not OrdersCount other) return false;
            return FixedWageOrderCount == other.FixedWageOrderCount && HourlyWageOrderCount == other.HourlyWageOrderCount && OverallCount == other.OverallCount;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
