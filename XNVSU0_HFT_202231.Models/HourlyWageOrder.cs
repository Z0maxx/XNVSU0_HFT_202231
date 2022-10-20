using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageOrder : Order
    {
        [Required(ErrorMessage = "Number of hours is required")]
        [Range(1, 12, ErrorMessage = "Number of hours must be between 1 and 12")]
        public double? Hours { get; set; }
        public virtual HourlyWageEmployee Employee { get; set; }
        public HourlyWageOrder()
        {
        }
        public HourlyWageOrder(int id, DateTime orderDate, int employeeId, int hours)
            : base(id, orderDate, employeeId)
        {
            Hours = hours;
        }
    }
}
