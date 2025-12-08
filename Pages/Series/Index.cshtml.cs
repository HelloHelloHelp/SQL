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

        public List<MoviesAPI.Models.Serie> series = new List<MoviesAPI.Models.Serie>();
        public async Task OnGet()
        {
            try
            {
                string constring = "Data Source=localhost;Initial C atalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
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
                                Models.Serie serie = new Models.Serie();
                                serie.ID = (int)reader["ID"];
                                serie.Titel = reader["Titel"].ToString();
                                serie.Date = (int)reader["Date"];
                                serie.Genre = reader["Genre"].ToString();
                                serie.Restricting_age = reader["Restricting_age"] == DBNull.Value ? null : (int)reader["Restricting_age"];
                                serie.Seasons = (int)reader["Seasons"];
                                serie.Platform = reader["Platform"].ToString();
                                serie.Watched = reader["Watched"].ToString();
                                serie.Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rating"));
                                series.Add(serie);

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
