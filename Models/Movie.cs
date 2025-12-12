using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ZAAL_SQL.Models
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

        [JsonIgnore]
        public byte[]? Poster { get; set; }

        [JsonProperty("Poster")]
        public string? PosterUrl { get; set; }

        public string? Watched { get; set; }
        public string? ImdbRating { get; set; }
        public Movie(string? title, string? year, string? genre, int? restricting_age, byte[]? poster, string? watched, string? imdbRating)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Restricting_age = restricting_age;
            Poster = poster;
            Watched = watched;
            ImdbRating = imdbRating;
        }
    }
}