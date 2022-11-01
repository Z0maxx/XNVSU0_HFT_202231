using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class EmployeeAverageHours : Stat
    {
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        [DisplayName("Average hours / order")]
        public double? AverageHours { get; set; }
    }
}
