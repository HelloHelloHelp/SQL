using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace MoviesAPI.Repository
{
    public interface ImovieRepository
    {
        List<Models.Movie> GetMovies();
    }
    public class movieRepository: ImovieRepository
    {
        
      public List<Models.Movie> GetMovies()
        {
            string constring = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                List<Models.Movie> movies = new List<Models.Movie>();
                string sql = "SELECT * FROM movies";

              using  SqlCommand command = new SqlCommand(sql, connection);
                {
                   using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            Models.Movie movie = new Models.Movie();
                            movie.ID = reader.GetInt32("ID");
                            movie.Titel = reader.GetString("Titel");
                            movie.Date = reader.GetString("Date");
                            movie.Genre = reader.GetString("Genre");
                            movie.Restricting_age = reader.GetString("Restricting_age");
                            movie.Watched = reader.GetBoolean("Watched");
                            movie.Rating = reader.GetInt32("Rating");
                            movies.Add(movie);
                        }
                    }
                  
                    reader.Close();
                }
               
             
                return movies;
            }
        }
    }
}
