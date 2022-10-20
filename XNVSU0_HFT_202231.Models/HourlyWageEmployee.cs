using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageEmployee : Employee
    {
        public double MinHours { get; set; }
        public double MaxHours { get; set; }
        public List<HourlyWageOrder> Orders { get; set; }
        public HourlyWageEmployee()
        {
        }

        public HourlyWageEmployee(int id, string firstName, string lastName, int jobId, double wage, DateTime hireDate, string emailAddress, string phoneNumber, double minHours, double maxHours)
            :base(id, firstName, lastName, jobId, wage, hireDate, emailAddress, phoneNumber)
        {
            MinHours = minHours;
            MaxHours = maxHours;
        }
    }
}
