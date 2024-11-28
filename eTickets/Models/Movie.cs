﻿using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id{ get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price{ get; set; }

        public string ImageURL { get; set; }

        public DateTime StartDate{ get; set; }

        public DateTime EndDate{ get; set; }

        public MovieCategory MovieCategory{ get; set; }

        public List<Actor_Movie> Actor_Movies { get; set; }

        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

    }
}
