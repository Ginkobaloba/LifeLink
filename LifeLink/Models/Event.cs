using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LifeLink.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        public DateTime EventDate { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }


        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public ApplicationUser AspNetUsers { get; set; }
    }
}