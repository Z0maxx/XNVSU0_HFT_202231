﻿using System;
using System.Collections.Generic;

namespace XNVSU0_HFT_202231.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public double Wage { get; set; }
        public DateTime HireDate { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Employee()
        {
        }
    }
}