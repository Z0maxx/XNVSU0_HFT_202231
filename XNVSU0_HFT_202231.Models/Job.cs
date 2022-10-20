using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<FixedWageEmployee> FixedWageEmployees { get; set; }
        [JsonIgnore]
        public virtual ICollection<HourlyWageEmployee> HourlyWageEmployees { get; set; }
        public Job()
        {
        }
        public Job(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
