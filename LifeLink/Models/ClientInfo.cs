using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LifeLink.Models
{
    public class ClientInfo
    {

        [Key]
        public string CientInfoId { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Sex { get; set; }

        public string BloodType { get; set; }

        public float height { get; set; }

        public int weight { get; set; }

        public bool Approved { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public ApplicationUser AspNetUsers { get; set; }


    }
}