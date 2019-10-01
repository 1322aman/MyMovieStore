using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieStore.Common
{
    public class MovieData
    {
        // Movie table data
       public int MovieId { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public int Rating { get; set; }
        public string Genre { get; set; }
        public int RentCost { get; set; }
        public string Flag { get; set; }
    }
}
