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
                                serie.Title = reader["Titel"].ToString();
                                serie.Year = reader["Date"].ToString();
                                serie.Genre = reader["Genre"].ToString();
                                serie.Plot = reader["Plot"].ToString();
                                serie.TotalSeasons = reader["TotalSeasons"].ToString();
                                serie.Runtime = reader["Runtime"].ToString();
                                serie.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                                serie.Watched = reader["Watched"].ToString();
                                serie.ImdbRating = reader["Rating"].ToString();
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
