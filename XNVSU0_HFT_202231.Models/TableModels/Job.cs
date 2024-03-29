﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    public class Job : TableModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters")]
        public string Name { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<FixedWageEmployee> FixedWageEmployees { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<HourlyWageEmployee> HourlyWageEmployees { get; set; }
        public Job()
        {
        }
        public Job(int id, string name)
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
            if (obj is not Job other) return false;
            return Name == other.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
