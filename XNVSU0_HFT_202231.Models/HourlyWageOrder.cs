using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public class HourlyWageOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public virtual HourlyWageEmployee Employee { get; set; }
        [Range(1, 12)]
        public double Hours { get; set; }
        public HourlyWageOrder()
        {
        }
        public HourlyWageOrder(int id, DateTime orderDate, int employeeId, double hours)
        {
            Id = id;
            OrderDate = orderDate;
            EmployeeId = employeeId;
            Hours = hours;
        }
    }
}
