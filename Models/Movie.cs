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

        public string? Date { get; set; }

        public string? Genre { get; set; }
        public string? Restricting_age { get; set; }
         public bool? Watched { get; set; }
      public  int? Rating { get; set; }
        public Movie(string? titel, string? date, string? genre, string? restricting_age, bool? watched, int? rating)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
            Watched = watched;
            Rating = rating;
        }
    }
}