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
        public int AppointmentID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public bool CancelAppointment { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }


        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public ApplicationUser AspNetUsers { get; set; }
    }
}