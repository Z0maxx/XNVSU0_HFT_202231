﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    [DisplayName("Hourly Wage Order")]
    public class HourlyWageOrder : Order
    {
        [Required(ErrorMessage = "Work Hours is required")]
        [DisplayName("Work Hours")]
        public double? Hours { get; set; }
        [NotMapped]
        public virtual HourlyWageEmployee Employee { get; set; }
        public HourlyWageOrder()
        {
        }
        public HourlyWageOrder(int id, DateTime orderDate, string firstName, string lastName, string emailAddress, string phoneNumber, int employeeId, int hours)
            : base(id, orderDate, firstName, lastName, emailAddress, phoneNumber, employeeId)
        {
            Hours = hours;
        }
        public override bool Equals(object obj)
        {
            if (obj is not HourlyWageOrder other) return false;
            return OrderDate == other.OrderDate && FirstName == other.FirstName && LastName == other.LastName && EmailAddress == other.EmailAddress && EmployeeId == other.EmployeeId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
