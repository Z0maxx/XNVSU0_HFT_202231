using System;
using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class IncomeFromOrder : Stat
    {
        [DisplayName("Order time")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        public double? Income { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is not IncomeFromOrder other) return false;
            return OrderDate == other.OrderDate && EmployeeName == other.EmployeeName && Income == other.Income;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
