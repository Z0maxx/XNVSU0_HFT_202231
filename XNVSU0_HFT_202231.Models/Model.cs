using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models
{
    public abstract class Model
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
    }
}
