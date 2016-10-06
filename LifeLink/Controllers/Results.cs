using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLink.Controllers
{
    public class Result
    {
        public List<HTMLAttribution> html_attributions { get; set; }
        public string next_page_token { get; set; }
        public List<Place> results { get; set; }
        public string status { get; set; }
        public class HTMLAttribution { }

    }
}
