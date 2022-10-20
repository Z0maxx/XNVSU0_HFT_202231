using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual HourlyWageEmployee Employee { get; set; }
        public double Hours { get; set; }
        public HourlyWageOrder()
        {
        }
        public HourlyWageOrder(int id, DateTime orderDate, int employeeId, double hours)
        {
            Id = id;
            OrderDate = orderDate;
            EmployeeId = employeeId;
            Hours = hours;
        }
    }
}
