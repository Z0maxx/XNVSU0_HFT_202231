using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class FixedWageEmployee : Employee
    {
        public double Hours { get; set; }
        public virtual ICollection<FixedWageOrder> Orders { get; set; }
        public FixedWageEmployee()
        {
        }
        public FixedWageEmployee(int id, string firstName, string lastName, int jobId, double wage, DateTime hireDate, string emailAddress, string phoneNumber, double hours)
            : base(id, firstName, lastName, jobId, wage, hireDate, emailAddress, phoneNumber)
        {
            Hours = hours;
        }
    }
}
