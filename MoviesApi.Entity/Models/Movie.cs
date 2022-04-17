using System;
using System.ComponentModel.DataAnnotations;


namespace MoviesApi.Entity.Models
{
    public class Movie
    {
        public Movie() { } // empty constructor for FAKEITEASY Tests
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public DateTime ReleaseDate { get; set; }


        public int TheaterId { get; set; }
    }
}
