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
            string constring = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
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
                            Models.Movie movie = new Models.Movie(
                                reader["Titel"].ToString(),
                                reader["Date"].ToString(),
                                reader["Genre"].ToString(),
                                Convert.ToInt32(reader["Restricting_age"])
                            );
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
