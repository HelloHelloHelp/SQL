using System.Data;
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
                string sql = "SELECT * FROM movie_info";

                using SqlCommand command = new SqlCommand(sql, connection);
                {
                  
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            Models.Movie movie = new Models.Movie();
                            movie.ID = (int)reader["ID"];
                            movie.Title = reader["Title "].ToString();
                            movie.Year = reader["Year"].ToString();
                            movie.Genre = reader["Genre"].ToString();
                            movie.Restricting_age = reader["Restricting_age"] == DBNull.Value ? null : (int)reader["Restricting_age"];
                            if (!reader.IsDBNull(reader.GetOrdinal("Poster")))
                            {
                                movie.Poster = (byte[])reader["Poster"];
                            }
                            else
                            {
                                movie.Poster = null;
                            }
                            movie.Watched = reader["Watched"].ToString();
                            movie.ImdbRating = reader["ImdbRating"].ToString();
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
//movie.Rating = (int)reader["Rating"] == DBNull.Value ? null : (int)reader["Rating"];