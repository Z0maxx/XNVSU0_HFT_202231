using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class FixedWageOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int EmployeeId { get; set; }
        public HourlyWageEmployee Employee { get; set; }
        public FixedWageOrder()
        {
        }
        public FixedWageOrder(int id, DateTime orderDate, int employeeId)
        {
            Id = id;
            OrderDate = orderDate;
            EmployeeId = employeeId;
        }
    }
}
