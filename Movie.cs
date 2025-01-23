using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppWPF
{
    public class Movie
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Year{ get; set; }
        public string Rating { get; set; }
        public string Review {  get; set; }
        public Movie(string title, string director, string year, string rating, string review) 
        {
            Title = title;
            Director = director;
            Year = year;
            Rating = rating;
            Review = review;
        }
        

        
    }
}
