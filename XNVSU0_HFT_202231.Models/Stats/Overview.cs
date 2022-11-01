using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class Overview : Stat
    {
        [DisplayName("Number of overall orders")]
        public int OrdersCount { get; set; }
        [DisplayName("Overall income")]
        public double? Income { get; set; }
        [DisplayName("Overall hours of work")]
        public double? Hours { get; set; }
    }
}
