using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Entities
{//one trailers belongs to one movie, one movie have many trailers
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        //this is navigation properity:
        //if you have trailer id you wanto find movie datails you will ues this
    }
}
