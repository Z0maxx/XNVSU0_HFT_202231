using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    [DisplayName("Fixed Wage Employee")]
    public class FixedWageEmployee : Employee
    {
        [Required(ErrorMessage = "Work Hours is required")]
        [Range(1, 12, ErrorMessage = "Work Hours must be between 1 and 12")]
        [DisplayName("Work Hours")]
        public double? Hours { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<FixedWageOrder> Orders { get; set; }
        public FixedWageEmployee()
        {
        }
        public FixedWageEmployee(int id, string firstName, string lastName, int jobId, double wage, DateTime hireDate, string emailAddress, string phoneNumber, double hours)
            : base(id, firstName, lastName, jobId, wage, hireDate, emailAddress, phoneNumber)
        {
            Hours = hours;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Job} - Wage: {Wage}; Work hours: {Hours}";
        }
        public override bool Equals(object obj)
        {
            if (obj is not FixedWageEmployee other) return false;
            return FirstName == other.FirstName && LastName == other.LastName && EmailAddress == other.EmailAddress;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
