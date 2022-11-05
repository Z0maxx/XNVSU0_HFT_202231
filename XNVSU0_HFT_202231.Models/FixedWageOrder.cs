using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XNVSU0_HFT_202231.Models
{
    [DisplayName("Fixed wage order")]
    public class FixedWageOrder : Order
    {
        [Required(ErrorMessage = "Event type id is required")]
        [DisplayName("Event type id")]
        public int? EventTypeId { get; set; }
        public virtual FixedWageEmployee Employee { get; set; }
        [DisplayName("Event type")]
        public virtual EventType EventType { get; set; }
        public FixedWageOrder()
        {
        }
        public FixedWageOrder(int id, DateTime orderDate, int employeeId, int eventTypeId)
            :base(id, orderDate, employeeId)
        {
            EventTypeId = eventTypeId;
        }
        public override bool Equals(object obj)
        {
            if (obj is not FixedWageOrder other) return false;
            return OrderDate == other.OrderDate && FirstName == other.FirstName && LastName == other.LastName && EmailAddress == other.EmailAddress && EmployeeId == other.EmployeeId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
