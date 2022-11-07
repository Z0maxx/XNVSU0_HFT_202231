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
        public override bool Equals(object obj)
        {
            if (obj is not IncomeFromJob other) return false;
            return Income == other.Income && Job == other.Job;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
