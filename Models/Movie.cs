using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesAPI.Models
{
    [Table("movie_info", Schema = "dbo")]
    public class Movie
    {
        public Movie() { }

        public int ID { get; set; }
        public string? Titel { get; set; }

        public int? Date { get; set; }

        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public byte[]? Poster { get; set; }

        public string? Watched { get; set; }
      public  int? Rating { get; set; }
        public Movie(string? titel, int? date, string? genre, int? restricting_age, byte[]? poster, string? watched, int? rating)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
            Poster = poster;
            Watched = watched;
            Rating = rating;
        }
    }
}