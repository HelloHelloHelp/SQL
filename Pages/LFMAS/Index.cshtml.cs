using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

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
                    String sql = "SELECT * FROM movies";

                    using (SqlCommand command = new SqlCommand("SELECT * FROM LFM", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MoviesAPI.Models.LFM movie = new MoviesAPI.Models.LFM();
                                movie.ID = reader.GetInt32("ID");
                                movie.Titel = reader.GetString("Titel");
                                movie.Date = reader.GetInt32("Date");
                                movie.Genre = reader.GetString("Genre");
                                movie.Restricting_age = reader.GetInt32("Restricting_age");
                                movie.Platform = reader.GetString("Platform");
                                movie.Rating = reader.GetInt32("Rating");

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
                                serie.ID = reader.GetInt32("ID");
                                serie.Titel = reader.GetString("Titel");
                                serie.Date = reader.GetInt32("Date");
                                serie.Genre = reader.GetString("Genre");
                                serie.Restricting_age = reader.GetInt32("Restricting_age");
                                serie.Platform = reader.GetString("Platform");
                                serie.Rating = reader.GetInt32("Rating");

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
