using System;
using System.ComponentModel.DataAnnotations;

namespace XNVSU0_HFT_202231.Models
{
    public class FixedWageOrder : Order
    {
        [Required(ErrorMessage = "Event type id is required")]
        public int? EventTypeId { get; set; }
        public virtual FixedWageEmployee Employee { get; set; }
        public virtual EventType EventType { get; set; }
        public FixedWageOrder()
        {
        }
        public FixedWageOrder(int id, DateTime orderDate, int employeeId, int eventTypeId)
            :base(id, orderDate, employeeId)
        {
            EventTypeId = eventTypeId;
        }
    }
}
