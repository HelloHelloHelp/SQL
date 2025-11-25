namespace ZAAL_SQL.Models
{
    public class Movie
    {
       public List<Models.Movie> GetMovies = new List<Models.Movie>();

        public Movie() { }

        public int ID { get; set; }
        public string? Titel { get; set; }
        public string? Date { get; set; }
        public string? Genre { get; set; }
        public int Restricting_age { get; set; }

        public Movie(string? titel, string? date, string? genre, int restricting_age)
        {
            Titel = titel;
            Date = date;
            Genre = genre;
            Restricting_age = restricting_age;
        }
    }
}
