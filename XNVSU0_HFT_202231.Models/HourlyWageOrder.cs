using System;
using System.ComponentModel.DataAnnotations;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageOrder : Order, IModel
    {
        [Required(ErrorMessage = "Number of hours is required")]
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
        public override bool Equals(object obj)
        {
            var other = obj as HourlyWageOrder;
            return OrderDate == other.OrderDate && FirstName == other.FirstName && LastName == other.LastName && EmailAddress == other.EmailAddress;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
