using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public abstract class Order : IModel
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Order date is required")]
        [DisplayName("Order date")]
        public DateTime? OrderDate { get; set; }
        [Required(ErrorMessage = "Employee id is required")]
        [DisplayName("Employee id")]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 30 characters")]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 30 characters")]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Email address must be between 10 and 50 characters")]
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
        [DisplayName("Phone number")]
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
