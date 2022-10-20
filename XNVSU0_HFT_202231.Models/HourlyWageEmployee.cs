﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageEmployee : Employee
    {
        [Range(1, 10)]
        public double MinHours { get; set; }
        [Range(2, 12)]
        public double MaxHours { get; set; }
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
    }
}
