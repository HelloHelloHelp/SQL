using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MoviesAPI.Models;

namespace MoviesAPI.Pages.LFMAS
{
    public class IndexModel : PageModel
    {

        public List<MoviesAPI.Models.LFM> Movies = new List<MoviesAPI.Models.LFM>();
        public async Task OnGet()
        {
            try
            {
                string constring = "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM LFM";

                    using (SqlCommand command = new SqlCommand("SELECT * FROM LFM", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MoviesAPI.Models.LFM movie = new MoviesAPI.Models.LFM();
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
                                Movies.Add(movie);
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

        public List<MoviesAPI.Models.LFS> Series = new List<MoviesAPI.Models.LFS>();
        public async Task OnGet1()
        {
            try
            {
                string constring = "Data Source=localhost;Initial Catalog=LFMAS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM LFS";

                    using (SqlCommand command = new SqlCommand("SELECT * FROM LFS", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MoviesAPI.Models.LFS serie = new MoviesAPI.Models.LFS();
                                serie.ID = (int)reader["ID"];
                                serie.Titel = reader["Titel"].ToString();
                                serie.Date = (int)reader["Date"];
                                serie.Genre = reader["Genre"].ToString();
                                serie.Restricting_age = reader["Restricting_age"] == DBNull.Value ? null : (int)reader["Restricting_age"];
                                serie.Seasons = (int)reader["Seasons"];
                                serie.Poster = reader["Poster"].ToString();
                                serie.Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rating"));
                               
                                Series.Add(serie);
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
