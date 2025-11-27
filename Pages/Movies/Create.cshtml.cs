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
            movie.Date = Request.Form["Date"];
            movie.Genre = Request.Form["Genre"];
            movie.Restricting_age = Request.Form["Restricting_age"];

            if (movie.Titel.Length == 0 || movie.Date.Length == 0 || movie.Genre.Length == 0 ||
                movie.Restricting_age.Length == 0)
            {
              errorMessage = ("All fields are required!");
                return;
            }

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

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            movie.Titel = ""; movie.Date = ""; movie.Genre = ""; movie.Restricting_age = "";
            successMessage = "New Movie Added!";
            Response.Redirect("/Movies/Index");
        }
    }

}
