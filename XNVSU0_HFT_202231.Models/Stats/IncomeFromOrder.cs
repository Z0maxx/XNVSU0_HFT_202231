using System;
using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class IncomeFromOrder : IStat
    {
        [DisplayName("Order time")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        public double? Income { get; set; }
    }
}
