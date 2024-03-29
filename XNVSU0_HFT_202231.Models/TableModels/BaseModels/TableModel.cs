﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XNVSU0_HFT_202231.Models.TableModels
{
    public abstract class TableModel
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public TableModel()
        {
        }
        public TableModel(int id)
        {
            Id = id;
        }
    }
}
