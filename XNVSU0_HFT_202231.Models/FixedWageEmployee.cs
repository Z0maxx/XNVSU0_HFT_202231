using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models
{
    [DisplayName("Fixed wage employee")]
    public class FixedWageEmployee : Employee, IModel
    {
        [Range(1, 12, ErrorMessage = "Work hours must be between 1 and 12")]
        [DisplayName("Work hours")]
        public double? Hours { get; set; }
        [JsonIgnore]
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
            var other = obj as FixedWageEmployee;
            return FirstName == other.FirstName && LastName == other.LastName && PhoneNumber == other.PhoneNumber;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
