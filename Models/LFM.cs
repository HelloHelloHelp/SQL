using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [Table("LFM", Schema = "dbo")]
    public class LFM
    {
        public LFM() { }

        public int ID { get; set; }
        public string? Title { get; set; }

        public string? Year { get; set; }

        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public string? Poster { get; set; }
        public string? ImdbRating { get; set; }
        public LFM(string? title, string? year, string? genre, int? restricting_age, string? poster, string? imdbRating)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Restricting_age = restricting_age;
            Poster = poster;
            ImdbRating = imdbRating;
        }
    }
}
