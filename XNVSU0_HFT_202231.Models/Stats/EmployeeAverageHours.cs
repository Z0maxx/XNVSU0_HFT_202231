using System.ComponentModel;

namespace XNVSU0_HFT_202231.Models.Stats
{
    public class EmployeeAverageHours : Stat
    {
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        [DisplayName("Average hours / order")]
        public double? AverageHours { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is not EmployeeAverageHours other) return false;
            return EmployeeName == other.EmployeeName && AverageHours == other.AverageHours;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
