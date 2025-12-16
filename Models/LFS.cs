using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MoviesAPI.Models
{
    [Table("LFS", Schema = "dbo")]
    public class LFS
    {
        public LFS() { }

        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? Genre { get; set; }
        public string? Rated { get; set; }
        public string? Language { get; set; }

        public string? Director { get; set; }
        public string? ImdbID { get; set; }

        [JsonProperty("Runtime")]
        public string? Runtime { get; set; }

        [JsonProperty("totalSeasons")]
        public string? TotalSeasons { get; set; }

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
        public LFS(string? director, string? imdbID, string? rated, string? language, string? title, string? year, string? runtime, string? plot, string? genre, string? totalSeasons, byte[]? poster, string? posterurl, string? imdbRating)
        {
            Title = title;
            Plot = plot;
            Year = year;
            Genre = genre;
            Poster = poster;
            PosterUrl = posterurl;
            ImdbRating = imdbRating;    
            TotalSeasons = totalSeasons;
            Runtime = runtime;
            Rated = rated;
            Language = language;
            Director = director;
            ImdbID = imdbID;
        }
    }
}
