using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public abstract class Employee
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 30 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 30 characters")]
        public string LastName { get; set; }
        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        [Range(1000, 99999, ErrorMessage = "Wage must be between 1000 and 99999")]
        public double? Wage { get; set; }
        [Required(ErrorMessage = "Hire date is required")]
        public DateTime? HireDate { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Email address must be between 10 and 50 characters")]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        protected Employee()
        {
        }
        protected Employee(int id, string firstName, string lastName, int jobId, double wage, DateTime hireDate, string emailAddress, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JobId = jobId;
            Wage = wage;
            HireDate = hireDate;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }
    }
}
