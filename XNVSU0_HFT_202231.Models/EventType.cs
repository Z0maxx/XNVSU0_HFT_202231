﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models
{
    public class EventType
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
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
    }
}
