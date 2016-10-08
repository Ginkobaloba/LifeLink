using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LifeLink.Models
{
    public class AppointmentViewModel 
    {
        public int id { get; set; }

        public string title { get; set; }

        [Display(Name = "Start Time")]
        public DateTime start { get; set; }

        [Display(Name = "End Time")]
        public DateTime end { get; set; }

        public string Status { get; set; }

        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}