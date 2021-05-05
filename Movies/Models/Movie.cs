using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Movie
    {
        public Object _id { get; set; } 
        public string movieTitle { get; set; }
        public string releaseYear { get; set; }
        public string director { get; set; }
        public string writers { get; set; }
        public string stars { get; set; }
        public string story { get; set; }


    }
}
