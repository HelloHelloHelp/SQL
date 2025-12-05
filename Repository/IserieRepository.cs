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
                string sql = "SELECT * FROM serie_info";

                using SqlCommand command = new SqlCommand(sql, connection);
                {
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    using SqlDataReader reader = sqlDataReader;
                    {
                        while (reader.Read())
                        {
                            Models.Serie serie = new Models.Serie();
                            serie.ID = (int)reader["ID"];
                            serie.Titel = reader["Titel"].ToString();
                            serie.Date = (int)reader["Date"];
                            serie.Genre = reader["Genre"].ToString();
                            serie.Restricting_age = reader["Restricting_age"] == DBNull.Value ? null : (int)reader["Restricting_age"];
                            serie.Seasons = (int)reader["Seasons"];
                            serie.Platform = reader["Platform"].ToString();
                            serie.Watched = reader["Watched"].ToString();
                            serie.Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rating"));
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
