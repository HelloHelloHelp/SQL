using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ZAAL_SQL.Models
{
    [Table("LFM", Schema = "dbo")]
    public class MovieModel
    {
        internal static object movies;

        public MovieModel() { }

        public int ID { get; set; }
        public string? Title { get; set; }

        public string? Year { get; set; }

        public string? Plot { get; set; }
        public string? Genre { get; set; }
        public string? Rated { get; set; }
        public string? Language { get; set; }

        public string? Director { get; set; }
        public string? ImdbID { get; set; }


        [JsonProperty("Runtime")]
        public string? Runtime { get; set; }

        [JsonIgnore]
        public byte[]? Poster { get; set; }

        [JsonProperty("Poster")]
        public string? PosterUrl { get; set; }

        [JsonProperty("Poster")]
        [NotMapped]
        public string? OmdbPosterUrl { get; set; }


        [NotMapped]
        public string? PosterBase64 =>
            Poster != null
                ? $"data:image/jpeg;base64,{Convert.ToBase64String(Poster)}"
                : null;



        public string? ImdbRating { get; set; }
        public MovieModel(string? director, string? imdbID, string? rated, string? language, string? title, string? runtime, string? plot, string? year, string? genre, byte[]? poster, string? imdbRating)
        {
            Title = title;
            Plot = plot;
            Year = year;
            Genre = genre;
            Poster = poster;
            ImdbRating = imdbRating;
            Runtime = runtime;
            Rated = rated;
            Language = language;
            Director = director;
            ImdbID = imdbID;
        }
    }
}