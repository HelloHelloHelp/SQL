using System.ComponentModel.DataAnnotations.Schema;

namespace ZAAL_SQL.Models
{
    [Table("LFS", Schema = "dbo")]
    public class SerieModel
    {
        public SerieModel() { }

        public int ID { get; set; }
        public string? Titel { get; set; }
        public int? Date { get; set; }
        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public int? Seasons { get; set; }
        public string? Watched { get; set; }
        public int? Rating { get; set; }
        public string? Platform { get; set; }

        public SerieModel(string? titel, int? date, string? genre, int? restricting_age, int? seasons, string? watched, int? rating, string? platform)
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
