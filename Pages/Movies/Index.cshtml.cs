using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ZAAL_SQL.Pages.Movies
{
    public class IndexModel : PageModel
    {

        public List<MoviesAPI.Models.Movie> ListMovies = new List<MoviesAPI.Models.Movie>();
        public async Task OnGet()
        {
            try
            {
                string constring = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM movies";

                    using (SqlCommand command = new SqlCommand("SELECT * FROM movies", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MoviesAPI.Models.Movie movie = new MoviesAPI.Models.Movie();
                                movie.ID =  reader.GetInt32("ID");
                                movie.Titel = reader.GetString("Titel");
                                movie.Date = reader.GetInt32("Date");
                                movie.Genre = reader.GetString("Genre");
                                movie.Restricting_age = reader.GetInt32("Restricting_age");
                                movie.Platform = reader.GetString("Platform");
                                movie.Watched = reader.GetBoolean("Watched");
                                movie.Rating = reader.GetInt32("Rating");

                                ListMovies.Add(movie);
                            }
                        }

                                                
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());
            }
        }
    }

}
