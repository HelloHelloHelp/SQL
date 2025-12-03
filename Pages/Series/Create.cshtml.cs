using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.MicrosoftExtensions;
using MoviesAPI.Models;

namespace MoviesAPI.Pages.Series
{
    public class CreateModel : PageModel
    {
        public MoviesAPI.Models.Serie serie = new MoviesAPI.Models.Serie();
        public string successMessage = "";
        public string errorMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            serie.Titel = Request.Form["Titel"];
            serie.Date = Request.Form["Date"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Date"]);
            serie.Genre = Request.Form["Genre"];
            serie.Restricting_age = Request.Form["Restricting_age"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Restrcting_age"]);
            serie.Seasons = Request.Form["Seasons"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Date"]);
            serie.Platform = Request.Form["Platform"];
            serie.Rating = Request.Form["Rating"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Rating"]);

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO series " +
                                 "(Titel, Date, Genre, Restricting_age, Seasons) VALUES " +
                                 "(@Titel, @Date, @Genre, @Restricting_age, @Seasons);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Titel", serie.Titel);
                        command.Parameters.AddWithValue("@Date", serie.Date);
                        command.Parameters.AddWithValue("@Genre", serie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", serie.Restricting_age);
                        command.Parameters.AddWithValue("@Seasons", serie.Seasons);
                        command.Parameters.AddWithValue("@Platform", serie.Platform);
                        command.Parameters.AddWithValue("@Rating", serie.Rating);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Movies/Index");
        }
    }

}
