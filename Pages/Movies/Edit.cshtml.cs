using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace ZAAL_SQL.Pages.Movies
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
                                movie.Titel = reader.GetString("Titel");
                                movie.Date = reader.GetString("Date");
                                movie.Genre = reader.GetString("Genre");
                                movie.Restricting_age = reader.GetString("Restricting_age");
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

            try             {
                String connectionString = "Data Source=localhost;Initial Catalog=movies;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE movies " +
                                 "SET Titel=@Titel, Date=@Date, Genre=@Genre, Restricting_age=@Restricting_age " +
                                 "WHERE ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Titel", movie.Titel);
                        command.Parameters.AddWithValue("@Date", movie.Date);
                        command.Parameters.AddWithValue("@Genre", movie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", movie.Restricting_age);
                        command.Parameters.AddWithValue("@ID", movie.ID);
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
