﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    public abstract class Employee : TableModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 30 characters")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 30 characters")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Job Id is required")]
        [DisplayName("Job Id")]
        public int? JobId { get; set; }
        [NotMapped]
        public virtual Job Job { get; set; }
        [Range(1000, 99999, ErrorMessage = "Wage must be between 1000 and 99999")]
        public double? Wage { get; set; }
        [Required(ErrorMessage = "Hire Date is required")]
        [DisplayName("Hire Date")]
        public DateTime? HireDate { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Email Address must be between 10 and 50 characters")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        protected Employee()
        {
        }
        protected Employee(int id, string firstName, string lastName, int jobId, double wage, DateTime hireDate, string emailAddress, string phoneNumber)
            :base(id)
        {
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
