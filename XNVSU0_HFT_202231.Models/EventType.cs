using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public class EventType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public virtual ICollection<FixedWageEmployee> Employees { get; set; }
        public EventType()
        {
        }
        public EventType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
