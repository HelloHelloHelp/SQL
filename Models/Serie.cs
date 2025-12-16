using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ZAAL_SQL.Models
{
    [Table("serie_info", Schema = "dbo")]
    public class Serie
    {
        public Serie() { }

        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? Genre { get; set; }
        public string? TotalSeasons { get; set; }
        public string? Director { get; set; }
        public string? ImdbID { get; set; }
        public string? Rated { get; set; }
        public string? Language { get; set; }
        public string? Plot { get; set; }

        [JsonProperty("Runtime")]
        public string? Runtime { get; set; }

        [JsonIgnore]
        public byte[]? Poster { get; set; }


        public string? PosterUrl { get; set; }


        [JsonProperty("Poster")]
        [NotMapped]
        public string? OmdbPosterUrl { get; set; }

        [NotMapped]
        public string? PosterBase64 =>
            Poster != null
                ? $"data:image/jpeg;base64,{Convert.ToBase64String(Poster)}"
                : null;

        public string? Watched { get; set; }
        public string? ImdbRating { get; set; }
        public Serie(string? director, string? imdbID, string? rated, string? language, string? title, string? runtime, string? plot, string? year, string? genre, string? totalSeasons, byte[]? poster, string? watched, string? imdbRating)
        {
            Title = title;
            Plot = plot;
            Year = year;
            Genre = genre;
            TotalSeasons = totalSeasons;
            Poster = poster;
            Watched = watched;
            ImdbRating = imdbRating;
            Runtime = runtime;
            Rated = rated;
            Language = language;
            Director = director;
            ImdbID = imdbID;
        }
    }
}
