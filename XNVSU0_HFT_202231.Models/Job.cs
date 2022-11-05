using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models
{
    public class Job : Model
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters")]
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
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Job other) return false;
            return Name == other.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
