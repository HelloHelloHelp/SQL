using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [Table("serie_info", Schema = "dbo")]
    public class Serie
    {
        public Serie() { }

          public int ID { get; set; }
        public string? Title { get; set; }

        public string? Year { get; set; }

        public string? Genre { get; set; }
        public int? Restricting_age { get; set; }
        public string? TotalSeasons { get; set; }
        public string? Watched { get; set; }
        public byte[]? Poster { get; set; }
        public string? ImdbRating { get; set; }
        public Serie(string? title, string? year, string? genre, string? watched, string? totalSeasons, int? restricting_age, byte[]? poster, string? imdbRating)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Watched = watched;
            Restricting_age = restricting_age;
            TotalSeasons = totalSeasons;
            Poster = poster;
            ImdbRating = imdbRating;
        }
    }
}
