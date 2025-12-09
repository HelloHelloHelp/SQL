using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesAPI.Models
{
    [Table("movie_info", Schema = "dbo")]
    public class Movie
    {
        public Movie() { }

        public int ID { get; set; }
        public string? Title { get; set; }

        public string? Year { get; set; }

        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public string? Watched { get; set; }
        public string? Poster { get; set; }
        public string? ImdbRating { get; set; }
        public Movie(string? title, string? year, string? genre, string? watched, int? restricting_age, string? poster, string? imdbRating)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Watched = watched;
            Restricting_age = restricting_age;
            Poster = poster;
            ImdbRating = imdbRating;
        }
    }
}