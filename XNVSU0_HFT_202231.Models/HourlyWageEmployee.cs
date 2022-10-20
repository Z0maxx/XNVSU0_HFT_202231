using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageEmployee
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public double MinHours { get; set; }
        public double MaxHours { get; set; }
        public List<HourlyWageOrder> Orders { get; set; }
        public HourlyWageEmployee()
        {
        }
    }
}
