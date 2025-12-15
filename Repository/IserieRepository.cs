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
                            serie.Title = reader["Title"].ToString();
                            serie.Year = reader["Year"].ToString();
                            serie.Genre = reader["Genre"].ToString();
                            serie.Restricting_age = reader["Restricting_age"] == DBNull.Value ? null : (int)reader["Restricting_age"];
                            serie.TotalSeasons = reader["TotalSeasons"].ToString();
                            serie.Plot = reader["Plot"].ToString();
                            serie.Poster = reader["Poster"] == DBNull.Value ? null : (byte[])reader["Poster"];
                            serie.Watched = reader["Watched"].ToString();
                            serie.ImdbRating = reader["ImdbRating"].ToString();
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
