using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLink.Models
{
   public class LocationViewModel
    {
        [Key]
        public int LocationId { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public double LocationLong { get; set; }

        public double LocationLat { get; set; }

        public double personlat { get; set; }
        
        public double personlng { get; set; }

    }
}
