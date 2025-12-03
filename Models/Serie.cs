using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [Table("serie_info", Schema = "dbo")]
    public class Serie
    {
        public Serie() { }

        public int ID { get; set; }
        public string? Titel { get; set; }
        public int? Date { get; set; }
        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public int? Seasons { get; set; }
        public string? Watched { get; set; }
        public int? Rating { get; set; }
        public string? Platform { get; set; }

        public Serie(string? titel, int? date, string? genre, int? restricting_age, int? seasons, string? watched, int? rating, string? platform)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
            Seasons = seasons;
            Watched = watched;
            Rating = rating;
            Platform = platform;
        }
    }
}
