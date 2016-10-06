using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LifeLink.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }

        public string title { get; set; }

        [Display(Name = "Start Time")]
        public DateTime start { get; set; }

        [Display(Name = "End Time")]
        public DateTime end { get; set; }

        public string Status { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }

        [Display(Name = "Username")]
        public ApplicationUser AspNetUsers { get; set; }
    }
}