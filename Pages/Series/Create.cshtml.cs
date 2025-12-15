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

        public async Task OnPost()
        {
            serie.Title = Request.Form["Title"];
            serie.Year = Request.Form["Year"].ToString();
            serie.Genre = Request.Form["Genre"];
            serie.Restricting_age = Request.Form["Restricting_age"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Restrcting_age"]);
            serie.Plot = Request.Form["Plot"];
            serie.TotalSeasons = Request.Form["TotalSeasons"];
            serie.ImdbRating = Request.Form["Rating"].ToString();

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO series " +
                                 "(Title, Year, Genre, Restricting_age, TotalSeasons, Plot, ImdbRating) VALUES " +
                                 "(@Title, @Year, @Genre, @Restricting_age, @TotalSeasons, @Plot, @ImdbRating);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", serie.Title);
                        command.Parameters.AddWithValue("@Year", serie.Year);
                        command.Parameters.AddWithValue("@Genre", serie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", serie.Restricting_age);
                        command.Parameters.AddWithValue("@Plot", serie.Plot);
                        command.Parameters.AddWithValue("@TotalSeasons", serie.TotalSeasons);
                        command.Parameters.AddWithValue("@ImdbRating", serie.ImdbRating);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Series/Index");
        }
    }

}
