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

namespace ZAAL_SQL.Pages.Movies
{
    public class CreateModel : PageModel
    {
        public MoviesAPI.Models.Movie movie = new MoviesAPI.Models.Movie();
        public string successMessage = "";
        public string errorMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            movie.Titel = Request.Form["Titel"];
            movie.Date = Request.Form["Date"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Date"]);
            movie.Genre = Request.Form["Genre"];
            movie.Restricting_age = Request.Form["Restricting_age"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Restrcting_age"]);
            movie.Platform = Request.Form["Platform"];
            movie.Rating = Request.Form["Rating"].ToString() == "" ? 0 : Convert.ToInt32(Request.Form["Rating"]);

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO movies " +
                                 "(Titel, Date, Genre, Restricting_age) VALUES " +
                                 "(@Titel, @Date, @Genre, @Restricting_age);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Titel", movie.Titel);
                        command.Parameters.AddWithValue("@Date", movie.Date);
                        command.Parameters.AddWithValue("@Genre", movie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", movie.Restricting_age);
                        command.Parameters.AddWithValue("@Platform", movie.Platform);
                        command.Parameters.AddWithValue("@Rating", movie.Rating);
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
