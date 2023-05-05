using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    [DisplayName("Fixed Wage Order")]
    public class FixedWageOrder : Order
    {
        [Required(ErrorMessage = "Event type Id is required")]
        [DisplayName("Event type Id")]
        public int? EventTypeId { get; set; }
        [NotMapped]
        public virtual FixedWageEmployee Employee { get; set; }
        [DisplayName("Event type")]
        [NotMapped]
        public virtual EventType EventType { get; set; }
        public FixedWageOrder()
        {
        }
        public FixedWageOrder(int id, DateTime orderDate, string firstName, string lastName, string emailAddress, string phoneNumber, int employeeId, int eventTypeId)
            :base(id, orderDate, firstName, lastName, emailAddress, phoneNumber, employeeId)
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
