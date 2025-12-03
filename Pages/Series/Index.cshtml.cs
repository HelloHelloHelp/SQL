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

namespace MoviesAPI.Pages.Series
{
    public class IndexModel : PageModel
    {

        public List<MoviesAPI.Models.Serie> ListSeries = new List<MoviesAPI.Models.Serie>();
        public async Task OnGet()
        {
            try
            {
                string constring = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM series";

                    using (SqlCommand command = new SqlCommand("SELECT * FROM series", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MoviesAPI.Models.Serie serie = new MoviesAPI.Models.Serie();
                                serie.ID = reader.GetInt32("ID");
                                serie.Titel = reader.GetString("Titel");
                                serie.Date = reader.GetInt32("Date");
                                serie.Genre = reader.GetString("Genre");
                                serie.Restricting_age = reader.GetInt32("Restricting_age");
                                serie.Seasons = reader.GetInt32("Seasons");
                                serie.Platform = reader.GetString("Platform");
                                serie.Watched = reader.GetString("Watched");
                                serie.Rating = reader.GetInt32("Rating");

                                ListSeries.Add(serie);
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
