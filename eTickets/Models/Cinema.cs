﻿using eTickets.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required]
        public string Name{ get; set; }

        [Display(Name = "Cinema Description")]
        public string Description { get; set; }

        public List<Movie> Movies{ get; set; }
    }
}
