using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace MoviesAPI.Repository
{
    public interface IserieRepository
    {
        List<Models.Serie> GetSeries();
    }
    public class serieRepository : IserieRepository
    {

        public List<Models.Serie> GetSeries()
        {
            string constring = "Data Source=localhost;Initial Catalog=series;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                List<Models.Serie> series = new List<Models.Serie>();
                string sql = "SELECT * FROM series";

                using SqlCommand command = new SqlCommand(sql, connection);
                {
                    using SqlDataReader reader = command.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            Models.Serie serie = new Models.Serie();
                            serie.ID = reader.GetInt32("ID");
                            serie.Titel = reader.GetString("Titel");
                            serie.Date = reader.GetInt32("Date");
                            serie.Genre = reader.GetString("Genre");
                            serie.Restricting_age = reader.GetInt32("Restricting_age");
                            serie.Seasons = reader.GetInt32("Seasons");
                            serie.Platform = reader.GetString("Platform");
                            serie.Watched = reader.GetString("Watched");
                            serie.Rating = reader.GetInt32("Rating");
                            series.Add(serie);
                        }
                    }

                    reader.Close();
                }


                return series;
            }
        }
    }
}
