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

namespace MoviesAPI.Pages.Series
{

    public class EditModel : PageModel
    {
        public MoviesAPI.Models.Serie serie = new MoviesAPI.Models.Serie();
        public string successMessage = "";
        public string errorMessage = "";


        public void OnGet()
        {
            String ID = Request.Query["ID"];

            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM   series WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                serie.ID = reader.GetInt32("ID");
                                serie.Title = reader.GetString("Title");
                                serie.Year = reader.GetString("Year");
                                serie.Genre = reader.GetString("Genre");
                                serie.Restricting_age = reader.GetInt32("Restricting_age");
                                serie.Plot = reader.GetString("Plot");
                                serie.TotalSeasons = reader.GetString("TotalSeasons");
                                serie.ImdbRating = reader.GetString("ImdbRating");
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
            serie.Title = Request.Form["Title"];
            serie.Year = Request.Form["Year"];
            serie.Genre = Request.Form["Genre"];
            serie.Restricting_age = int.Parse(Request.Form["Restricting_age"]);
            serie.Plot = Request.Form["Plot"];
            serie.TotalSeasons = Request.Form["TotalSeasons"];
            serie.ImdbRating = Request.Form["ImdbRating"];
            var file = Request.Form.Files["Poster"];

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    serie.Poster = ms.ToArray();
                }
            }


            try
            {
                String connectionString = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE series " +
                                 "SET Title=@Title, Year=@Year, Genre=@Genre, Restricting_age=@Restricting_age, Plot=@Plot, TotalSeasons=@TotalSeasons " +
                                 "WHERE ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", serie.Title);
                        command.Parameters.AddWithValue("@Year", serie.Year);
                        command.Parameters.AddWithValue("@Genre", serie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", serie.Restricting_age);
                        command.Parameters.AddWithValue("@TotalSeasons", serie.TotalSeasons);
                        command.Parameters.AddWithValue("@Plot", serie.Plot);
                        command.Parameters.AddWithValue("@Poster", serie.Poster);
                        command.Parameters.AddWithValue("@ID", serie.ID);
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
