using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        public string? Rated { get; set; }

        public string? Director { get; set; }
        public string? ImdbID { get; set; }
        public string? Language { get; set; }

        [JsonProperty("Runtime")]
        public string? Runtime { get; set; }

        [JsonProperty("Plot")]
        public string? Plot { get; set; }

        [JsonIgnore]
        public byte[]? Poster { get; set; }


        [Column("PosterUrl")]
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
        public LFM(string? director, string? imdbID, string? rated, string? language, string? title, string? year, string? plot, string? runtime, string? genre, byte[]? poster, string? posterurl, string? imdbRating)
        {
            Title = title;
            Plot = plot;
            Year = year;
            Genre = genre;
            Poster = poster;
            PosterUrl = posterurl;
            ImdbRating = imdbRating;
            Runtime = runtime;
            Rated = rated;
            Language = language;
            Director = director;
            ImdbID = imdbID;
        }
    }
}
