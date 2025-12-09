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
                                serie.Titel = reader.GetString("Titel");
                                serie.Date = reader.GetInt32("Date");
                                serie.Genre = reader.GetString("Genre");
                                serie.Restricting_age = reader.GetInt32("Restricting_age");
                                serie.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                                serie.Seasons = reader.GetInt32("Seasons");
                                serie.Rating = reader.GetInt32("Rating");
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
            serie.Titel = Request.Form["Titel"];
            serie.Date = int.Parse(Request.Form["Date"]);
            serie.Genre = Request.Form["Genre"];
            serie.Restricting_age = int.Parse(Request.Form["Restricting_age"]);
            serie.Seasons = int.Parse(Request.Form["Seasons"]);
            serie.Rating = int.Parse(Request.Form["Rating"]);
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
                                 "SET Titel=@Titel, Date=@Date, Genre=@Genre, Restricting_age=@Restricting_age, Seasons=@Seasons " +
                                 "WHERE ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Titel", serie.Titel);
                        command.Parameters.AddWithValue("@Date", serie.Date);
                        command.Parameters.AddWithValue("@Genre", serie.Genre);
                        command.Parameters.AddWithValue("@Restricting_age", serie.Restricting_age);
                        command.Parameters.AddWithValue("@Poster", serie.Poster);
                        command.Parameters.AddWithValue("@ID", serie.ID);
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
            Response.Redirect("/Series/Index");

        }
    }
}
