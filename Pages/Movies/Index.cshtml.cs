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
using MoviesAPI.Models;

namespace MoviesAPI.Pages.Movies
{
    public class IndexModel : PageModel
    {

        public List<MoviesAPI.Models.Movie> movies = new List<MoviesAPI.Models.Movie>();
        public async Task OnGet()
        {
            try
            {
                string constring = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM movie_info";

                    using (SqlCommand command = new SqlCommand("SELECT * FROM movie_info", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
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
                                movie.ImdbRating = reader["ImdbRating"].ToString();
                                movies.Add(movie);

                                movies.Add(movie);
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
