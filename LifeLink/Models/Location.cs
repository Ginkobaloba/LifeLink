using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LifeLink.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        public double LocationLong { get; set; }

        public double LocationLat { get; set; }
    }
}