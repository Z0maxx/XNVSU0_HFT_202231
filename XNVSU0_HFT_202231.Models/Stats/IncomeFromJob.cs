using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class IncomeFromJob : Stat
    {
        public double? Income { get; set; }
        public string Job { get; set; }
    }
}
