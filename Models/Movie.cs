using System.ComponentModel.DataAnnotations.Schema;

namespace ZAAL_SQL.Models
{
    [Table("movie_info", Schema = "dbo")]
    public class Movie
    {
        public Movie() { }

        public int ID { get; set; }
        public string? Titel { get; set; }

        public int? Date { get; set; }

        public string? Genre { get; set; }
        public int Restricting_age { get; set; }

        public Movie(string? titel, int? date, string? genre, int restricting_age)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
        }
    }
}