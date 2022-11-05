using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models
{
    [DisplayName("Event type")]
    public class EventType : Model
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters")]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<FixedWageOrder> Orders { get; set; }
        public EventType()
        {
        }
        public EventType(int id, string name)
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
