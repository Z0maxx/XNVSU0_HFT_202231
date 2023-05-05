using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    public class Order : TableModel
    {
        [Required(ErrorMessage = "Order Date is required")]
        [DisplayName("Order Date")]
        public DateTime? OrderDate { get; set; }
        [Required(ErrorMessage = "Employee Id is required")]
        [DisplayName("Employee Id")]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 30 characters")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 30 characters")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Email Address must be between 10 and 50 characters")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public Order()
        {
        }
        public Order(int id, DateTime orderDate, string firstName, string lastName, string emailAddress, string phoneNumber, int employeeId)
            :base(id)
        {
            OrderDate = orderDate;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            EmployeeId = employeeId;
        }
    }
}
