using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class FixedWageEmployee
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public double Hours { get; set; }
        public List<FixedWageOrder> Orders { get; set; }
        public FixedWageEmployee()
        {
        }
    }
}
