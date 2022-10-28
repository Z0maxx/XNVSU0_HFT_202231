using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class OrdersCount : IStat
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
    }
}
