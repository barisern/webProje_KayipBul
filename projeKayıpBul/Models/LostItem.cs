﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projeKayıpBul.Models
{
    public class LostItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LosingDate { get; set; }

        public MoreInfo MoreInfo { get; set; }

        public Status Status { get; set; }

        public byte[] Photo { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }

    public enum MoreInfo
    {
        None,
        Rewarded,
        Urgent
    }
    public enum Status
    {
        Searching,
        Lost,
        Found
    }
}
