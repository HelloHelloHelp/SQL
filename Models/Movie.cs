using System;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesAPI.Models
{
    [Table("movie_info", Schema = "dbo")]
    public class Movie
    {
        public Movie() { }

        public int ID { get; set; }
        public string? Title { get; set; }

        public string? Year { get; set; }

        public string? Runtime { get; set; }
        public string? Genre { get; set; }
        public string? Watched { get; set; }
        public string? Plot { get; set; }
        public byte[]? Poster { get; set; }
        public string? ImdbRating { get; set; }
        public Movie(string? title, string? plot, string? runtime, string? year, string? genre, string? watched, byte[]? poster, string? imdbRating)
        {
            Title = title;
            Plot = plot;
            Year = year;
            Genre = genre;
            Watched = watched;
            Poster = poster;
            ImdbRating = imdbRating;
            Runtime = runtime;
        }
    }
}