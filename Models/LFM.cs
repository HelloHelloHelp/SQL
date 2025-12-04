using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [Table("LFM", Schema = "dbo")]
    public class LFM
    {
        public LFM() { }

        public int ID { get; set; }
        public string? Titel { get; set; }

        public int? Date { get; set; }

        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public string? Platform { get; set; }
        public int? Rating { get; set; }
        public LFM(string? titel, int? date, string? genre, int? restricting_age, string? platform, int? rating)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
            Platform = Platform;
            Rating = rating;
        }
    }
}
