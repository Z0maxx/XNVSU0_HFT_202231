﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    [DisplayName("Event type")]
    public class EventType : TableModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters")]
        public string Name { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<FixedWageOrder> Orders { get; set; }
        public EventType()
        {
        }
        public EventType(int id, string name)
            :base(id)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not EventType other) return false;
            return Name == other.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
