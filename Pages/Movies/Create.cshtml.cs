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

namespace MoviesAPI.Pages.Movies
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
            movie.Title = Request.Form["Title"];
            movie.Year = Request.Form["Year"];
            movie.Genre = Request.Form["Genre"];
            movie.Plot = Request.Form["Plot"];
            movie.ImdbRating = Request.Form["ImdbRating"];
            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO movies " +
                                 "(Title, ImdbID, Director, Year, Genre, Rated, Language Plot, ImdbRating) VALUES " +
                                 "(@Title, @ImdbID, @Director, @Year, @Genre, @Rated, @Language, @Plot, @ImdbRating);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", movie.Title);
                        command.Parameters.AddWithValue("@ImdbID", movie.ImdbID);
                        command.Parameters.AddWithValue("@Director", movie.Director);
                        command.Parameters.AddWithValue("@Year", movie.Year);
                        command.Parameters.AddWithValue("@Genre", movie.Genre);
                        command.Parameters.AddWithValue("@Rated", movie.Rated);
                        command.Parameters.AddWithValue("@Language", movie.Language);
                        command.Parameters.AddWithValue("@Plot", movie.Plot);
                        command.Parameters.AddWithValue("@ImdbRating", movie.ImdbRating);
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
