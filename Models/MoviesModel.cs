using System.ComponentModel.DataAnnotations.Schema;

namespace ZAAL_SQL.Models
{
    [Table("LFM", Schema = "dbo")]
    public class MovieModel
    {
        public MovieModel() { }
         
        public int ID { get; set; }
        public string? Titel { get; set; }
        public int? Date { get; set; }
        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public int? Rating { get; set; }
        public string? Platform { get; set; }

        public MovieModel(string? titel, int? date, string? genre, int? restricting_age, int? rating, string? platform)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
            Rating = rating;
            Platform = platform;
        }
    }
}