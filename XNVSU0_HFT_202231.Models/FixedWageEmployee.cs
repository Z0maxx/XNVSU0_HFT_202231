﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models
{
    public class FixedWageEmployee : Employee
    {
        [Range(1, 12, ErrorMessage = "Work hours must be between 1 and 12")]
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
    }
}
