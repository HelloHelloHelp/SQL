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
        public string? Rated { get; set; }
        public string? Language { get; set; }
        public string? Genre { get; set; }
        public string? TotalSeasons { get; set; }
        public string? Watched { get; set; }
        public string? Director { get; set; }
        public string? ImdbID { get; set; }
        public string ? Plot { get; set; }
        public string? Runtime { get; set; }
        public byte[]? Poster { get; set; }
        public string? ImdbRating { get; set; }
        public Serie(string? director, string? imdbID, string? rated, string?language, string? title, string? plot, string? runtime, string? year, string? genre, string? watched, string? totalSeasons, byte[]? poster, string? imdbRating)
        {
            Title = title;
            Plot = plot;
            Year = year;
            Genre = genre;
            Watched = watched;
            TotalSeasons = totalSeasons;
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
