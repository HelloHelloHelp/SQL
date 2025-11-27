using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string? Titel { get; set; }
        public string? Date { get; set; }
        public string? Genre { get; set; }
        public string? Restricting_age { get; set; }

        public Movie()
        {
           
        }

    }
}


/*int id, string? titel, string? date, string? genre, int restricting_age

     ID = id;
Titel = titel;
Date = date;
Genre = genre;
Restricting_age = restricting_age;*/