using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    [DisplayName("Hourly wage employee")]
    public class HourlyWageEmployee : Employee
    {
        [Required(ErrorMessage = "Minimum work hours is required")]
        [Range(1, 10, ErrorMessage = "Minimum work hours must be between 1 and 10")]
        [DisplayName("Minimum work hours")]
        public double? MinHours { get; set; }
        [Required(ErrorMessage = "Maximum work hours is required")]
        [Range(2, 12, ErrorMessage = "Maximum work hours must be between 2 and 12")]
        [DisplayName("Maximum work hours")]
        public double? MaxHours { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<HourlyWageOrder> Orders { get; set; }
        public HourlyWageEmployee()
        {
        }

        public HourlyWageEmployee(int id, string firstName, string lastName, int jobId, double wage, DateTime hireDate, string emailAddress, string phoneNumber, double minHours, double maxHours)
            :base(id, firstName, lastName, jobId, wage, hireDate, emailAddress, phoneNumber)
        {
            MinHours = minHours;
            MaxHours = maxHours;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Job} - Wage: {Wage} Work hours: {MinHours} - {MaxHours}";
        }
        public override bool Equals(object obj)
        {
            if (obj is not HourlyWageEmployee other) return false;
            return FirstName == other.FirstName && LastName == other.LastName && EmailAddress == other.EmailAddress;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
