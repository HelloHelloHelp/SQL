using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MoviesAPI.Models;

namespace MoviesAPI.Pages.Movies
{

    public class EditModel : PageModel
    {
        public MoviesAPI.Models.Movie movie = new MoviesAPI.Models.Movie();
        public string successMessage = "";
        public string errorMessage = "";

  
        public void OnGet()
        {
            String ID = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM   movies WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                movie.ID =  reader.GetInt32("ID");
                                movie.Title = reader.GetString("Title");
                                movie.Year = reader.GetString("Year");
                                movie.Genre = reader.GetString("Genre");
                                movie.Restricting_age = reader.GetInt32("Restricting_age");
                                movie.Plot = reader.GetString("Plot");
                                movie.ImdbRating = reader.GetString("ImdbRating");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public async Task OnPost()
        {
            movie.Title = Request.Form["Title"];
            movie.Year = Request.Form["Year"];
            movie.Genre = Request.Form["Genre"];
            movie.Restricting_age = int.Parse(Request.Form["Restricting_age"]);
            movie.Plot = Request.Form["Plot"];
            movie.ImdbRating = Request.Form["ImdbRating"];

            try             {
                String connectionString = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE movies " +
                                 "SET Title=@Title, Year=@Year, Genre=@Genre, Restricting_age=@Restricting_age, Plot=@Plot,  ImdbRating=@ImdbRating " +
                                 "WHERE ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", movie.Title);
                        command.Parameters.AddWithValue("@Year", movie.Year);
                        command.Parameters.AddWithValue("@Genre", movie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", movie.Restricting_age);
                        command.Parameters.AddWithValue("@Plot", movie.Plot ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Watched", movie.Watched);
                        command.Parameters.AddWithValue("@ID", movie.ID);
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
