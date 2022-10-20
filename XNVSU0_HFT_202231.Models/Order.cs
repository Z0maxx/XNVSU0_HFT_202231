using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public abstract class Order
    {
        [Key]
        [Required(ErrorMessage = "Order id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Order id must be greater than 0")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Order date is required")]
        public DateTime? OrderDate { get; set; }
        [Required(ErrorMessage = "Order employee id is required")]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = "Order first name is required")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Order last name is required")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Order email address is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Order email address is required")]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        protected Order()
        {
        }
        protected Order(int id, DateTime orderDate, int employeeId)
        {
            Id = id;
            OrderDate = orderDate;
            EmployeeId = employeeId;
        }
    }
}
